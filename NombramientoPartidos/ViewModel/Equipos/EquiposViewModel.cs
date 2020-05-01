using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using NombramientoPartidos.Services;
using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
namespace NombramientoPartidos.ViewModel.Equipos
{
    public enum Accion
    {
        Nuevo,
        Editar,
        Borrar
    }

    class EquiposViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Equipo Equipo { get; set; }
        public ObservableCollection<string> Categorias { get; set; }
        public ObservableCollection<Equipo> ListaEquipos { get; private set; }
        public Accion Accion { get; private set; }
        public ObservableCollection<string> Provincias { get; set; }
        private ObservableCollection<Partido> Partidos { get; set; }
        private ObservableCollection<Jugador> Jugadores { get; set; }
        private ObservableCollection<Staff> Staffs { get; set; }

        string fotoantigua;

        public EquiposViewModel(Accion accion)
        {
            this.Accion = accion;

            if(this.Accion == Accion.Nuevo)
            {
                Equipo = new Equipo()
                {
                    Foto = "/Assets/equipodefecto.jpg"
                };
            }
            Categorias = Utils.Categorias;
            Provincias = Utils.Provincias;
            Partidos = new ObservableCollection<Partido>();
            Jugadores = new ObservableCollection<Jugador>();
            Staffs = new ObservableCollection<Staff>();
        }

        public int Save_Execute()
        {
            switch (Accion)
            {
                case Accion.Nuevo:
                    if (Equipo.Categoria != null)
                    {  
                        ValidacionesRegexp.ValidarEmail(Equipo.Correo);
                        if (!Equipo.Foto.Equals("/Assets/equipodefecto.jpg") && !Equipo.Foto.Contains("http"))
                        {
                            string[] referenceblob = Equipo.Foto.Split('/');
                            Equipo.Foto = BlobStorage.GuardarImagen(Equipo.Foto, referenceblob[referenceblob.Length - 1], Equipo);
                        }
                        
                        ApiRest.InsertEquipo(Equipo);
                        return 1;
                        
                  

                    }
                    else return -1;

                case Accion.Editar:
                    if (!string.IsNullOrWhiteSpace(Equipo.Nombre) && !string.IsNullOrWhiteSpace(Equipo.Provincia) && Equipo.Categoria != null)
                    { 
                        ValidacionesRegexp.ValidarEmail(Equipo.Correo);
                        if (!Equipo.Foto.Equals("/Assets/equipodefecto.jpg") && !Equipo.Foto.Contains("http"))
                        {
                            string[] referenceblob = Equipo.Foto.Split('/');
                            BlobStorage.EliminarImagen(fotoantigua, Equipo);
                            Equipo.Foto =  BlobStorage.GuardarImagen(Equipo.Foto, referenceblob[referenceblob.Length - 1], Equipo);
                        }
                       
                        ApiRest.UpdateEquipo(Equipo);
                        return 2;
                    }
                    else
                        return -1;

                case Accion.Borrar:

                    MessageBoxResult messageresult = MessageBox.Show("Esta seguro que desea el eliminar el Equipo: " + Equipo.Nombre + "?. Los jugadores y staffs que pertenezcan a este equipo o los partidos que haya participado se verán afectados", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (messageresult == MessageBoxResult.Yes)
                    {
                        CambiaEquipoDelte();
                        ApiRest.DeleteEquipo(Equipo.IdEquipo);
                        if (!Equipo.Foto.Equals("/Assets/equipodefecto.jpg"))
                        {
                            string[] referenceblob = Equipo.Foto.Split('/');
                            BlobStorage.EliminarImagen(referenceblob[referenceblob.Length - 1], Equipo);
                        }
                        return 3;
                    }
                    return 0;

                default:
                    return 0;
            }

            
        }

        public void CambiaAccion(Accion accion)
        {
            LimpiaCampos();
            this.Accion = accion;
        }
        public void FiltroCategoria()
        {
            ListaEquipos = Utils.FiltroEquipos(Equipo.Categoria);
        }

        public void LimpiaCampos()
        {
            Equipo = new Equipo()
            {
                Foto = "/Assets/equipodefecto.jpg"
            };
            ListaEquipos = new ObservableCollection<Equipo>();
        }

        public void ElegirFoto()
        {
            string ruta = Utils.ObtenerRutaFichero().Replace('\\','/');
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                if (!Equipo.Foto.Equals("/Assets/equipodefecto.jpg"))
                {
                    string[] urlBlob = Equipo.Foto.Split('/');

                    fotoantigua = urlBlob[urlBlob.Length - 1];
                }
                Equipo.Foto = ruta;
            }
        }

        private void CambiaEquipoDelte()
        {
            Partidos = new ObservableCollection<Partido>( ApiRest.RescartarPartidos().Where(x => x.EquipoLocal == Equipo.IdEquipo || x.EquipoVisitante == Equipo.IdEquipo));
            Jugadores = new ObservableCollection<Jugador>(ApiRest.RescatarJugadores().Where(x=>x.Equipo == Equipo.IdEquipo));
            Staffs = new ObservableCollection<Staff>(ApiRest.RescatarStaffs().Where(x=> x.Equipo == Equipo.IdEquipo));

            for(int i =0; i < Partidos.Count; i++)
            {
                if(Partidos[i].EquipoLocal == Equipo.IdEquipo)
                {
                    Partidos[i].EquipoLocal = -1;
                }else if(Partidos[i].EquipoVisitante == Equipo.IdEquipo)
                {
                    Partidos[i].EquipoVisitante = -1;
                }
                ApiRest.UpdatePartido(Partidos[i]);
            }

            for(int i = 0; i<Jugadores.Count; i++)
            {
                Jugadores[i].Equipo = -1;
                ApiRest.UpdateJugador(Jugadores[i]);
            }

            for(int i = 0; i<Staffs.Count; i++)
            {
                Staffs[i].Equipo = -1;
                ApiRest.UpdateStaff(Staffs[i]);
            }
        }
    }
}
