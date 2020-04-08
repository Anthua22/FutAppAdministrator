using NombramientoPartidos.Services;
using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NombramientoPartidos.ViewModel.JuadoresStaff
{
    class InsertarJugadorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<string> Categorias { get; private set; }
        public ObservableCollection<Equipo> Equipos { get; private set; }
        public Equipo EquipoJugador { get; set; }
        public string Categoria { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Jugador JugadorInsertar { get; set; }

        public InsertarJugadorViewModel()
        {
            Categorias = Utils.Categorias;
            JugadorInsertar = new Jugador();
            EquipoJugador = new Equipo();
            FechaNacimiento = DateTime.Now;
        }

        public void Filtro()
        {
            switch (Categoria)
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
              
            }
        }

        public void PonerImagen(Image imagen)
        {
            imagen.Source = new BitmapImage(new Uri(Utils.ObtenerRutaFichero()));
        }

        public bool Execute()
        {
            JugadorInsertar.Equipo = EquipoJugador.IdEquipo;
            JugadorInsertar.Fecha_Nacimiento = FechaNacimiento.Year + "-" + FechaNacimiento.Month + "-" + FechaNacimiento.Day;
            return ApiRest.InsertJugador(JugadorInsertar);
        }
    }
}
