using NombramientoPartidos.Services;
using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
