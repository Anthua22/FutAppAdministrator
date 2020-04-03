using NombramientoPartidos.Services;
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
    class DeleteViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<string> Categorias{ get; private set; }
        public ObservableCollection<Arbitro> Arbitros { get; private set; }

        public DeleteViewModel()
        {
            Categorias = Utils.Categorias;
            Arbitros = new ObservableCollection<Arbitro>();
        }

        public void Filtro(string categoria)
        {
            switch (categoria)
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
            }
        }

        public bool Delete(Arbitro arbitro)
        {
            MessageBoxResult messageresult = MessageBox.Show("Esta seguro de eliminar a " + arbitro.Nombre_Completo + " con DNI: " + arbitro.Dni + '?', "Advertencia", MessageBoxButton.YesNo,MessageBoxImage.Warning);
            if(messageresult == MessageBoxResult.Yes)
            {
                ApiRest.DeleteteArbitro(arbitro.Id);
                return true;
            }
            return false;
        }

    }
}
