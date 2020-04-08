using NombramientoPartidos.ViewModel.JuadoresStaff;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NombramientoPartidos.View.JugadoresStaff
{
    /// <summary>
    /// Lógica de interacción para InsertarJugadorView.xaml
    /// </summary>
    public partial class InsertarJugadorView : Window
    {
        public InsertarJugadorView()
        {
            DataContext = new InsertarJugadorViewModel();
            InitializeComponent();
        }

        private void CategoriasComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as InsertarJugadorViewModel).Filtro();
            EquiposComboBox.IsEnabled = true;
        }

        private void FotoButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as InsertarJugadorViewModel).PonerImagen(ImagenJugador);
        }

        private void InsertCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ((DataContext as InsertarJugadorViewModel).EquipoJugador.IdEquipo!=0 && !string.IsNullOrWhiteSpace((DataContext as InsertarJugadorViewModel).JugadorInsertar.Nombre_Completo) && (DataContext as InsertarJugadorViewModel).FechaNacimiento != null);
        }

        private void InsertCommandExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if((DataContext as InsertarJugadorViewModel).Execute())
            {
                DialogResult = true;
            }
        }
    }
}
