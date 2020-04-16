using NombramientoPartidos.Services;
using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NombramientoPartidos.ViewModel.JugadoresStaff
{
    class EditarStaffViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Equipo> Equipos { get; set; }

        public ObservableCollection<string> Categorias { get; set; }

        public ObservableCollection<string> Cargos { get; set; }

        public ObservableCollection<Staff> Staffs { get; set; }

        public Staff StaffUpdate { get; set; }

        public Equipo EquipoStaff { get; set; }


        public EditarStaffViewModel()
        {
            Staffs = new ObservableCollection<Staff>();
            Categorias = Utils.Categorias;
            Cargos = Utils.CargosStaff;
            EquipoStaff = new Equipo();
        }

        public void FiltroEquipos(string categoria)
        {
            Equipos = Utils.FiltroEquipos(categoria);
        }

        public void FiltroStaffs()
        {
            if (EquipoStaff != null)
            {
                Staffs = new ObservableCollection<Staff>(ApiRest.RescatarStaffs().Where(x => x.Equipo == EquipoStaff.IdEquipo).OrderBy(y => y.Nombre_Completo));
            }
        }

        public bool Update()
        {
            StaffUpdate.Dni = StaffUpdate.Dni.ToUpper();
            StaffUpdate.Equipo = EquipoStaff.IdEquipo;
            return ApiRest.UpdateStaff(StaffUpdate);
        }
    }
}
