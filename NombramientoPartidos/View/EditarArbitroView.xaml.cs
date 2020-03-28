using NombramientoPartidos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NombramientoPartidos.View
{
    /// <summary>
    /// Lógica de interacción para EditarArbitroView.xaml
    /// </summary>
    public partial class EditarArbitroView : Window
    {
        public EditarArbitroView()
        {
            this.DataContext = new EditarArbitroViewModel();
            InitializeComponent();
        }

        private void AceptarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CategoriaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (this.DataContext as EditarArbitroViewModel).FiltroCategoria(CategoriaComboBox.SelectedItem as string);
            ArbitroComboBox.IsEnabled = true;
            ListaArbitrosDataGrid.IsEnabled = true;
        }

        private void SeleccionImagenElementoButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
