using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NombramientoPartidos.Utilidades
{
    public static class ComandosPersonalizados
    {
        public static readonly RoutedUICommand Entrar = new RoutedUICommand
            (
                "Entrar",
                "Entrar",
                typeof(ComandosPersonalizados),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.Enter)
                }
            );
        
        public static readonly RoutedUICommand AceptarEdicion = new RoutedUICommand
            (
                "Aceptar",
                "Aceptar",
                typeof(ComandosPersonalizados)
            );

        public static readonly RoutedUICommand AceptarEliminacion = new RoutedUICommand
            (
                "Aceptar",
                "Aceptar",
                typeof(ComandosPersonalizados),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.Enter)
                }

            );

        public static readonly RoutedUICommand ComandoJugador = new RoutedUICommand
            (
                "Aceptar",
                "Aceptar",
                typeof(ComandosPersonalizados)
            );

        public static readonly RoutedUICommand ComandoStaff = new RoutedUICommand
            (
                "Aceptar",
                "Aceptar",
                typeof(ComandosPersonalizados)
            );

    }
}
