    using NombramientoPartidos.ViewModel.JuadoresStaff;
using System.Windows;
using System.Windows.Controls;

namespace NombramientoPartidos.View.JugadoresStaff
{
    /// <summary>
    /// Lógica de interacción para JugadoresStaffView.xaml
    /// </summary>
    public partial class JugadoresStaffView : UserControl
    {
        public JugadoresStaffView()
        {
            DataContext = new JugadoresStaffViewModel();
            InitializeComponent();
        }

      

        private void AddJugadorButton_Click(object sender, RoutedEventArgs e)
        {
            if((DataContext as JugadoresStaffViewModel).InsertJugador())
            {
                MessageBox.Show("Nuevo Jugador Añadido", "Información",MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void EditJugadorButton_Click(object sender, RoutedEventArgs e)
        {
            if((DataContext as JugadoresStaffViewModel).UpdateJugador())
            {
                MessageBox.Show("Jugador Modificado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteJugadorButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as JugadoresStaffViewModel).DeleteJugador();
        }
    }
}
