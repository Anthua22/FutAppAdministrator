using NombramientoPartidos.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace NombramientoPartidos.View
{
    /// <summary>
    /// Lógica de interacción para Arbitros.xaml
    /// </summary>
    public partial class Arbitros : UserControl
    {
        public Arbitros()
        {
            this.DataContext = new ArbitroViewModel();
            InitializeComponent();
        }

        private void AgregarArbitroButton_Click(object sender, RoutedEventArgs e)
        {
        
            if((DataContext as ArbitroViewModel).InsertarArbitroClick())
            {
                MessageBox.Show("Nuevo Árbitro insertado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ModificarArbitroButton_Click(object sender, RoutedEventArgs e)
        {

            if((this.DataContext as ArbitroViewModel).EditarArbitroClick())
            {
                MessageBox.Show("Arbitro Actualizado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void EliminarArbitroButton_Click(object sender, RoutedEventArgs e)
        {
            if((DataContext as ArbitroViewModel).DeleteArbitroClick())
            {
                MessageBox.Show("Arbitro Eliminado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
