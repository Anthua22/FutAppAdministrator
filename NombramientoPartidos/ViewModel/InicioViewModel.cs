using NombramientoPartidos.Utilidades.ClasesPojos;
using NombramientoPartidos.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NombramientoPartidos.ViewModel
{
    class InicioViewModel : INotifyPropertyChanged
    {
        public int MyProperty { get; set; }

        public Administrador AdministradorActual { get; set; }
        public ConfiguracionCuenta Configuracion { get; set; }


        public InicioViewModel(Administrador administrador)
        {
            AdministradorActual = administrador;
           // Configuracion = new ConfiguracionCuenta(administrador);
        }

       

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
