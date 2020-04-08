﻿using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
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
    /// Lógica de interacción para DeleteArbitroView.xaml
    /// </summary>
    public partial class DeleteArbitroView : Window
    {
        public DeleteArbitroView()
        {
            DataContext = new DeleteViewModel();
            InitializeComponent();

        }

        private void CategoriaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as DeleteViewModel).Filtro();
            ArbitrosComboBox.IsEnabled = true;
        }


        private void BorrarCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if((DataContext as DeleteViewModel).DeleteCanExecute())
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void BorrarCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if ((DataContext as DeleteViewModel).DeleteExecute())
                {
                    DialogResult = true;
                }
            }
            catch (CRUDException crud)
            {
                MessageBox.Show("Error: " + crud.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}