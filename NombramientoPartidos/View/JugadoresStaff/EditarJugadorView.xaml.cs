using NombramientoPartidos.ViewModel.JuadoresStaff;
using System;
using System.Windows;
using System.Windows.Controls;

namespace NombramientoPartidos.View.JugadoresStaff
{
    /// <summary>
    /// Lógica de interacción para EditarJugadorView.xaml
    /// </summary>
    public partial class EditarJugadorView : Window
    {
        public EditarJugadorView()
        {
            DataContext = new EditarJugadorViewModel();
            InitializeComponent();
        }

        private void CategoriasCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as EditarJugadorViewModel).FiltroEquipos(CategoriasCombobox.SelectedItem as string);
            EquiposComboBox.IsEnabled = true;
            DatosJugadorBorder.Visibility = Visibility.Hidden;
        }

        private void EquiposComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as EditarJugadorViewModel).Jugadores.Clear();
            (DataContext as EditarJugadorViewModel).FiltroJugadores();
            JugadoresComboBox.IsEnabled = true;    
        }

        private void JugadoresComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as EditarJugadorViewModel).EquipoCambio = (DataContext as EditarJugadorViewModel).EquipoJugador;
            DatosJugadorBorder.Visibility = Visibility.Visible;
        }

        private void EditarJugador_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute =  (DataContext as EditarJugadorViewModel).JugadorUpdate != null;
        }

        private void EditarJugadorExecuted(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            try
            {
                if((DataContext as EditarJugadorViewModel).Update())
                {
                    DialogResult = true;
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
