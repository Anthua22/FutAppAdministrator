using NombramientoPartidos.Services;
using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
using System.ComponentModel;

namespace NombramientoPartidos.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        
        public bool Entrar(string dni, string pass)
        {
            Administrador x = ApiRest.RescatarAdministrador(dni);
            if(x!=null && x.Pass.Equals(Utils.EncriptarEnSHA1(pass)))
            {
                return true;
            }
            return false;
        }
    }
}
