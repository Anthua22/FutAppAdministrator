using NombramientoPartidos.Utilidades;
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
    /// Lógica de interacción para InsertarArbitroView.xaml
    /// </summary>
    public partial class InsertarArbitroView : Window
    {
        public InsertarArbitroView()
        {
            DataContext = new InsertarArbitroViewModel();
            InitializeComponent();
        }

        private void AceptarButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if((DataContext as InsertarArbitroViewModel).Insert())
                {
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Hay algunos campos requiros vacíos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    CambioCalores();

                }
               
            }
        
            catch (Exception ex)
            {
                if(ex.Message.Equals("El objeto que acepta valores Null debe tener un valor."))
                {
                    MessageBox.Show("Hay algunos campos requiros vacíos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    CambioCalores();
                }
                else
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }   
                
            }
           
        }
        private void CambioCalores()
        {
            DniTextBox.BorderBrush = Brushes.Red;
            ContraseñaTextBox.BorderBrush = Brushes.Red;
            ProvinciaTextBox.BorderBrush = Brushes.Red;
            CpTextBox.BorderBrush = Brushes.Red;
            EmailTextBox.BorderBrush = Brushes.Red;
            CategoriaComboBox.BorderThickness = new Thickness(2);
            CategoriaComboBox.BorderBrush = Brushes.Red;
            FechaDatePicker.BorderBrush = Brushes.Red;
        }

        private void FotoButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as InsertarArbitroViewModel).PonerImagen(ImagenArbitroImage);
        }
    }
}
