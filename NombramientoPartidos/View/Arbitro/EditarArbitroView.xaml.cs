using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
using NombramientoPartidos.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            ListaArbitrosDataGrid.DataContext = (DataContext as EditarArbitroViewModel).Vista;
        }


        private void CategoriaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                (this.DataContext as EditarArbitroViewModel).FiltroCategoria();
                
                FiltroNombreTextBox.IsEnabled = true;
                ListaArbitrosDataGrid.Visibility = Visibility.Visible;
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }


        private void FiltarButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as EditarArbitroViewModel).RecuperandoInformacion(FiltroNombreTextBox.Text);
            (DataContext as EditarArbitroViewModel).Vista.View.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (DataContext as EditarArbitroViewModel).EditarImagen(FotoArbitroImage, (Arbitro)(ListaArbitrosDataGrid.SelectedItem));
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message+"\nNo se ha elegido ninguna imagen", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void EditarCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
           
            if ((DataContext as EditarArbitroViewModel).EditarCanExecute())
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
          
        }

        private void EditarCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if((DataContext as EditarArbitroViewModel).UpdateExecute())
                {
                    DialogResult = true;
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void ListaArbitrosDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as EditarArbitroViewModel).ArbitroUpdate = ListaArbitrosDataGrid.SelectedItem as Arbitro;
        }
    }
}
