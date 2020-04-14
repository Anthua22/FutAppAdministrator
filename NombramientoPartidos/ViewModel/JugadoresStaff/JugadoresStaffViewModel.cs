
using NombramientoPartidos.View.JugadoresStaff;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NombramientoPartidos.ViewModel.JuadoresStaff
{
    class JugadoresStaffViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public JugadoresStaffViewModel()
        {

        }

        public bool InsertJugador()
        {
            InsertarJugadorView insertJugador = new InsertarJugadorView();
            return (bool)insertJugador.ShowDialog();
        }

        public bool UpdateJugador()
        {
            EditarJugadorView editarJugador = new EditarJugadorView();
            return (bool)editarJugador.ShowDialog();
        }
    }
}
