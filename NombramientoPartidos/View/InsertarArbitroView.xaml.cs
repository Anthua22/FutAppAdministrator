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
                if((DataContext as InsertarArbitroViewModel).InsertarArbitro(DniTextBox.Text, ContraseñaTextBox.Text, NombreCompletoTextBox.Text ,EmailTextBox.Text, FechaDatePicker.SelectedDate.Value.Year+"-"+FechaDatePicker.SelectedDate.Value.Month+"-"+FechaDatePicker.SelectedDate.Value.Day, ProvinciaTextBox.Text, LocalidadTextBox.Text, int.Parse(CpTextBox.Text), CategoriaComboBox.SelectedItem as string, TelefonoTextBox.Text))
                {
                    DialogResult = true;
                }
            }catch(InsertException ins){
                MessageBox.Show("Error: " + ins.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }
    }
}
