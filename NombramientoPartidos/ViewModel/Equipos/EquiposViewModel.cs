using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
        }

        public int Save_Execute()
        {
            switch (Accion)
            {
                case Accion.Nuevo:
                    if (Equipo.Categoria != null)
                    {
                        
                        if (!string.IsNullOrWhiteSpace(Equipo.Nombre) && !string.IsNullOrWhiteSpace(Equipo.Provincia))
                        {
                            if (!Equipo.Foto.Equals("/Assets/equipodefecto.jpg") && !Equipo.Foto.Contains("http"))
                            {
                                string[] referenceblob = Equipo.Foto.Split('/');
                                BlobStorage.GuardarImagen(Equipo.Foto, referenceblob[referenceblob.Length - 1], Equipo);
                            }
                            ApiRest.InsertEquipo(Equipo);
                            return 1;
                        }
                        else return -1;

                    }
                    else return -1;

                case Accion.Editar:
                    if (!string.IsNullOrWhiteSpace(Equipo.Nombre) && !string.IsNullOrWhiteSpace(Equipo.Provincia) && Equipo.Categoria != null)
                    {
                        if (!Equipo.Foto.Equals("/Assets/equipodefecto.jpg") && !Equipo.Foto.Contains("http"))
                        {
                            string[] referenceblob = Equipo.Foto.Split('/');
                            BlobStorage.EliminarImagen(fotoantigua, Equipo);
                            BlobStorage.GuardarImagen(Equipo.Foto, referenceblob[referenceblob.Length - 1], Equipo);
                        }
                        ApiRest.UpdateEquipo(Equipo);
                        return 2;
                    }
                    else
                        return -1;

                case Accion.Borrar:
                    if (ApiRest.DeleteEquipo(Equipo.IdEquipo))
                    {
                        if(!Equipo.Foto.Equals("/Assets/equipodefecto.jpg"))
                        {
                            string[] referenceblob = Equipo.Foto.Split('/');
                            BlobStorage.EliminarImagen(referenceblob[referenceblob.Length-1], Equipo);
                        }
                        return 3;
                    }
                       
                    else
                        return -1;

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

            switch (Equipo.Categoria)
            {
                case "1º División":
                    ListaEquipos = new ObservableCollection<Equipo>(ApiRest.RescatarEquipos().Where(x => x.Categoria.Equals("1º División")));
                    break;
                case "2º División":
                    ListaEquipos = new ObservableCollection<Equipo>(ApiRest.RescatarEquipos().Where(x => x.Categoria.Equals("2º División")));
                    break;
                case "2ºB División":
                    ListaEquipos = new ObservableCollection<Equipo>(ApiRest.RescatarEquipos().Where(x => x.Categoria.Equals("2ºB División")));
                    break;
                case "3º División":
                    ListaEquipos = new ObservableCollection<Equipo>(ApiRest.RescatarEquipos().Where(x => x.Categoria.Equals("3º División")));
                    break;
                case "Preferente":
                    ListaEquipos = new ObservableCollection<Equipo>(ApiRest.RescatarEquipos().Where(x => x.Categoria.Equals("Preferente")));
                    break;
                case "Fútbol Base":
                    ListaEquipos = new ObservableCollection<Equipo>(ApiRest.RescatarEquipos().Where(x => x.Categoria.Equals("Fútbol Base")));
                    break;
                case "Regional":
                    ListaEquipos = new ObservableCollection<Equipo>(ApiRest.RescatarEquipos().Where(x => x.Categoria.Equals("Regional")).OrderBy(y => y.Nombre));
                    break;
                default:
                    ListaEquipos = new ObservableCollection<Equipo>();
                    break;
            }
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
    }
}
