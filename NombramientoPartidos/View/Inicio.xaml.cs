using NombramientoPartidos.Utilidades.ClasesPojos;
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
    public partial class Inicio : Window, INotifyPropertyChanged
    {
        public Administrador AdministradorActual { get; set; }
        public Inicio(Administrador administrador)
        {
          
            InitializeComponent();
            AdministradorActual = administrador;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void CambioDatosCuenta(object sender, RoutedEventArgs e)
        {
            ConfiguracionCuenta configuracion = new ConfiguracionCuenta(AdministradorActual);
            configuracion.Owner = this;
            if(configuracion.ShowDialog() == true)
            {
                MessageBox.Show("Datos Cambiados", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
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
