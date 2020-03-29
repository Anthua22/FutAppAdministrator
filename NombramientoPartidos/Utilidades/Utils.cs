using NombramientoPartidos.Utilidades.ClasesPojos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
    }
}
