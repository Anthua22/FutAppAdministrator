using NombramientoPartidos.Services;
using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace NombramientoPartidos.ViewModel
{
    class EditarArbitroViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<string> Categorias { get; }
        public ObservableCollection<string> CategoriasNueva { get; }
        public event PropertyChangedEventHandler PropertyChanged;
        public CollectionViewSource Vista { get; private set; }
        public string  Categoria { get; set; }
        public ObservableCollection<string> Provincias { get; set; }
        string filtro;
        string fotoantigua;
       
        public Arbitro ArbitroUpdate { get; set; }
        
    
        
        public EditarArbitroViewModel()
        {
            Categorias = Utils.Categorias;
            CategoriasNueva = Utils.Categorias;
            Provincias = Utils.Provincias;
            Vista = new CollectionViewSource();
            Vista.Source = new ObservableCollection<Arbitro>();
            Vista.Filter += Vista_Filter;
            ArbitroUpdate = new Arbitro();
        }

        public void RecuperandoInformacion(string filtro)
        {
            this.filtro = filtro;
        }

        private void Vista_Filter(object sender, FilterEventArgs e)
        {
            Arbitro item = (Arbitro)e.Item;
            
              if (string.IsNullOrWhiteSpace(filtro))
              {
                  e.Accepted = true;
              }
              else
              {
                  if (item.Nombre_Completo.Contains(filtro) || item.Dni.Equals(filtro))
                  {
                      e.Accepted = true;
                  }
                  else
                  {
                      e.Accepted = false;
                  }
              }
        }

        public void FiltroCategoria()
        {
            Vista.Source = Utils.FiltroArbitros(Categoria);
        }

        public bool UpdateExecute()
        {
            if (!ArbitroUpdate.Foto.Equals("/Assets/defecto.jpg") && !ArbitroUpdate.Foto.Contains("http"))
            {
                string[] urlBlob = ArbitroUpdate.Foto.Split('/');
                BlobStorage.EliminarImagen(fotoantigua, ArbitroUpdate);
                ArbitroUpdate.Foto = BlobStorage.GuardarImagen(ArbitroUpdate.Foto, urlBlob[urlBlob.Length - 1], ArbitroUpdate);
            }
            ArbitroUpdate.Dni = ArbitroUpdate.Dni.ToUpper();

            return ApiRest.UpdateArbitro(ArbitroUpdate);
        }

        public void EditarImagen()
        {
            string ruta = Utils.ObtenerRutaFichero().Replace('\\', '/');
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                if (!ArbitroUpdate.Foto.Equals("/Assets/defecto.jpg"))
                {
                    string[] urlBlob = ArbitroUpdate.Foto.Split('/');

                    fotoantigua = urlBlob[urlBlob.Length - 1];
                }
                ArbitroUpdate.Foto = ruta;
            }
          
        }

        public bool EditarCanExecute()
        {
            return ArbitroUpdate!=null && ArbitroUpdate.Categoria != null ;
        }


       
    }
}
