using NombramientoPartidos.ViewModel.Equipos;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NombramientoPartidos.View.Equipo
{
    /// <summary>
    /// Lógica de interacción para EquiposView.xaml
    /// </summary>
    public partial class EquiposView : UserControl
    {
        public EquiposView()
        {
            DataContext = new EquiposViewModel(Accion.Nuevo);
            InitializeComponent();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            int respuesta = ((EquiposViewModel)this.DataContext).Save_Execute();

            switch (respuesta)
            {
                case -1:
                    MessageBox.Show("No se ha podido realizar la acción, asegurese de haber escogido un equipo o haber rellenado los todos los campos requiridos", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

                    break;

                case 0:
                    MessageBox.Show("Acción cancelada", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;

                case 1:
                    MessageBox.Show("Nuevo equipo creado correctamente", "Insertar", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;

                case 2:
                    MessageBox.Show("Equipo modificado correctamente", "Editar", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;

                case 3:
                    MessageBox.Show("Equipo eliminado correctamente", "Eliminar", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;

            }
            ControlCombosBox();
            ((EquiposViewModel)this.DataContext).LimpiaCampos();
            ImagenEquipoImage.Source = null;
         
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (((EquiposViewModel)DataContext).Accion == Accion.Nuevo)
            {
                e.CanExecute = (((EquiposViewModel)DataContext).Equipo.Categoria != null && !string.IsNullOrWhiteSpace((DataContext as EquiposViewModel).Equipo.Nombre) && !string.IsNullOrWhiteSpace((DataContext as EquiposViewModel).Equipo.Provincia));
            }
            else
            {
                e.CanExecute = ((EquiposViewModel)DataContext).Equipo != null;
            }
        }

        private void SeleccionImagenEquipoButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as EquiposViewModel).ElegirFoto(ImagenEquipoImage);
        }

        private void AñadirEquipoButton_Click(object sender, RoutedEventArgs e)
        {
            ControlCombosBox();
            CRUDGroupBox.Header = "Añadir Equipo";
            AceptarCambiosButton.Content = "Añadir Equipo";   
            ((EquiposViewModel)this.DataContext).CambiaAccion(Accion.Nuevo);
            OcultarElementos((DataContext as EquiposViewModel).Accion);
 
        }

        private void ModificarEquipoButton_Click(object sender, RoutedEventArgs e)
        {
            ControlCombosBox(); 
            CRUDGroupBox.Header = "Modificar Equipo";
            AceptarCambiosButton.Content = "Modificar Equipo";
            ((EquiposViewModel)this.DataContext).CambiaAccion(Accion.Editar);
            OcultarElementos((DataContext as EquiposViewModel).Accion);
           
        }

        private void EliminarEquipoButton_Click(object sender, RoutedEventArgs e)
        {
            ControlCombosBox();
            CRUDGroupBox.Header = "Eliminar Equipo";
            AceptarCambiosButton.Content = "Eliminar Equipo";
            ((EquiposViewModel)this.DataContext).CambiaAccion(Accion.Borrar);
            OcultarElementos((DataContext as EquiposViewModel).Accion);
            EquiposComboBox.IsEnabled = false;
               
            
        }

        private void ControlCombosBox()
        {
            CategoriasCRUDComboBox.SelectedItem = null;
            EquiposComboBox.IsEnabled = false;
            ImagenEquipoImage.Source = null;

        }
        private void OcultarElementos(Accion accion)
        {
            switch (accion)
            {
                case Accion.Nuevo:
                    EquiposComboBox.Visibility = Visibility.Collapsed;
                    SeleccionEquipoLabel.Visibility = Visibility.Collapsed;
                    NombreEquipoLabel.Visibility = Visibility.Visible;
                    NombreEquipoTextBox.Visibility = Visibility.Visible;
                    ProvinciaLabel.Visibility = Visibility.Visible;
                    ProvinciaEquipoTextBox.Visibility = Visibility.Visible;
                    DireccionCampoTextBox.Visibility = Visibility.Visible;
                    DireccionLabel.Visibility = Visibility.Visible;
                    EscudoLabel.Visibility = Visibility.Visible;
                    SeleccionImagenEquipoButton.Visibility = Visibility.Visible;
                    break;
                case Accion.Editar:
                    EquiposComboBox.Visibility = Visibility.Visible;
                    SeleccionEquipoLabel.Visibility = Visibility.Visible;
                    NombreEquipoLabel.Visibility = Visibility.Visible;
                    NombreEquipoTextBox.Visibility = Visibility.Visible;
                    ProvinciaLabel.Visibility = Visibility.Visible;
                    ProvinciaEquipoTextBox.Visibility = Visibility.Visible;
                    DireccionCampoTextBox.Visibility = Visibility.Visible;
                    DireccionLabel.Visibility = Visibility.Visible;
                    EscudoLabel.Visibility = Visibility.Visible;
                    SeleccionImagenEquipoButton.Visibility = Visibility.Visible;
                    break;
                case Accion.Borrar:
                    EquiposComboBox.Visibility = Visibility.Visible;
                    SeleccionEquipoLabel.Visibility = Visibility.Visible;
                    NombreEquipoLabel.Visibility = Visibility.Hidden;
                    NombreEquipoTextBox.Visibility = Visibility.Hidden;
                    ProvinciaLabel.Visibility = Visibility.Hidden;
                    ProvinciaEquipoTextBox.Visibility = Visibility.Hidden;
                    DireccionCampoTextBox.Visibility = Visibility.Hidden;
                    DireccionLabel.Visibility = Visibility.Hidden;
                    EscudoLabel.Visibility = Visibility.Hidden;
                    SeleccionImagenEquipoButton.Visibility = Visibility.Hidden;
                    break;
            }
           
        }

        private void CategoriasCRUDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as EquiposViewModel).FiltroCategoria(CategoriasCRUDComboBox.SelectedItem as string);
            EquiposComboBox.IsEnabled = true;
        }
    }
}
