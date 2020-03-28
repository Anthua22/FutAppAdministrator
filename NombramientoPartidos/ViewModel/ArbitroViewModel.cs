using NombramientoPartidos.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
