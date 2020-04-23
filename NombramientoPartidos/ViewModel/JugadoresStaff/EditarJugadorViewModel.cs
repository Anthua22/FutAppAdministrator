using NombramientoPartidos.Services;
using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
using System;
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

        public string CategoriaJugador { get; set; }

        public Equipo EquipoCambio { get; set; }

        string fotoantigua;

        public EditarJugadorViewModel()
        {
            Categorias = Utils.Categorias;
            EquipoJugador = new Equipo();
            EquipoCambio = new Equipo();
            Equipos = new ObservableCollection<Equipo>();
            Jugadores = new ObservableCollection<Jugador>();
        }

        public void FiltroEquipos(string categoria)
        {
            Equipos = Utils.FiltroEquipos(categoria);
        }

        public void FiltroJugadores()
        {
            if (EquipoJugador != null)
            {
                Jugadores = new ObservableCollection<Jugador>(ApiRest.RescatarJugadores().Where(x => x.Equipo == EquipoJugador.IdEquipo).OrderBy(y => y.Nombre_Completo));
            }
          
        }

        public void EditarImagen()
        {
            string ruta = Utils.ObtenerRutaFichero().Replace('\\', '/');
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                if (!JugadorUpdate.Foto.Equals("/Assets/defecto.jpg"))
                {
                    string[] urlBlob = JugadorUpdate.Foto.Split('/');

                    fotoantigua = urlBlob[urlBlob.Length - 1];
                }
                JugadorUpdate.Foto = ruta;
            }

        }
        public bool Update()
        {
            JugadorUpdate.Dni = JugadorUpdate.Dni.ToUpper();
            ValidacionesRegexp.ValidarDniNie(JugadorUpdate.Dni);
            JugadorUpdate.Equipo = EquipoCambio.IdEquipo;
            string[] fecha = JugadorUpdate.Fecha_Nacimiento.Split('-');
            JugadorUpdate.Categoria = Utils.ObtenerCategoriaJugador(new DateTime(int.Parse(fecha[0]),int.Parse(fecha[1]),int.Parse(fecha[2])), 2019);
            if (!JugadorUpdate.Foto.Equals("/Assets/defecto.jpg") && !JugadorUpdate.Foto.Contains("http"))
            {
                string[] urlBlob = JugadorUpdate.Foto.Split('/');
                BlobStorage.EliminarImagen(fotoantigua, JugadorUpdate);
                JugadorUpdate.Foto = BlobStorage.GuardarImagen(JugadorUpdate.Foto, urlBlob[urlBlob.Length - 1], JugadorUpdate);
            }
            return ApiRest.UpdateJugador(JugadorUpdate);
        }
    }
}
