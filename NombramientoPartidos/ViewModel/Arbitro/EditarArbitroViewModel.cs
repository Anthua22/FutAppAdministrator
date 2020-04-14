﻿using NombramientoPartidos.Services;
using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NombramientoPartidos.ViewModel
{
    class EditarArbitroViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<string> Categorias { get; }
        public event PropertyChangedEventHandler PropertyChanged;
        public CollectionViewSource Vista { get; private set; }
        private ObservableCollection<Arbitro> ArbitrosFilter { get; set; }
        string filtro;
       
        public Arbitro ArbitroUpdate { get; set; }
        
        public string Categoria { get; set; }
        
        public EditarArbitroViewModel()
        {
            Categorias = Utils.Categorias;
            ArbitrosFilter = new ObservableCollection<Arbitro>();
            Vista = new CollectionViewSource();
            Vista.Source = ArbitrosFilter;
            Vista.Filter += Vista_Filter;
            
        }

        public void RecuperandoInformacion(string filtro)
        {
            this.filtro = filtro.ToLower();
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

            switch (Categoria)
            {
                case "1º División":
                    ArbitrosFilter = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x => x.Categoria.Equals("1º División")));
                    break;
                case "2º División":
                    ArbitrosFilter = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x => x.Categoria.Equals("2º División")));
                    break;
                case "2ºB División":
                    ArbitrosFilter = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x => x.Categoria.Equals("2ºB División")));
                    break;
                case "3º División":
                    ArbitrosFilter = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x => x.Categoria.Equals("3º División")));
                    break;
                case "Preferente":
                    ArbitrosFilter = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x => x.Categoria.Equals("Preferente")));
                    break;
                case "Fútbol Base":
                    ArbitrosFilter = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x => x.Categoria.Equals("Fútbol Base")));
                    break;
                case "Regional":
                    ArbitrosFilter = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x => x.Categoria.Equals("Regional")).OrderBy(y => y.Nombre_Completo));
                    break;
            }
            Vista.Source = ArbitrosFilter;

        }

        public bool UpdateExecute()
        {
            ArbitroUpdate.Dni = ArbitroUpdate.Dni.ToUpper();
            return ApiRest.UpdateArbitro(ArbitroUpdate);
        }

        public void EditarImagen(Image image, Arbitro e)
        {

            image.Source = new BitmapImage(new Uri(Utils.ObtenerRutaFichero()));
          
        }

        public bool EditarCanExecute()
        {
            return (ArbitroUpdate != null && Categoria != null);
        }


       
    }
}
