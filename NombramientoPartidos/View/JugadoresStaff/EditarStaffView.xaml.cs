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
    /// Lógica de interacción para EditarStaffView.xaml
    /// </summary>
    public partial class EditarStaffView : Window
    {
        public EditarStaffView()
        {
            DataContext = new EditarStaffViewModel();
            InitializeComponent();
        }

        private void CategoriasCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                (DataContext as EditarStaffViewModel).FiltroEquipos(CategoriasCombobox.SelectedItem as string);
                EquiposComboBox.IsEnabled = true;
                DatosStaffBorder.Visibility = Visibility.Hidden;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void EquiposComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                (DataContext as EditarStaffViewModel).Staffs.Clear();
                (DataContext as EditarStaffViewModel).FiltroStaffs();
                StaffsComboBox.IsEnabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
          
        }

        private void StaffsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as EditarStaffViewModel).EquipoCambio = (DataContext as EditarStaffViewModel).EquipoStaff;
            DatosStaffBorder.Visibility = Visibility.Visible;
        }

        private void UpdateStaff_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (DataContext as EditarStaffViewModel).StaffUpdate != null;
        }

        private void UpdateStaff_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if ((DataContext as EditarStaffViewModel).Update())
                {
                    DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

       
        private void FotoStaffButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (DataContext as EditarStaffViewModel).CambiarFoto();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }
    }
}
