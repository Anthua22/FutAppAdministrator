using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NombramientoPartidos.Utilidades
{
    public static class EncriptacionDescriptacion
    {
        public static byte[] EncodingPrivateKey(string privada)
        {
            byte[] key = UTF8Encoding.UTF8.GetBytes(privada);
            int keysize = 32;
            Array.Resize(ref key, keysize);
            return key;
        }

        public static byte[] EncodingPublicKey(string publica)
        {
            byte[] iv = UTF8Encoding.UTF8.GetBytes(publica);
            int ivSize = 16;
            Array.Resize(ref iv, ivSize);
            return iv;
        }

        public static void Encriptar(string mensaje,string path, byte[] key, byte[] iv)
        {
            FileStream fileStream = new FileStream(path, FileMode.Create);

            //Creo el algoritmo
            Rijndael RijndaelAlg = Rijndael.Create();

            CryptoStream cryptoStream = new CryptoStream(fileStream, RijndaelAlg.CreateEncryptor(key, iv), CryptoStreamMode.Write);

            StreamWriter streamWriter = new StreamWriter(cryptoStream);

            streamWriter.WriteLine(mensaje);

            streamWriter.Close();
            cryptoStream.Close();
            fileStream.Close();

        }

        public static string Desincriptar(string path, byte[] key, byte[] iv)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);

            //Creo el algoritmo
            Rijndael RijndaelAlg = Rijndael.Create();

            CryptoStream cryptoStream = new CryptoStream(fileStream, RijndaelAlg.CreateDecryptor(key, iv), CryptoStreamMode.Read);

            StreamReader streamReader = new StreamReader(cryptoStream);

            string mensaje = streamReader.ReadLine();

            streamReader.Close();
            cryptoStream.Close();
            fileStream.Close();
            return mensaje;

        }

    }
}
