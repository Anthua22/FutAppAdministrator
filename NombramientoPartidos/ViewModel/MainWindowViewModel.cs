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

        private string ClavePrivada { get; set; }
        private string ClavePublic { get; set; }

        public MainWindowViewModel()
        {
            AdministradorActual = new Administrador();
            ClavePublic = "Archivo2334@";
            User = "";
            ClavePrivada = "Dfdf@FF4523.";
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
                //Se comprueba si el archivo existe y si existe se elimina, así siempre que se marque la casilla se guardará 
                //las credenciales puesta consiguiendo así que diferentes usuarios puedan guardar su contraseña
                if (File.Exists("Datos.txt"))
                {
                    File.Delete("Datos.txt");
                }

                string Datos = "usuraio="+PosibleAdministrador.Dni+','+"contraseña="+PosibleAdministrador.Pass;
                EncriptacionDescriptacion.Encriptar(Datos, "Datos.txt",EncriptacionDescriptacion.EncodingPrivateKey(ClavePrivada), EncriptacionDescriptacion.EncodingPublicKey(ClavePublic));
               
                
            }
        }

        public void ObtenerContraseña()
        {
            if (File.Exists("Datos.txt"))
            {
                string datos= EncriptacionDescriptacion.Desincriptar("Datos.txt", EncriptacionDescriptacion.EncodingPrivateKey(ClavePrivada), EncriptacionDescriptacion.EncodingPublicKey(ClavePublic));

                string[] credenciales = datos.Split(',');
                string[] usuario = credenciales[0].Split('=');
                string[] contraseña = credenciales[1].Split('=');
                User = usuario[1];
                Pass = contraseña[1]; 
            
            }
          
            
        }
      




        public bool EntrarcanExecute()
        {
            return (User != null && Pass != null);
        }

       

      
    }
}
