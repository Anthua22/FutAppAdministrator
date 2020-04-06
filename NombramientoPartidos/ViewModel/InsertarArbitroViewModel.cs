using NombramientoPartidos.Services;
using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
using System;
using System.Collections.Generic;
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

        public InsertarArbitroViewModel()
        {
            Categorias = Utils.Categorias;
            ArbitroInsertar = new Arbitro();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Insert(string dni, string contraseña, string nombre_completo, string email, string fecha_nacimiento, string provincia, string localidad, string cp, string catergoria, string telefono)
        {
            ArbitroInsertar = new Arbitro()
            {
                Dni = dni,
                Pass = contraseña,
                Nombre_Completo = nombre_completo,
                Email = email,
                Fecha_Nacimiento = fecha_nacimiento,
                Provincia = provincia,
                Localidad = localidad,
                Cp = cp,
                Categoria = catergoria,
                Telefono = telefono
            };
            if(!Utils.HayCamposVacios(ArbitroInsertar.Dni, ArbitroInsertar.Pass, ArbitroInsertar.Fecha_Nacimiento, ArbitroInsertar.Email, ArbitroInsertar.Provincia, ArbitroInsertar.Cp))
            {
                return ApiRest.InsertArbitro(ArbitroInsertar);
            }
            return false;
        }

        public void PonerImagen(Image imagen)
        {
            imagen.Source = new BitmapImage(new Uri(Utils.ObtnerRutaFichero()));
        }

    }


}
