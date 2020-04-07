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
       // public string Categoria { get; set; }
        public ObservableCollection<string> Categorias { get; set; }
        public ObservableCollection<Equipo> ListaEquipos { get; private set; }
        public Accion Accion { get; private set; }

        public EquiposViewModel(Accion accion)
        {
            this.Accion = accion;

            if(this.Accion == Accion.Nuevo)
            {
                Equipo = new Equipo();
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
                            ApiRest.InsertEquipo(Equipo);
                            return 1;
                        }
                        else return -1;

                    }
                    else return -1;

                case Accion.Editar:
                    if (!string.IsNullOrWhiteSpace(Equipo.Nombre) && !string.IsNullOrWhiteSpace(Equipo.Provincia) && Equipo.Categoria != null)
                    {     
                        ApiRest.UpdateEquipo(Equipo);
                        return 2;
                    }
                    else
                        return -1;

                case Accion.Borrar:
                    if (ApiRest.DeleteEquipo(Equipo.IdEquipo))
                        return 3;
                    else
                        return -1;

                default:
                    return 0;
            }

            
        }

        public void CambiaAccion(Accion accion)
        {
            if (accion == Accion.Editar || accion == Accion.Borrar)
            {
                ListaEquipos = null;
          

            }

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
            }
        }

        public void LimpiaCampos()
        { 
            Equipo = new Equipo();
   
        }

        public void ElegirFoto(Image image)
        {
            image.Source = new BitmapImage(new Uri(Utils.ObtnerRutaFichero()));
        }
    }
}
