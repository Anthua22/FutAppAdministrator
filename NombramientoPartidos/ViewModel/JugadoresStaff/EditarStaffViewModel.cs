using NombramientoPartidos.Services;
using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

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

        public Equipo EquipoCambio { get; set; }

        string fotoantigua;

        public EditarStaffViewModel()
        {
            Staffs = new ObservableCollection<Staff>();
            Categorias = Utils.Categorias;
            Cargos = Utils.CargosStaff;
            EquipoCambio = new Equipo();
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
            ValidacionesRegexp.ValidarDniNie(StaffUpdate.Dni);
            StaffUpdate.Equipo = EquipoCambio.IdEquipo;
            if (!StaffUpdate.Foto.Equals("/Assets/defecto.jpg") && !StaffUpdate.Foto.Contains("http"))
            {
                string[] urlBlob = StaffUpdate.Foto.Split('/');
                BlobStorage.EliminarImagen(fotoantigua, StaffUpdate);
                StaffUpdate.Foto = BlobStorage.GuardarImagen(StaffUpdate.Foto, urlBlob[urlBlob.Length - 1], StaffUpdate);
            }
            return ApiRest.UpdateStaff(StaffUpdate);
        }

        public void CambiarFoto()
        {
            string ruta = Utils.ObtenerRutaFichero().Replace('\\', '/');
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                if (!StaffUpdate.Foto.Equals("/Assets/defecto.jpg"))
                {
                    string[] urlBlob = StaffUpdate.Foto.Split('/');

                    fotoantigua = urlBlob[urlBlob.Length - 1];
                }
                StaffUpdate.Foto = ruta;
            }
        }
    }
}
