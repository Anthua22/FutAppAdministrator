using NombramientoPartidos.View;
using System.ComponentModel;

namespace NombramientoPartidos.ViewModel
{
    class ArbitroViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ArbitroViewModel()
        {
    
        }

        public bool EditarArbitroClick()
        {
            EditarArbitroView editarArbitroView = new EditarArbitroView();
            return (bool)editarArbitroView.ShowDialog();
        }

        public bool InsertarArbitroClick()
        {
            InsertarArbitroView insertarArbitroView = new InsertarArbitroView();
            return (bool)insertarArbitroView.ShowDialog();
        }

        public bool DeleteArbitroClick()
        {
            DeleteArbitroView deleteArbitroView = new DeleteArbitroView();
            return (bool)deleteArbitroView.ShowDialog();
        }
    }
}
