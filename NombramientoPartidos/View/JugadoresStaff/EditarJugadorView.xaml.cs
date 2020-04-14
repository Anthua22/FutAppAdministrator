using NombramientoPartidos.ViewModel.JuadoresStaff;
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
            (DataContext as EditarJugadorViewModel).Equipos = new System.Collections.ObjectModel.ObservableCollection<Utilidades.ClasesPojos.Equipo>();
            (DataContext as EditarJugadorViewModel).EquipoJugador = new Utilidades.ClasesPojos.Equipo();
            (DataContext as EditarJugadorViewModel).FiltroEquipos(CategoriasCombobox.SelectedItem as string);
            EquiposComboBox.IsEnabled = true;
        }

        private void EquiposComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as EditarJugadorViewModel).Jugadores.Clear();
            (DataContext as EditarJugadorViewModel).FiltroJugadores();
            JugadoresComboBox.IsEnabled = true;    
        }

        private void JugadoresComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DatosJugadorBorder.Visibility = Visibility.Visible;
        }
    }
}
