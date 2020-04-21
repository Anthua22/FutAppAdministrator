using NombramientoPartidos.Utilidades;
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
    /// Lógica de interacción para InsertarArbitroView.xaml
    /// </summary>
    public partial class InsertarArbitroView : Window
    {
        public InsertarArbitroView()
        {
            DataContext = new InsertarArbitroViewModel();
            InitializeComponent();
        }

 
      
        private void FotoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (DataContext as InsertarArbitroViewModel).PonerImagen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void InsertArbitro_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrWhiteSpace((DataContext as InsertarArbitroViewModel).ArbitroInsertar.Dni) && !string.IsNullOrWhiteSpace((DataContext as InsertarArbitroViewModel).ArbitroInsertar.Pass) && !string.IsNullOrWhiteSpace((DataContext as InsertarArbitroViewModel).ArbitroInsertar.Email) && !string.IsNullOrWhiteSpace((DataContext as InsertarArbitroViewModel).ArbitroInsertar.Provincia) && !string.IsNullOrWhiteSpace((DataContext as InsertarArbitroViewModel).ArbitroInsertar.Cp);
        }

        private void InsertArbitro_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if ((DataContext as InsertarArbitroViewModel).Insert())
                {
                    DialogResult = true;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
