using MySql.Data.MySqlClient;
using NombramientoPartidos.View;
using NombramientoPartidos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
