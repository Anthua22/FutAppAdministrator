using NombramientoPartidos.Utilidades.ClasesPojos;
using NombramientoPartidos.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Lógica de interacción para Inicio.xaml
    /// </summary>
    public partial class Inicio : Window
    {
        public Inicio(Administrador administrador)
        {
            DataContext = new InicioViewModel(administrador);
            InitializeComponent();
            
        }

        public event PropertyChangingEventHandler PropertyChanging;

        private void CambioDatosCuenta(object sender, RoutedEventArgs e)
        {
            (DataContext as InicioViewModel).Configuracion = new ConfiguracionCuenta((DataContext as InicioViewModel).AdministradorActual);
            (DataContext as InicioViewModel).Configuracion.Owner = this;
            if ((DataContext as InicioViewModel).Configuracion.ShowDialog() == true)
            {
                MessageBox.Show("Datos modificados", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
