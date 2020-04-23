using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
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
    /// Lógica de interacción para DeleteArbitroView.xaml
    /// </summary>
    public partial class DeleteArbitroView : Window
    {
        public DeleteArbitroView()
        {
            DataContext = new DeleteArbitroViewModel();
            InitializeComponent();

        }

        private void CategoriaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as DeleteArbitroViewModel).Filtro();
            ArbitrosComboBox.IsEnabled = true;
        }


        private void BorrarCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if((DataContext as DeleteArbitroViewModel).DeleteCanExecute())
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void BorrarCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if ((DataContext as DeleteArbitroViewModel).DeleteExecute())
                {
                    DialogResult = true;
                }
            }
            catch (CRUDException crud)
            {
                MessageBox.Show(crud.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
