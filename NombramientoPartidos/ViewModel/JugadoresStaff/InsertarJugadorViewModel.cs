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
        public DateTime FechaNacimiento { get; set; }
        public Jugador JugadorInsertar { get; set; }

        public InsertarJugadorViewModel()
        {
            Categorias = Utils.Categorias;
            JugadorInsertar = new Jugador();
            EquipoJugador = new Equipo();
            FechaNacimiento = DateTime.Now;
        }

        public void Filtro(string categoria)
        {
            Equipos = Utils.FiltroEquipos(categoria);
        }

        public void PonerImagen(Image imagen)
        {
            imagen.Source = new BitmapImage(new Uri(Utils.ObtenerRutaFichero()));
        }

        public bool Execute()
        {
            JugadorInsertar.Equipo = EquipoJugador.IdEquipo;
            JugadorInsertar.Dni = JugadorInsertar.Dni.ToUpper();
            JugadorInsertar.Categoria = Utils.ObtenerCategoriaJugador(FechaNacimiento,2019);
            JugadorInsertar.Fecha_Nacimiento = FechaNacimiento.Year + "-" + FechaNacimiento.Month + "-" + FechaNacimiento.Day;
            return ApiRest.InsertJugador(JugadorInsertar);
        }

        public void AsignarEquipo()
        {
            if (EquipoJugador != null)
            {
                JugadorInsertar.Id = EquipoJugador.IdEquipo;
            }
        }
    }
}
