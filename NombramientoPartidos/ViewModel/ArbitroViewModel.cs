using NombramientoPartidos.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NombramientoPartidos.ViewModel
{
    class ArbitroViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void EditarArbitroClick()
        {
            EditarArbitroView editarArbitroView = new EditarArbitroView();
            editarArbitroView.ShowDialog();
        }
    }
}
