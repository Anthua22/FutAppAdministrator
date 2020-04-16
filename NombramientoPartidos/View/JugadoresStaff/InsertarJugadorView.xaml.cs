﻿using NombramientoPartidos.ViewModel.JuadoresStaff;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NombramientoPartidos.View.JugadoresStaff
{
    /// <summary>
    /// Lógica de interacción para InsertarJugadorView.xaml
    /// </summary>
    public partial class InsertarJugadorView : Window
    {
        public InsertarJugadorView()
        {
            DataContext = new InsertarJugadorViewModel();
            InitializeComponent();
        }

        private void CategoriasComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as InsertarJugadorViewModel).Filtro(CategoriasComboBox.SelectedItem as string);
            EquiposComboBox.IsEnabled = true;
        }

        private void FotoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (DataContext as InsertarJugadorViewModel).PonerImagen(ImagenJugador);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void InsertCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ((DataContext as InsertarJugadorViewModel).EquipoJugador!=null && (DataContext as InsertarJugadorViewModel).EquipoJugador.IdEquipo !=0&& !string.IsNullOrWhiteSpace((DataContext as InsertarJugadorViewModel).JugadorInsertar.Nombre_Completo) && !string.IsNullOrWhiteSpace((DataContext as InsertarJugadorViewModel).JugadorInsertar.Dni));
        }

        private void InsertCommandExecute(object sender, ExecutedRoutedEventArgs e)
        {
            try
            { 
                if((DataContext as InsertarJugadorViewModel).Execute())
                {
                    DialogResult = true;
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void EquiposComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as InsertarJugadorViewModel).AsignarEquipo();
        }
    }
}
