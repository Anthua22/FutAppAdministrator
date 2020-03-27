using NombramientoPartidos.Services;
using NombramientoPartidos.Utilidades.ClasesPojos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NombramientoPartidos.ViewModel
{
    class EditarArbitroViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<string> Categorias { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Arbitro> ArbitrosFilter { get; set; }

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
        }
    }
}
