using NombramientoPartidos.View;
using NombramientoPartidos.ViewModel;
using System;
using System.Windows;

namespace NombramientoPartidos
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = new MainWindowViewModel();
            InitializeComponent();
          
        }
        private void EntrarButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((this.DataContext as MainWindowViewModel).Entrar(UsernameTextBlock.Text, PasswordPasswordBox.Password))
                {
                   
                    Inicio inicio = new Inicio();
                    inicio.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
