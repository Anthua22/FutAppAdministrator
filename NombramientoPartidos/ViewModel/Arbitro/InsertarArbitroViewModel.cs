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
        public ObservableCollection<string> Provincias { get; }
        public InsertarArbitroViewModel()
        {
            Categorias = Utils.Categorias;
            Provincias = Utils.Provincias;
            ArbitroInsertar = new Arbitro()
            {
                Foto = "/Assets/defecto.jpg"
            };
            FechaNacimiento = DateTime.Now;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Insert()
        {
            ArbitroInsertar.Fecha_Nacimiento = FechaNacimiento.Year + "-" + FechaNacimiento.Month + "-" + FechaNacimiento.Day;
            ArbitroInsertar.Dni = ArbitroInsertar.Dni.ToUpper();
            if (!ArbitroInsertar.Foto.Equals("/Assets/defecto.jpg"))
            {
                string[] rutaimagen = ArbitroInsertar.Foto.Split('/');
                string urlImagen = BlobStorage.GuardarImagen(ArbitroInsertar.Foto, rutaimagen[rutaimagen.Length - 1], ArbitroInsertar);
                ArbitroInsertar.Foto = urlImagen;
            }
         
            return ApiRest.InsertArbitro(ArbitroInsertar);
          
        }

        public void PonerImagen()
        {      
            string ruta = Utils.ObtenerRutaFichero().Replace('\\', '/');
            if(!string.IsNullOrWhiteSpace(ruta))
            {
                ArbitroInsertar.Foto = ruta;
            }
            
        }



    }


}
