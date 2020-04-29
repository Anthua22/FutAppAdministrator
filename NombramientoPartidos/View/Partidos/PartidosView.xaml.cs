using NombramientoPartidos.ViewModel.Partidos;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NombramientoPartidos.View.Partidos
{
    /// <summary>
    /// Lógica de interacción para AsignarPartidos.xaml
    /// </summary>
    public partial class PartidosView : UserControl
    {
        public PartidosView()
        {
            DataContext = new PartidosViewModel();
            InitializeComponent();
        }

        private void AsignarPartidoButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as PartidosViewModel).LimpiarCampos();
            (DataContext as PartidosViewModel).AccionAsignarmodificar = Accion.Asignar;
            CRUDGroupBox.Header = "Modificar un Partido";
            AceptarCambiosButton.Content = "Modificar Partido";
            OcultarControlPorAccion();
           
        }

        private void ModificarPartidoButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as PartidosViewModel).AccionAsignarmodificar = Accion.Modificar;
            CRUDGroupBox.Header = "Asignar un Partido";
            AceptarCambiosButton.Content = "Asignar Partido";
            OcultarControlPorAccion();
            (DataContext as PartidosViewModel).LimpiarCampos();
        }

        private void CategoriasCRUDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            OcultarMostrarcontrolesPorCategoria();
            (DataContext as PartidosViewModel).ObtenerEquiposyArbitrosPrincipales();
         
            (DataContext as PartidosViewModel).CambioAccionCategorias();

            (DataContext as PartidosViewModel).ObtenerPartidos();
            EquiposLocalesComboBox.IsEnabled = true;
            ArbitrosPrincipalesComboBox.IsEnabled = true;
        }

        private void ArbitrosPrincipalesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as PartidosViewModel).ObtenerArbitrosSecundarios();
            if((DataContext as PartidosViewModel).AccionCategoriaTrabajo == AccionCategoria.RegionalyPreferente)
            {
                (DataContext as PartidosViewModel).ObtenerCronometradores();
                CronometradoresComboBox.IsEnabled = true;
            }
            ArbitrosSecundariosComboBox.IsEnabled = true;
        }

        private void EquiposLocalesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as PartidosViewModel).ObtenerEquiposVisitantes();
            EquiposVisitantesComboBox.IsEnabled = true;
        }

        private void ArbitrosSecundariosComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as PartidosViewModel).ObtenerCronometradores();
            CronometradoresComboBox.IsEnabled = true;
        }

        private void AceptarCambiosButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int respuesta = (DataContext as PartidosViewModel).Excecute();
                switch (respuesta)
                {
                    case 1:
                        MessageBox.Show("Nuevo partido asignado correctamente", "Insertar", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void CronometradoresComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TercerosArbitrosComboBox.IsEnabled = true;
        }

        private void OcultarMostrarcontrolesPorCategoria()
        {
            if ((DataContext as PartidosViewModel).Categoria != null)
            {
                if ((DataContext as PartidosViewModel).Categoria.Equals("1º División") || (DataContext as PartidosViewModel).Categoria.Equals("2º División"))
                {
                    ArbitrosSecundariosComboBox.Visibility = Visibility.Visible;
                    ArbitrosSecundariosLabel.Visibility = Visibility.Visible;
                    CronometradoresComboBox.Visibility = Visibility.Visible;
                    CronometradoresLabel.Visibility = Visibility.Visible;
                    TercerArbitroLabel.Visibility = Visibility.Visible;
                    TercerosArbitrosComboBox.Visibility = Visibility.Visible;

                }
                else if ((DataContext as PartidosViewModel).Categoria.Equals("2ºB División") || (DataContext as PartidosViewModel).Categoria.Equals("3º División"))
                {
                    ArbitrosSecundariosComboBox.Visibility = Visibility.Visible;
                    ArbitrosSecundariosLabel.Visibility = Visibility.Visible;
                    CronometradoresComboBox.Visibility = Visibility.Visible;
                    CronometradoresLabel.Visibility = Visibility.Visible;
                    TercerArbitroLabel.Visibility = Visibility.Collapsed;
                    TercerosArbitrosComboBox.Visibility = Visibility.Collapsed;
                }
                else if ((DataContext as PartidosViewModel).Categoria.Equals("Preferente") || (DataContext as PartidosViewModel).Categoria.Equals("Regional"))
                {
                    ArbitrosSecundariosComboBox.Visibility = Visibility.Collapsed;
                    ArbitrosSecundariosLabel.Visibility = Visibility.Collapsed;
                    CronometradoresComboBox.Visibility = Visibility.Visible;
                    CronometradoresLabel.Visibility = Visibility.Visible;
                    TercerArbitroLabel.Visibility = Visibility.Collapsed;
                    TercerosArbitrosComboBox.Visibility = Visibility.Collapsed;
                }
                else
                {
                    ArbitrosSecundariosComboBox.Visibility = Visibility.Collapsed;
                    ArbitrosSecundariosLabel.Visibility = Visibility.Collapsed;
                    CronometradoresComboBox.Visibility = Visibility.Collapsed;
                    CronometradoresLabel.Visibility = Visibility.Collapsed;
                    TercerArbitroLabel.Visibility = Visibility.Collapsed;
                    TercerosArbitrosComboBox.Visibility = Visibility.Collapsed;
                }
            }
          
        }

        private void OcultarControlPorAccion()
        {
            if((DataContext as PartidosViewModel).AccionAsignarmodificar == Accion.Modificar)
            {
                EquipoLocalLabel.Visibility = Visibility.Collapsed;
                EquiposLocalesComboBox.Visibility = Visibility.Collapsed;
                EquipoVisitanteLabel.Visibility = Visibility.Collapsed;
                EquiposVisitantesComboBox.Visibility = Visibility.Collapsed;

                EligirPartidoLabel.Visibility = Visibility.Visible;
                PartidosComboBox.Visibility = Visibility.Visible;
                DisputadoLabel.Visibility = Visibility.Visible;
                DisputadoCheckBox.Visibility = Visibility.Visible;
                ResultadoLabel.Visibility = Visibility.Visible;
                ResultadoStackPanel.Visibility = Visibility.Visible;

                AsignarPartidoButton.IsEnabled = true;
                ModificarPartidoButton.IsEnabled = false;

            }
            else if((DataContext as PartidosViewModel).AccionAsignarmodificar == Accion.Asignar)
            {
                EquipoLocalLabel.Visibility = Visibility.Visible;
                EquiposLocalesComboBox.Visibility = Visibility.Visible;
                EquipoVisitanteLabel.Visibility = Visibility.Visible;
                EquiposVisitantesComboBox.Visibility = Visibility.Visible;

                EligirPartidoLabel.Visibility = Visibility.Collapsed;
                PartidosComboBox.Visibility = Visibility.Collapsed;
                DisputadoLabel.Visibility = Visibility.Collapsed;
                DisputadoCheckBox.Visibility = Visibility.Collapsed;
                ResultadoLabel.Visibility = Visibility.Collapsed;
                ResultadoStackPanel.Visibility = Visibility.Collapsed;

                AsignarPartidoButton.IsEnabled = false;
                ModificarPartidoButton.IsEnabled = true;
            }
        }

        private void PartidosComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if((DataContext as PartidosViewModel).Partidos.Count > 0)
            {
                (DataContext as PartidosViewModel).ObtenerDatosPartidoUpdate();
            }
           
        }
    }
}
