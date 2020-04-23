using NombramientoPartidos.ViewModel.JugadoresStaff;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NombramientoPartidos.View.JugadoresStaff
{
    /// <summary>
    /// Lógica de interacción para BorrarStaffView.xaml
    /// </summary>
    public partial class BorrarStaffView : Window
    {
        public BorrarStaffView()
        {
            DataContext = new BorrarStaffViewModel();
            InitializeComponent();
        }

        private void CategoriasComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as BorrarStaffViewModel).Filtro(CategoriasComboBox.SelectedItem as string);
            EquiposComboBox.IsEnabled = true;
        }

        private void EquiposComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as BorrarStaffViewModel).Staffs.Clear();
            (DataContext as BorrarStaffViewModel).FiltroStaff();
            StaffsComboBox.IsEnabled = true;
        }

        private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (DataContext as BorrarStaffViewModel).StaffDelete.Nombre_Completo != null;
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if ((DataContext as BorrarStaffViewModel).DeleteExecute())
                {
                    DialogResult = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
