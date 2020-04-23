﻿using NombramientoPartidos.Services;
using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NombramientoPartidos.ViewModel
{
    class DeleteArbitroViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<string> Categorias{ get; private set; }
        public ObservableCollection<Arbitro> Arbitros { get; private set; }
        public string Categoria { get; set; }
        public Arbitro ArbitroEliminar { get; set; }


        public DeleteArbitroViewModel()
        {
            Categorias = Utils.Categorias;
            Arbitros = new ObservableCollection<Arbitro>();
            ArbitroEliminar = new Arbitro();
        }

        public void Filtro()
        {
            switch (ArbitroEliminar.Categoria)
            {
                case "1º División":
                    Arbitros = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x => x.Categoria.Equals("1º División")).OrderBy(y=> y.Nombre_Completo));
                    break;
                case "2º División":
                    Arbitros = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x => x.Categoria.Equals("2º División")).OrderBy(y => y.Nombre_Completo));
                    break;
                case "2ºB División":
                    Arbitros = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x => x.Categoria.Equals("2ºB División")).OrderBy(y => y.Nombre_Completo));
                    break;
                case "3º División":
                    Arbitros = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x => x.Categoria.Equals("3º División")).OrderBy(y => y.Nombre_Completo));
                    break;
                case "Preferente":
                    Arbitros = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x => x.Categoria.Equals("Preferente")).OrderBy(y => y.Nombre_Completo));
                    break;
                case "Fútbol Base":
                    Arbitros = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x => x.Categoria.Equals("Fútbol Base")).OrderBy(y => y.Nombre_Completo));
                    break;
                case "Regional":
                    Arbitros = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x => x.Categoria.Equals("Regional")).OrderBy(y => y.Nombre_Completo));
                    break;
            }
        }

        public bool DeleteExecute()
        {
            MessageBoxResult messageresult = MessageBox.Show("Esta seguro de eliminar a " + ArbitroEliminar.Nombre_Completo + " con DNI: " + ArbitroEliminar.Dni + '?', "Advertencia", MessageBoxButton.YesNo,MessageBoxImage.Warning);
            if(messageresult == MessageBoxResult.Yes)
            {
              
                if(!ArbitroEliminar.Foto.Equals("/Assets/equipodefecto.jpg"))
                {
                    string[] blobreference = ArbitroEliminar.Foto.Split('/');
                    BlobStorage.EliminarImagen(blobreference[blobreference.Length - 1], ArbitroEliminar);
                }
                
                ApiRest.DeleteteArbitro(ArbitroEliminar.Id);
                return true;
            }
            return false;
        }

        public bool DeleteCanExecute()
        {
            return (ArbitroEliminar.Categoria!=null);
        }

    }
}