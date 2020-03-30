using NombramientoPartidos.View;
using System.ComponentModel;

namespace NombramientoPartidos.ViewModel
{
    class ArbitroViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool EditarArbitroClick()
        {
            EditarArbitroView editarArbitroView = new EditarArbitroView();
            return (bool)editarArbitroView.ShowDialog();
          
        }
    }
}
