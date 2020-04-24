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
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void EntrarCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
           // e.CanExecute = UsernameTextBlock.Text.Length > 0 && PasswordPasswordBox.Password.Length > 0;
        }

        private void EntrarCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
