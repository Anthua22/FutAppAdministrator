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

        private void AceptarButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if((DataContext as EditarArbitroViewModel).Update(ListaArbitrosDataGrid.SelectedItem))
                {
                    DialogResult = true;
                }
                
            }catch(PassException pas)
            {
                MessageBox.Show("Error: " + pas.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (EmailException em)
            {
                MessageBox.Show("Error: " + em.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(FechaException fe)
            {
                MessageBox.Show("Error: " + fe.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(CategoriaException cat)
            {
                MessageBox.Show("Error: " + cat.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(CRUDException exp)
            {
                MessageBox.Show("Error: " + exp.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void CategoriaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                (this.DataContext as EditarArbitroViewModel).FiltroCategoria(CategoriaComboBox.SelectedItem as string);
                
                FiltroNombreTextBox.IsEnabled = true;
                ListaArbitrosDataGrid.Visibility = Visibility.Visible;
            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void SeleccionImagenElementoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FiltarButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as EditarArbitroViewModel).RecuperandoInformacion(FiltroNombreTextBox);
            (DataContext as EditarArbitroViewModel).Vista.View.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as EditarArbitroViewModel).EditarImagen(FotoArbitroImage, (Arbitro)(ListaArbitrosDataGrid.SelectedItem));
        }

        private void ListaArbitrosDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as EditarArbitroViewModel).FiltroCategoria(sender as string);
        }

        private void EditarCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void EditarCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (DataContext as EditarArbitroViewModel).Update(ListaArbitrosDataGrid.SelectedItem);
        }
    }
}
