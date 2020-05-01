using NombramientoPartidos.ViewModel.Equipos;
using NombramientoPartidos.ViewModel.Partidos;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            DireccionTextBox.IsEnabled = true;
            FechaEncuentro.IsEnabled = true;
            LocalidadTextBox.IsEnabled = true;
            HoraEncuentroStackPanel.IsEnabled = true;
            (DataContext as PartidosViewModel).AccionAsignarmodificar = Accion.Nuevo;
            CRUDGroupBox.Header = "Asignar un Partido";
            AceptarCambiosButton.Content = "Asignar Partido";
            OcultarControlPorAccion();
           
        }

        private void ModificarPartidoButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as PartidosViewModel).AccionAsignarmodificar = Accion.Editar;
            CRUDGroupBox.Header = "Modificar un Partido";
            AceptarCambiosButton.Content = "Modificar Partido";
            OcultarControlPorAccion();
            (DataContext as PartidosViewModel).LimpiarCampos();
            DeshabilitarControles();
        }

        private void EliminarPartidoButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as PartidosViewModel).AccionAsignarmodificar = Accion.Borrar;
            CRUDGroupBox.Header = "Eliminar un Partido";
            AceptarCambiosButton.Content = "Eliminar Partido";
            OcultarControlPorAccion();
            (DataContext as PartidosViewModel).LimpiarCampos();
        }

        private void CategoriasCRUDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                (DataContext as PartidosViewModel).EquiposVisitantes = new System.Collections.ObjectModel.ObservableCollection<Utilidades.ClasesPojos.Equipo>();
                (DataContext as PartidosViewModel).EquipoVisitante = new Utilidades.ClasesPojos.Equipo();
                
                (DataContext as PartidosViewModel).ObtenerEquiposyArbitrosPrincipales();
                if ((DataContext as PartidosViewModel).AccionAsignarmodificar == Accion.Nuevo && (DataContext as PartidosViewModel).Categoria != null)
                {
                    EquiposLocalesComboBox.IsEnabled = true;
                    ArbitrosPrincipalesComboBox.IsEnabled = true;
                    PartidosComboBox.IsEnabled = false;
                    OcultarMostrarcontrolesPorCategoria();
                    (DataContext as PartidosViewModel).CambioAccionCategorias();
                }
                else if ((DataContext as PartidosViewModel).AccionAsignarmodificar == Accion.Editar && (DataContext as PartidosViewModel).Categoria != null)
                {
                    OcultarMostrarcontrolesPorCategoria();
                    (DataContext as PartidosViewModel).ObtenerPartidos();
                    (DataContext as PartidosViewModel).CambioAccionCategorias();
                    PartidosComboBox.IsEnabled = true;
                }
                else
                {
                    (DataContext as PartidosViewModel).ObtenerPartidos();
                    PartidosComboBox.IsEnabled = true;
                }
                EquiposVisitantesComboBox.IsEnabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void ArbitrosPrincipalesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                (DataContext as PartidosViewModel).ObtenerArbitrosSecundarios();
                if ((DataContext as PartidosViewModel).AccionCategoriaTrabajo == AccionCategoria.RegionalyPreferente)
                {
                    (DataContext as PartidosViewModel).ObtenerCronometradores();
                    CronometradoresComboBox.IsEnabled = true;
                }
                ArbitrosSecundariosComboBox.IsEnabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void EquiposLocalesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if ((DataContext as PartidosViewModel).Categoria != null && (DataContext as PartidosViewModel).AccionAsignarmodificar == Accion.Nuevo)
                {
                    (DataContext as PartidosViewModel).ObtenerEquiposVisitantes();
                    EquiposVisitantesComboBox.IsEnabled = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                       
        }

        private void ArbitrosSecundariosComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                (DataContext as PartidosViewModel).ObtenerCronometradores();
                CronometradoresComboBox.IsEnabled = true;
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
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
                    case 2:
                        MessageBox.Show("Partido modificado correctamente", "Modificar", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    case 3:
                        MessageBox.Show("Partido eliminado correctamente", "Modificar", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                }
                OcultarControlPorAccion();

            }
            catch(Exception ex)
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
            if((DataContext as PartidosViewModel).AccionAsignarmodificar == Accion.Editar)
            {
                EquipoLocalLabel.Visibility = Visibility.Collapsed;
                EquiposLocalesComboBox.Visibility = Visibility.Collapsed;
                EquipoVisitanteLabel.Visibility = Visibility.Collapsed;
                EquiposVisitantesComboBox.Visibility = Visibility.Collapsed;

                ElegirPartidoLabel.Visibility = Visibility.Visible;
                PartidosComboBox.Visibility = Visibility.Visible;
                DisputadoLabel.Visibility = Visibility.Visible;
                DisputadoCheckBox.Visibility = Visibility.Visible;
                ResultadoLabel.Visibility = Visibility.Visible;
                ResultadoStackPanel.Visibility = Visibility.Visible;

                MostrarArbitrosyJornadayDestalles();
                JornadaComboBox.IsEnabled = false;
                DeshabilitarControles();
                AsignarPartidoButton.IsEnabled = true;
                ModificarPartidoButton.IsEnabled = false;
                EliminarPartidoButton.IsEnabled = true;


            }
            else if((DataContext as PartidosViewModel).AccionAsignarmodificar == Accion.Nuevo)
            {
                EquipoLocalLabel.Visibility = Visibility.Visible;
                EquiposLocalesComboBox.Visibility = Visibility.Visible;
                EquipoVisitanteLabel.Visibility = Visibility.Visible;
                EquiposVisitantesComboBox.Visibility = Visibility.Visible;

                ElegirPartidoLabel.Visibility = Visibility.Collapsed;
                PartidosComboBox.Visibility = Visibility.Collapsed;
                DisputadoLabel.Visibility = Visibility.Collapsed;
                DisputadoCheckBox.Visibility = Visibility.Collapsed;
                ResultadoLabel.Visibility = Visibility.Collapsed;
                ResultadoStackPanel.Visibility = Visibility.Collapsed;

                MostrarArbitrosyJornadayDestalles();

                JornadaComboBox.IsEnabled = true;
                HoraEncuentroStackPanel.IsEnabled = true;
                FechaEncuentro.IsEnabled = true;
                LocalidadTextBox.IsEnabled = true;
                DireccionTextBox.IsEnabled = true;

                HabilitarControles();
                DeshabilitarControlesArbitros();
            

                AsignarPartidoButton.IsEnabled = false;
                EliminarPartidoButton.IsEnabled = true;
                ModificarPartidoButton.IsEnabled = true;
            }
            else
            {
                JornadaLabel.Visibility = Visibility.Collapsed;
                JornadaComboBox.Visibility = Visibility.Collapsed;
             

                EquipoLocalLabel.Visibility = Visibility.Collapsed;
                EquiposLocalesComboBox.Visibility = Visibility.Collapsed;
                EquipoVisitanteLabel.Visibility = Visibility.Collapsed;
                EquiposVisitantesComboBox.Visibility = Visibility.Collapsed;

                ArbitrosPrincipalesLabel.Visibility = Visibility.Collapsed;
                ArbitrosPrincipalesComboBox.Visibility = Visibility.Collapsed;
                ArbitrosSecundariosLabel.Visibility = Visibility.Collapsed;
                ArbitrosSecundariosComboBox.Visibility = Visibility.Collapsed;
                CronometradoresLabel.Visibility = Visibility.Collapsed;
                CronometradoresComboBox.Visibility = Visibility.Collapsed;
                TercerArbitroLabel.Visibility = Visibility.Collapsed;
                TercerosArbitrosComboBox.Visibility = Visibility.Collapsed;

                ProvinciaLabel.Visibility = Visibility.Collapsed;
                ProvinciasComboBox.Visibility = Visibility.Collapsed;
                LocalidadLabel.Visibility = Visibility.Collapsed;
                LocalidadTextBox.Visibility = Visibility.Collapsed;
                DireccionLabel.Visibility = Visibility.Collapsed;
                DireccionTextBox.Visibility = Visibility.Collapsed;
                FechaLabel.Visibility = Visibility.Collapsed;
                FechaEncuentro.Visibility = Visibility.Collapsed;
                HoraLabel.Visibility = Visibility.Collapsed;
                HoraEncuentroStackPanel.Visibility = Visibility.Collapsed;
                DisputadoLabel.Visibility = Visibility.Collapsed;
                DisputadoCheckBox.Visibility = Visibility.Collapsed;
                ResultadoLabel.Visibility = Visibility.Collapsed;
                ResultadoStackPanel.Visibility = Visibility.Collapsed;

                ElegirPartidoLabel.Visibility = Visibility.Visible;
                PartidosComboBox.Visibility = Visibility.Visible;
                DisputadoLabel.Visibility = Visibility.Collapsed;
                DisputadoCheckBox.Visibility = Visibility.Collapsed;
                ResultadoLabel.Visibility = Visibility.Collapsed;
                ResultadoStackPanel.Visibility = Visibility.Collapsed;

                AsignarPartidoButton.IsEnabled = true;
                EliminarPartidoButton.IsEnabled = false;
                ModificarPartidoButton.IsEnabled = true;


            }
        }

        private void PartidosComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if ((DataContext as PartidosViewModel).Partidos.Count > 0)
                {
                    (DataContext as PartidosViewModel).ObtenerDatosPartidoUpdate();
                    HabilitarControles();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                      
        }

        private void MostrarArbitrosyJornadayDestalles()
        {

            JornadaLabel.Visibility = Visibility.Visible;
            JornadaComboBox.Visibility = Visibility.Visible;

            ArbitrosPrincipalesLabel.Visibility = Visibility.Visible;
            ArbitrosPrincipalesComboBox.Visibility = Visibility.Visible;
            ArbitrosSecundariosLabel.Visibility = Visibility.Visible;
            ArbitrosSecundariosComboBox.Visibility = Visibility.Visible;
            CronometradoresLabel.Visibility = Visibility.Visible;
            CronometradoresComboBox.Visibility = Visibility.Visible;
            TercerArbitroLabel.Visibility = Visibility.Visible;
            TercerosArbitrosComboBox.Visibility = Visibility.Visible;

            ProvinciaLabel.Visibility = Visibility.Visible;
            ProvinciasComboBox.Visibility = Visibility.Visible;
            LocalidadLabel.Visibility = Visibility.Visible;
            LocalidadTextBox.Visibility = Visibility.Visible;
            DireccionLabel.Visibility = Visibility.Visible;
            DireccionTextBox.Visibility = Visibility.Visible;
            FechaLabel.Visibility = Visibility.Visible;
            FechaEncuentro.Visibility = Visibility.Visible;
            HoraLabel.Visibility = Visibility.Visible;
            HoraEncuentroStackPanel.Visibility = Visibility.Visible;

        }

        private void HabilitarControles()
        {
            DisputadoCheckBox.IsEnabled = true;
            ResultadoStackPanel.IsEnabled = true;
            LocalidadTextBox.IsEnabled = true;
            JornadaComboBox.IsEnabled = true;
            ArbitrosPrincipalesComboBox.IsEnabled = true;
            ArbitrosSecundariosComboBox.IsEnabled = true;
            CronometradoresComboBox.IsEnabled = true;
            TercerosArbitrosComboBox.IsEnabled = true;
            DireccionTextBox.IsEnabled = true;
            FechaEncuentro.IsEnabled = true;
            HoraEncuentroStackPanel.IsEnabled = true;
        }

        private void DeshabilitarControles()
        {
            EquiposLocalesComboBox.IsEnabled = false;
            EquiposVisitantesComboBox.IsEnabled = false;
            HoraEncuentroStackPanel.IsEnabled = false;
            FechaEncuentro.IsEnabled = false;
            LocalidadTextBox.IsEnabled = false;
            DireccionTextBox.IsEnabled = false;
            PartidosComboBox.IsEnabled = false;
            DisputadoCheckBox.IsEnabled = false;
            ResultadoStackPanel.IsEnabled = false;
            DeshabilitarControlesArbitros();
        }

        private void DeshabilitarControlesArbitros()
        {
            ArbitrosPrincipalesComboBox.IsEnabled = false;
            ArbitrosSecundariosComboBox.IsEnabled = false;
            TercerosArbitrosComboBox.IsEnabled = false;
            CronometradoresComboBox.IsEnabled = false;
        }

        private void AccionPartido_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (((PartidosViewModel)DataContext).AccionAsignarmodificar == Accion.Nuevo)
            {
                if(((PartidosViewModel)DataContext).AccionCategoriaTrabajo == AccionCategoria.PrimeraySegunda)
                {
                    e.CanExecute = (DataContext as PartidosViewModel).ArbitroPrincipal != null && (DataContext as PartidosViewModel).ArbitroSecundario != null && (DataContext as PartidosViewModel).TercerArbitro != null && (DataContext as PartidosViewModel).Cronometrador != null && (DataContext as PartidosViewModel).PartidoUso.Jornada != null && (DataContext as PartidosViewModel).Fecha != null && (DataContext as PartidosViewModel).Hora != "00" && (DataContext as PartidosViewModel).Categoria!=null;
                }else if((DataContext as PartidosViewModel).AccionCategoriaTrabajo == AccionCategoria.TerceraySegundaB)
                {
                    e.CanExecute = (DataContext as PartidosViewModel).ArbitroPrincipal != null && (DataContext as PartidosViewModel).ArbitroSecundario != null  && (DataContext as PartidosViewModel).Cronometrador != null && (DataContext as PartidosViewModel).PartidoUso.Jornada != null && (DataContext as PartidosViewModel).Fecha != null && (DataContext as PartidosViewModel).Hora != "00"  && (DataContext as PartidosViewModel).Categoria != null;
                }else if((DataContext as PartidosViewModel).AccionCategoriaTrabajo == AccionCategoria.RegionalyPreferente)
                {
                    e.CanExecute = (DataContext as PartidosViewModel).ArbitroPrincipal != null && (DataContext as PartidosViewModel).Cronometrador != null && (DataContext as PartidosViewModel).PartidoUso.Jornada != null && (DataContext as PartidosViewModel).Fecha != null && (DataContext as PartidosViewModel).Hora != "00" && (DataContext as PartidosViewModel).Categoria != null;
                }
                else
                {
                    e.CanExecute = (DataContext as PartidosViewModel).ArbitroPrincipal != null && (DataContext as PartidosViewModel).ArbitroSecundario != null && (DataContext as PartidosViewModel).TercerArbitro != null  && (DataContext as PartidosViewModel).PartidoUso.Jornada != null && (DataContext as PartidosViewModel).Fecha != null && (DataContext as PartidosViewModel).Hora != "00" && (DataContext as PartidosViewModel).Categoria != null;
                }
                
            }
            else
            {
                e.CanExecute = (DataContext as PartidosViewModel).EquipoTemplate != null && (DataContext as PartidosViewModel).EquipoTemplate.EquipoLocal != null;
            }
        }

        private void AccionPartido_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                int respuesta = (DataContext as PartidosViewModel).Excecute();
                switch (respuesta)
                {
                    case 1:
                        MessageBox.Show("Nuevo partido asignado correctamente", "Insertar", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    case 2:
                        MessageBox.Show("Partido modificado correctamente", "Modificar", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    case 3:
                        MessageBox.Show("Partido eliminado correctamente", "Modificar", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                }
                OcultarControlPorAccion();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


    }
}
