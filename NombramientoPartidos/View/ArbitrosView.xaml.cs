﻿using NombramientoPartidos.ViewModel;
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

namespace NombramientoPartidos.View
{
    /// <summary>
    /// Lógica de interacción para Arbitros.xaml
    /// </summary>
    public partial class Arbitros : UserControl
    {
        public Arbitros()
        {
            this.DataContext = new ArbitroViewModel();
            InitializeComponent();
        }

        private void AgregarArbitroButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ModificarArbitroButton_Click(object sender, RoutedEventArgs e)
        {
            if((this.DataContext as ArbitroViewModel).EditarArbitroClick())
            {
                MessageBox.Show("Arbitro Actualizado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void EliminarArbitroButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
