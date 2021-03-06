﻿using NombramientoPartidos.View;
using NombramientoPartidos.ViewModel;
using System;
using System.Windows;

namespace NombramientoPartidos
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = new MainWindowViewModel();
            InitializeComponent();
            ObtenerCredenciales();
          
        }
        private void EntrarButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (DataContext as MainWindowViewModel).Contraseña = (bool)RecordarContraseñaCheckBox.IsChecked;
                if ((this.DataContext as MainWindowViewModel).Entrar(PasswordPasswordBox.Password))
                {
                    
                    (DataContext as MainWindowViewModel).GuardarContraseña();
                   
                    Inicio inicio = new Inicio((DataContext as MainWindowViewModel).AdministradorActual);
                    inicio.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ObtenerCredenciales()
        {
            if (Properties.Settings.Default.GuardarContraseña)
            {
               (DataContext as MainWindowViewModel).ObtenerContraseña();
                PasswordPasswordBox.Password = (DataContext as MainWindowViewModel).Pass;

            }
        }

        private void EntrarCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (DataContext as MainWindowViewModel).User.Length>0 && PasswordPasswordBox.Password.Length > 0;
        }

        private void EntrarCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            try
            {
                (DataContext as MainWindowViewModel).Contraseña = (bool)RecordarContraseñaCheckBox.IsChecked;
                if ((this.DataContext as MainWindowViewModel).Entrar(PasswordPasswordBox.Password))
                {

                    (DataContext as MainWindowViewModel).GuardarContraseña();

                    Inicio inicio = new Inicio((DataContext as MainWindowViewModel).AdministradorActual);
                    inicio.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
