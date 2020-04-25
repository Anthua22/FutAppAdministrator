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

        }

        private void ModificarPartidoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CategoriasCRUDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as PartidosViewModel).ObtenerEquiposyArbitrosPrincipales();
            EquiposLocalesComboBox.IsEnabled = true;
          //  CronometradoresComboBox.IsEnabled = true;
            ArbitrosPrincipalesComboBox.IsEnabled = true;
        }

        private void ArbitrosPrincipalesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as PartidosViewModel).ObtenerArbitrosSecundarios();
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
    }
}
