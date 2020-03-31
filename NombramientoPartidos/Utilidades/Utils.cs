using NombramientoPartidos.Utilidades.ClasesPojos;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Media.Imaging;

namespace NombramientoPartidos.Utilidades
{
    public static class Utils
    {
        public static string EncriptarEnSHA1(string str)
        {
            SHA1 sha1 = SHA1Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            StringBuilder sb = new StringBuilder();
            byte[] stream = sha1.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
       

        public static bool ControlCamposUpdate(Arbitro arbitro)
        {
            if (arbitro.Pass.Length != 40 && ValidacionesRegexp.ValidarPass(arbitro.Pass))
            {
                arbitro.Pass = EncriptarEnSHA1(arbitro.Pass);
               
            } 
            if(ValidacionesRegexp.ComprobarCategoria(arbitro.Categoria) && ValidacionesRegexp.ValidarFecha(arbitro.Fecha_Nacimiento) && ValidacionesRegexp.ValidarEmail(arbitro.Email))
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
        public static BitmapImage ObtenerBitmap(Arbitro e)
        {
            var bytes = Encoding.UTF8.GetBytes(e.Foto);
           
            MemoryStream ms = new MemoryStream(bytes);
            Image img = Image.FromStream(ms);
            ms.Close();
            return ToWpfImage(img);
        }
    }
}
