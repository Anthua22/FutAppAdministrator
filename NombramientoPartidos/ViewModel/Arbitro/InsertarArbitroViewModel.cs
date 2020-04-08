using NombramientoPartidos.Services;
using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NombramientoPartidos.ViewModel
{
    class InsertarArbitroViewModel:INotifyPropertyChanged
    {
        public Arbitro ArbitroInsertar { get; set; }
        public ObservableCollection<string> Categorias { get; }
        public DateTime FechaNacimiento { get; set; }

        public InsertarArbitroViewModel()
        {
            Categorias = Utils.Categorias;
            ArbitroInsertar = new Arbitro();
            FechaNacimiento = DateTime.Now;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Insert()
        {
            ArbitroInsertar.Fecha_Nacimiento = FechaNacimiento.Year + "-" + FechaNacimiento.Month + "-" + FechaNacimiento.Day;
            if(!Utils.HayCamposVacios(ArbitroInsertar.Dni, ArbitroInsertar.Pass, ArbitroInsertar.Fecha_Nacimiento, ArbitroInsertar.Email, ArbitroInsertar.Provincia, ArbitroInsertar.Cp))
            {
                return ApiRest.InsertArbitro(ArbitroInsertar);
            }
            return false;
        }

        public void PonerImagen(Image imagen)
        {
            imagen.Source = new BitmapImage(new Uri(Utils.ObtenerRutaFichero()));
        }

    }


}
