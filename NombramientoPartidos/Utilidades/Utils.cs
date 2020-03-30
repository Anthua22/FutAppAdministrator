using NombramientoPartidos.Utilidades.ClasesPojos;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;

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

        public static Image ObtenerBitmap(Arbitro e)
        {
            byte[] arraybty = e.Foto;
            MemoryStream ms = new MemoryStream(arraybty);
            Image img = Image.FromStream(ms);
            ms.Close();
            return img;
        }
    }
}
