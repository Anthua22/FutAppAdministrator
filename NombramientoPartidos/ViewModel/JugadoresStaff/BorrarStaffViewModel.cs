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
using System.Windows;

namespace NombramientoPartidos.ViewModel.JugadoresStaff
{
    class BorrarStaffViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> Categorias { get; set; }

        public ObservableCollection<Staff> Staffs { get; set; }

        public ObservableCollection<Equipo> Equipos { get; set; }

        public Staff StaffDelete { get; set; }

        public Equipo EquipoStaff { get; set; }

        public BorrarStaffViewModel()
        {
            Categorias = Utils.Categorias;
            Equipos = new ObservableCollection<Equipo>();
            Staffs = new ObservableCollection<Staff>();
            EquipoStaff = new Equipo();
            StaffDelete = new Staff();

        }

        public void Filtro(string categoria)
        {
            Equipos = Utils.FiltroEquipos(categoria);
        }

        public void FiltroStaff()
        {
            if(EquipoStaff != null){
                Staffs = new ObservableCollection<Staff>(ApiRest.RescatarStaffs().Where(x => x.Equipo == EquipoStaff.IdEquipo));
            }
        }

        public bool DeleteExecute()
        {
            MessageBoxResult messageresult = MessageBox.Show("Esta seguro que desea eliminar a " + StaffDelete.Nombre_Completo + " del equipo: " + EquipoStaff.Nombre + '?', "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageresult == MessageBoxResult.Yes)
            {
                if (!StaffDelete.Foto.Equals("/Assets/equipodefecto.jpg"))
                {
                    string[] blobreference = StaffDelete.Foto.Split('/');
                    BlobStorage.EliminarImagen(blobreference[blobreference.Length - 1], StaffDelete);
                }

                ApiRest.DeleteStaff(StaffDelete.Id);
                return true;
            }
            return false;
        }

    }
}
