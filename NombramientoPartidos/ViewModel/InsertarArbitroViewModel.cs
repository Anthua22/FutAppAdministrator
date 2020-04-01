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

namespace NombramientoPartidos.ViewModel
{
    class InsertarArbitroViewModel:INotifyPropertyChanged
    {
        public Arbitro ArbitroInsertar { get; private set; }
        public ObservableCollection<string> Categorias { get; }

        public InsertarArbitroViewModel()
        {
            Categorias = new ObservableCollection<string>
            {
                "1º División",
                "2º División",
                "2ºB División",
                "3º División",
                "Preferente",
                "Fútbol Base"
            };
            ArbitroInsertar = new Arbitro();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool InsertarArbitro(string dni, string contraseña, string nombre_completo, string email, string fecha_nacimiento, string provincia, string localidad, int cp, string catergoria, string telefono)
        {
            ArbitroInsertar = new Arbitro(dni, contraseña, nombre_completo, email, fecha_nacimiento, provincia, localidad, cp, catergoria, telefono);
           // ArbitroInsertar = Utils.ComprobarCamposVacios(ArbitroInsertar);
            return ApiRest.InsertArbitro(ArbitroInsertar);
        }
    }
}
