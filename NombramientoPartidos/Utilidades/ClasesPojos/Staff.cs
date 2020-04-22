using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NombramientoPartidos.Utilidades.ClasesPojos
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Staff: INotifyPropertyChanged
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "equipo")]
        public int Equipo { get; set; }

        [JsonProperty(PropertyName = "cargo")]
        public string Cargo { get; set; }

        [JsonProperty(PropertyName = "fecha_nacimiento")]
        public string Fecha_Nacimiento { get; set; }

        [JsonProperty(PropertyName = "nombre_completo")]
        public string Nombre_Completo { get; set; }

        [JsonProperty(PropertyName = "foto")]
        public string Foto { get; set; }

        [JsonProperty(PropertyName = "dni")]
        public string Dni { get; set; }

        public Staff()
        {

        }

        public Staff(string nombre_completo, string cargo, string fecha_nacimiento, string dni, int equipo)
        {
            Nombre_Completo = nombre_completo;
            Fecha_Nacimiento = fecha_nacimiento;
            Dni = dni;
            Cargo = cargo;
            Equipo = equipo;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
