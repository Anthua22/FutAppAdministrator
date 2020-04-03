using NombramientoPartidos.Services;
using NombramientoPartidos.Utilidades.ClasesPojos;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System;

namespace NombramientoPartidos.Utilidades
{
    public static class Utils
    {
        public static ObservableCollection<string> Categorias { get; }
  

        static Utils()
        {
            Categorias = new ObservableCollection<string>()
            {
                "1º División",
                "2º División",
                "2ºB División",
                "3º División",
                "Preferente",
                "Fútbol Base"
            };
        }

       
        public static string EncriptarEnSHA1(string str)
        {
            SHA1 sha1 = SHA1Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            StringBuilder sb = new StringBuilder();
            byte[] stream = sha1.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
       
        public static bool ControlCampos(Arbitro arbitro)
        {
            if (arbitro.Pass.Length != 40 && ValidacionesRegexp.ValidarPass(arbitro.Pass))
            {
                string v = EncriptarEnSHA1(arbitro.Pass);
                arbitro.Pass = v;
            } 
            if(ValidacionesRegexp.ComprobarCategoria(arbitro.Categoria)
                && ValidacionesRegexp.ValidarFecha(arbitro.Fecha_Nacimiento)
                && ValidacionesRegexp.ValidarEmail(arbitro.Email))
            {
                return true;
            }
           
            return false;
        }

        public static byte[] ImagenABytes(string path)
        {
            MemoryStream ms = new MemoryStream();
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

            ms.SetLength(fs.Length);
            fs.Read(ms.GetBuffer(), 0, (int)fs.Length);
            byte[] btyimagen = ms.GetBuffer();
            ms.Flush();
            fs.Close();
            return btyimagen;

        }

        public static bool HayCamposVacios(string dni, string contraseña, string fecha_nacimiento, string email, string provincia, string cp)
        {
            return (string.IsNullOrWhiteSpace(dni) || string.IsNullOrWhiteSpace(contraseña) || string.IsNullOrWhiteSpace(fecha_nacimiento) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(provincia) || string.IsNullOrWhiteSpace(cp));
        }

        public static BitmapImage ToWpfImage(System.Drawing.Image img)
        {
            MemoryStream ms = new MemoryStream();  // no using here! BitmapImage will dispose the stream after loading
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            BitmapImage ix = new BitmapImage();
            ix.BeginInit();
            ix.CacheOption = BitmapCacheOption.OnLoad;
            ix.StreamSource = ms;
            ix.EndInit();
            return ix;
        }

        public static string ObtnerRutaFichero()
        {
            OpenFileDialog dialogoImagen = new OpenFileDialog();
            string ruta = "";
            dialogoImagen.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            dialogoImagen.Filter = "Imágenes JPG (*.jpg)|*.jpg|Imágenes PNG (*.png)|*.png|Todos los archivos (*.*)|*.*";

            dialogoImagen.FilterIndex = 3;

            dialogoImagen.RestoreDirectory = true;

            if (dialogoImagen.ShowDialog() == DialogResult.OK)
            {

                ruta = dialogoImagen.FileName;

            }
            return ruta;
        }
        
       
    }
}
