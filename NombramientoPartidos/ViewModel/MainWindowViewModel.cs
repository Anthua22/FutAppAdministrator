using NombramientoPartidos.Services;
using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace NombramientoPartidos.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string User { get; set; }
        public string Pass { get; set; }

        public bool Contraseña { get; set; }
        public Administrador AdministradorActual { get; set; }

        public Administrador PosibleAdministrador { get; set; }

        private string Key { get; set; }
        private string Path { get; set; }

        public MainWindowViewModel()
        {
            AdministradorActual = new Administrador();
            Key = "hAC8hMf3N5Zb/DZhFKi6Sg ==";
            Path = "Datos.bin";
        }

        public bool Entrar(string pass)
        {
            Pass = pass;
            PosibleAdministrador = new Administrador(User, Pass);
            AdministradorActual = ApiRest.RescatarAdministrador(User);


            if(AdministradorActual != null && AdministradorActual.Pass.Equals(Utils.EncriptarEnSHA1(Pass)))
            {
                Properties.Settings.Default.GuardarContraseña= Contraseña;
                Properties.Settings.Default.Save();
                return true;
            }
            return false;
        }

        public void GuardarContraseña()
        {
            if (Properties.Settings.Default.GuardarContraseña)
            {

                if (File.Exists("Datos.bin"))
                {
                    File.Delete("Datos.bin");
                }

                using (Stream stream = new FileStream("Datos.bin", FileMode.Create, FileAccess.Write))
                { 
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, PosibleAdministrador);

                }
              //  Encriptar();
               
                
            }
        }

        public void ObtenerContraseña()
        {
            IFormatter formatter = new BinaryFormatter();
            using(Stream stream = new FileStream("Datos.bin",FileMode.Open, FileAccess.Read))
            {
                AdministradorActual = (Administrador)formatter.Deserialize(stream);
            }
        }




        public bool EntrarcanExecute()
        {
            return (User != null && Pass != null);
        }

        private void Encriptar()
        {
            byte[] plainContent = File.ReadAllBytes(Path);
            using(var Des = new DESCryptoServiceProvider())
            {
               int g = Des.BlockSize;
                Des.IV = Encoding.UTF8.GetBytes(Key);
                Des.Key = Encoding.UTF8.GetBytes(Key);
                Des.Mode = CipherMode.CBC;
                Des.Padding = PaddingMode.PKCS7;

                using(var menStream = new MemoryStream())
                {
                    CryptoStream cryptoStream = new CryptoStream(menStream, Des.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(plainContent, 0, plainContent.Length);
                    cryptoStream.FlushFinalBlock();
                    File.WriteAllBytes(Path, menStream.ToArray());

                }
            }
        }

        private void Desincriptar()
        {
            byte[] encryp = File.ReadAllBytes(Path);
            using(var Des = new DESCryptoServiceProvider())
            {
                Des.IV = Encoding.UTF8.GetBytes(Key);
                Des.Key = Encoding.UTF8.GetBytes(Key);
                Des.Mode = CipherMode.CBC;
                Des.Padding = PaddingMode.PKCS7;

                using(var menStream = new MemoryStream())
                {
                    CryptoStream cryptoStream = new CryptoStream(menStream, Des.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(encryp, 0, encryp.Length);
                    cryptoStream.FlushFinalBlock();
                    File.WriteAllBytes(Path, menStream.ToArray());

                }
            }
        }
    }
}
