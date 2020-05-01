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
    class DeleteArbitroViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<string> Categorias{ get; private set; }
        public ObservableCollection<Arbitro> Arbitros { get; private set; }
        public string Categoria { get; set; }
        public Arbitro ArbitroEliminar { get; set; }
        private ObservableCollection<Partido> Partidos { get; set; }

        public DeleteArbitroViewModel()
        {
            Categorias = Utils.Categorias;
            Arbitros = new ObservableCollection<Arbitro>();
            ArbitroEliminar = new Arbitro();
            Partidos = new ObservableCollection<Partido>();
        }

        public void Filtro()
        {
            Arbitros = Utils.FiltroArbitros(ArbitroEliminar.Categoria);
        }

        public bool DeleteExecute()
        {
            MessageBoxResult messageresult = MessageBox.Show("Esta seguro de eliminar a " + ArbitroEliminar.Nombre_Completo + " con DNI: " + ArbitroEliminar.Dni + "?. Los partidos en los que ha participados se verán afectados", "Advertencia", MessageBoxButton.YesNo,MessageBoxImage.Warning);
            if(messageresult == MessageBoxResult.Yes)
            {
                CambiaArbitroDelete();
                if (!ArbitroEliminar.Foto.Equals("/Assets/equipodefecto.jpg"))
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

        private void CambiaArbitroDelete()
        {
            Partidos = new ObservableCollection<Partido>(ApiRest.RescartarPartidos().Where(x => x.ArbitroPrincipal == ArbitroEliminar.Id || x.ArbitroSecundario == ArbitroEliminar.Id || x.Cronometrador == ArbitroEliminar.Id || x.Tercer_Arbitro == ArbitroEliminar.Id));
            for(int i = 0; i < Partidos.Count; i++)
            {
                if(Partidos[i].ArbitroPrincipal == ArbitroEliminar.Id)
                {
                    Partidos[i].ArbitroPrincipal = -1;
                    ApiRest.UpdatePartido(Partidos[i]);
                }else if(Partidos[i].ArbitroSecundario == ArbitroEliminar.Id)
                {
                    Partidos[i].ArbitroSecundario = -1;
                    ApiRest.UpdatePartido(Partidos[i]);
                }else if(Partidos[i].Cronometrador == ArbitroEliminar.Id)
                {
                    Partidos[i].Cronometrador = -1;
                    ApiRest.UpdatePartido(Partidos[i]);
                }
                else
                {
                    Partidos[i].Tercer_Arbitro = -1;
                    ApiRest.UpdatePartido(Partidos[i]);
                }

            }
        }

    }
}
