using NombramientoPartidos.Utilidades.ClasesPojos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NombramientoPartidos.Utilidades
{
    public class EquiposTemplate : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

         public Equipo EquipoLocal { get; set; }

        public Equipo EquipoVisitante { get; set; }

        public EquiposTemplate()
        {

        }

        public EquiposTemplate(Equipo equipolocal, Equipo equipovisitante)
        {
            EquipoLocal=equipolocal;
            EquipoVisitante = equipovisitante;
        }
    }
}
