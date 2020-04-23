using NombramientoPartidos.Services;
using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace NombramientoPartidos.ViewModel.JugadoresStaff
{
    class BorrarJugadorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Equipo> Equipos { get; set; }

        public ObservableCollection<Jugador> Jugadores { get; set; }

        public ObservableCollection<string> Categorias { get; set; }

        public Equipo EquipoJugador { get; set; }

        public Jugador JugadorDelete { get; set; }

        public BorrarJugadorViewModel()
        {
            Categorias = Utils.Categorias;
            Jugadores = new ObservableCollection<Jugador>();
            EquipoJugador = new Equipo();
            Equipos = new ObservableCollection<Equipo>();
            JugadorDelete = new Jugador();
        }

        public void Filtro(string categoria)
        {
            Equipos = Utils.FiltroEquipos(categoria);
        }

        public void FiltroJugadores()
        {
            if (EquipoJugador != null)
            {
                Jugadores = new ObservableCollection<Jugador>(ApiRest.RescatarJugadores().Where(x => x.Equipo == EquipoJugador.IdEquipo).OrderBy(y=>y.Nombre_Completo));
            }

        }

        public bool DeleteExecute()
        {
            MessageBoxResult messageresult = MessageBox.Show("Esta seguro que desea eliminar a " + JugadorDelete.Nombre_Completo + " del equipo: " + EquipoJugador.Nombre + '?', "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageresult == MessageBoxResult.Yes)
            {
                if (!JugadorDelete.Foto.Equals("/Assets/equipodefecto.jpg"))
                {
                    string[] blobreference = JugadorDelete.Foto.Split('/');
                    BlobStorage.EliminarImagen(blobreference[blobreference.Length - 1], JugadorDelete);
                }

                ApiRest.DeleteteJugador(JugadorDelete.Id);
                return true;
            }
            return false;
        }
    }

}
