using NombramientoPartidos.Utilidades.ClasesPojos;
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
using System.Windows.Shapes;

namespace NombramientoPartidos.View
{
    /// <summary>
    /// Lógica de interacción para ConfiguracionCuenta.xaml
    /// </summary>
    public partial class ConfiguracionCuenta : Window
    {

        public ConfiguracionCuenta(Administrador administrador)
        {

            DataContext = new ConfiguracionCuentaViewModel(administrador);
            InitializeComponent();
        }

        private void AceptarButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (DataContext as ConfiguracionCuentaViewModel).ContraseñaActual = ContraseñaActualPassword.Password;
                (DataContext as ConfiguracionCuentaViewModel).ContraseñaConfirmar = ContraseñaConfirmarPassword.Password;
                (DataContext as ConfiguracionCuentaViewModel).ContraseñaNueva = NuevaContraseñaPassword.Password;
                if((DataContext as ConfiguracionCuentaViewModel).UpdateAdministrador())
                {
                    DialogResult = true;
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)CambioContraseñaCheckBox.IsChecked)
            {
                ContraseñaActualTextBlock.Visibility = Visibility.Visible;
                ContraseñaActualPassword.Visibility = Visibility.Visible;
                ContraseñaConfirmarPassword.Visibility = Visibility.Visible;
                ContraseñaConfirmarTextBlock.Visibility = Visibility.Visible;
                ContraseñaNuevaTextBlock.Visibility = Visibility.Visible;
                NuevaContraseñaPassword.Visibility = Visibility.Visible;
                (DataContext as ConfiguracionCuentaViewModel).Accion = AccionAdministrar.ContraseñaCambiada;
            }
            else 
            {
                ContraseñaActualTextBlock.Visibility = Visibility.Collapsed;
                ContraseñaActualPassword.Visibility = Visibility.Collapsed;
                ContraseñaConfirmarPassword.Visibility = Visibility.Collapsed;
                ContraseñaConfirmarTextBlock.Visibility = Visibility.Collapsed;
                ContraseñaNuevaTextBlock.Visibility = Visibility.Collapsed;
                NuevaContraseñaPassword.Visibility = Visibility.Collapsed;
                (DataContext as ConfiguracionCuentaViewModel).Accion = AccionAdministrar.SinContraseñaCambiada;
            }
        }
    }
}
