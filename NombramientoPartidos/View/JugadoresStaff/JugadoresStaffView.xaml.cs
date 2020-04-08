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

        private void AddStaffButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void AddJugadorButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as JugadoresStaffViewModel).InsertJugador();
        }
    }
}
