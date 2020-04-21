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
    public class Equipo: INotifyPropertyChanged
    {
        [JsonProperty(PropertyName = "idEquipo")]
        public int IdEquipo { get; set; }

        [JsonProperty(PropertyName = "nombre")]
        public string Nombre { get; set; }

        [JsonProperty(PropertyName = "categoria")]
        public string Categoria { get; set; }

        [JsonProperty(PropertyName = "provincia")]
        public string Provincia { get; set; }

        [JsonProperty(PropertyName = "direccion_campo")]
        public string Direccion_Campo { get; set; }

        public Equipo()
        {

        }
        
        public Equipo(string nombre, string categoria, string provincia, string direccion_campo)
        {
            Nombre = nombre;
            Categoria = categoria;
            Provincia = provincia;
            Direccion_Campo = direccion_campo;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
