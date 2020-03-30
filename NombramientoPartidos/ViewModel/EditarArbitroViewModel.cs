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
        public CollectionViewSource Vista { get; set; }
        private ObservableCollection<Arbitro> ArbitrosFilter { get; set; }
        string filtro;
        private Arbitro ArbitroUpdate { get; set; } 

        public string Categoria { get; set; }
        public EditarArbitroViewModel()
        {
            Categorias = new ObservableCollection<string>
            {
                "1º División",
                "2º División",
                "2ºB División",
                "3º División",
                "Preferente",
                "Fútbol Base"
            };
            ArbitrosFilter = new ObservableCollection<Arbitro>();
            Vista = new CollectionViewSource();
            Vista.Filter += Vista_Filter;
            Vista.Source = ArbitrosFilter;
        }

        public void RecuperandoInformacion(System.Windows.Controls.TextBox textBox)
        {
            filtro = textBox.Text;
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
                  if (item.Nombre_Completo.Contains(filtro) || item.Dni.Contains(filtro))
                  {
                      e.Accepted = true;
                  }
                  else
                  {
                      e.Accepted = false;
                  }
              }
        }

        public void FiltroCategoria(string categoria)
        {

            switch (categoria)
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
            }
            Vista.Source = ArbitrosFilter;

        }

        public bool Update(object e)
        {
            ArbitroUpdate = e as Arbitro;
            return ApiRest.UpdateArbitro(ArbitroUpdate);
        }

        public void EditarImagen(Image image, Arbitro e)
        {
            OpenFileDialog dialogoImagen = new OpenFileDialog();

            dialogoImagen.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            dialogoImagen.Filter = "Imágenes JPG (*.jpg)|*.jpg|Imágenes PNG (*.png)|*.png|Todos los archivos (*.*)|*.*";

            dialogoImagen.FilterIndex = 3;

            dialogoImagen.RestoreDirectory = true;

            if (dialogoImagen.ShowDialog() == DialogResult.OK)
            {

                string ruta = dialogoImagen.FileName;
                image.Source = new BitmapImage(new Uri(ruta));
                e.Foto = Utils.ImagenABytes(ruta);
                
            }
        }

        public void ColocarImagen(Arbitro arbitro, Image image)
        {
            image = Utils.ObtenerBitmap(arbitro)
        }
        
    }
}
