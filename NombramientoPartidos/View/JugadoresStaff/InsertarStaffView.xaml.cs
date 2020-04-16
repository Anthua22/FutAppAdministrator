using NombramientoPartidos.ViewModel.JugadoresStaff;
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

namespace NombramientoPartidos.View.JugadoresStaff
{
    /// <summary>
    /// Lógica de interacción para InsertarStaffView.xaml
    /// </summary>
    public partial class InsertarStaffView : Window
    {
        public InsertarStaffView()
        {
            DataContext = new InsertarStaffViewModel();
            InitializeComponent();
        }

        private void InsertStaff_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ((DataContext as InsertarStaffViewModel).EquipoStaff != null && (DataContext as InsertarStaffViewModel).EquipoStaff.IdEquipo != 0 && !string.IsNullOrWhiteSpace((DataContext as InsertarStaffViewModel).StaffInsert.Nombre_Completo) && (DataContext as InsertarStaffViewModel).StaffInsert.Cargo != null);
        }

        private void InsertStaff_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if ((DataContext as InsertarStaffViewModel).Execute())
                {
                    DialogResult = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void CategoriasComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as InsertarStaffViewModel).Filtro(CategoriasComboBox.SelectedItem as string);
            EquiposComboBox.IsEnabled = true;
        }
    }
}
