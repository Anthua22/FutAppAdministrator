using NombramientoPartidos.Services;
using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace NombramientoPartidos.ViewModel.JuadoresStaff
{
    class EditarJugadorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Jugador> Jugadores { get; set; }

        public ObservableCollection<string> Categorias { get; set; }

        public ObservableCollection<Equipo> Equipos { get; set; }

        public Jugador JugadorUpdate { get; set; }

        public Equipo EquipoJugador { get; set; }

        public EditarJugadorViewModel()
        {
            Categorias = Utils.Categorias;
            EquipoJugador = new Equipo();
            Equipos = new ObservableCollection<Equipo>();
            Jugadores = new ObservableCollection<Jugador>();
        }

        public void FiltroEquipos(string categoria)
        {
            switch (categoria)
            {

                case "1º División":
                    Equipos = new ObservableCollection<Equipo>(ApiRest.RescatarEquipos().Where(x => x.Categoria.Equals("1º División")).OrderBy(y => y.Nombre));
                    break;
                case "2º División":
                    Equipos = new ObservableCollection<Equipo>(ApiRest.RescatarEquipos().Where(x => x.Categoria.Equals("2º División")).OrderBy(y => y.Nombre));
                    break;
                case "2ºB División":
                    Equipos = new ObservableCollection<Equipo>(ApiRest.RescatarEquipos().Where(x => x.Categoria.Equals("2ºB División")).OrderBy(y => y.Nombre));
                    break;
                case "3º División":
                    Equipos = new ObservableCollection<Equipo>(ApiRest.RescatarEquipos().Where(x => x.Categoria.Equals("3º División")).OrderBy(y => y.Nombre));
                    break;
                case "Preferente":
                    Equipos = new ObservableCollection<Equipo>(ApiRest.RescatarEquipos().Where(x => x.Categoria.Equals("Preferente")).OrderBy(y => y.Nombre));
                    break;
                case "Fútbol Base":
                    Equipos = new ObservableCollection<Equipo>(ApiRest.RescatarEquipos().Where(x => x.Categoria.Equals("Fútbol Base")).OrderBy(y => y.Nombre));
                    break;
                case "Regional":
                    Equipos = new ObservableCollection<Equipo>(ApiRest.RescatarEquipos().Where(x => x.Categoria.Equals("Regional")).OrderBy(y => y.Nombre));
                    break;
            }
        }

        public void FiltroJugadores()
        {
            Jugadores = new ObservableCollection<Jugador>(ApiRest.RescatarJugadores().Where(x => x.Equipo == EquipoJugador.IdEquipo).OrderBy(y => y.Nombre_Completo));

        }


    }
}
