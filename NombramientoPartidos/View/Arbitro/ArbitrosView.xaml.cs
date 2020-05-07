using NombramientoPartidos.ViewModel;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes;
using MaterialDesignThemes.Wpf;
using System.Threading.Tasks;

namespace NombramientoPartidos.View
{
    /// <summary>
    /// Lógica de interacción para Arbitros.xaml
    /// </summary>
    public partial class Arbitros : UserControl
    {
        public string Mensaje { get; set; }
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
                Mensaje = "Nuevo Árbitro insertado";
            }
        }

        private  void ModificarArbitroButton_Click(object sender, RoutedEventArgs e)
        {

            if((this.DataContext as ArbitroViewModel).EditarArbitroClick())
            {
                MessageBox.Show("Arbitro Actualizado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
               /* Mensaje = "Arbitro Actualizado";
                StackPanel d = new StackPanel() { Height = 50 };
                TextBlock f = new TextBlock() { Text = "Árbitro Actualizado" , Margin= new Thickness(10,5,10,0)};
                Button button = new Button() { Content = "Aceptar", Width=80 };
                d.Children.Add(f);
                d.Children.Add(button);
                DialogoError.ShowDialog(d);*/
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
