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
    public class Partido : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        [JsonProperty(PropertyName = "idPartido")]
        public int IdPartido { get; set; }

        [JsonProperty(PropertyName = "jornada")]
        public int Jornada { get; set; }

        [JsonProperty(PropertyName = "equipolocal")]
        public Equipo EquipoLocal { get; set; }

        [JsonProperty(PropertyName = "equipovisitante")]
        public Equipo EquipoVisitante { get; set; }

        [JsonProperty(PropertyName = "provincia")]
        public string Provincia { get; set; }

        [JsonProperty(PropertyName = "localidad")]
        public string Localidad { get; set; }

        [JsonProperty(PropertyName = "arbitroprincipal")]
        public Arbitro ArbitroPrincipal { get; set; }

        [JsonProperty(PropertyName = "arbitrosecundario")]
        public Arbitro ArbitroSecundario { get; set; }

        [JsonProperty(PropertyName = "cronometrador")]
        public Arbitro Cronometrador { get; set; }

        [JsonProperty(PropertyName = "tercer_arbitro")]
        public Arbitro Tercer_Arbitro { get; set; }

        [JsonProperty(PropertyName = "cp")]
        public int Cp { get; set; }

        [JsonProperty(PropertyName = "direccion_encuentro")]
        public string Direccion_Encuentro { get; set; }

        [JsonProperty(PropertyName = "fecha_encuentro")]
        public DateTime Fecha_Encuentro { get; set; }

        [JsonProperty(PropertyName = "resultado")]
        public string Resultado { get; set; }

        public Partido()
        {

        }

        public Partido(int jornada, Equipo equipolocal, Equipo equipovisitante, Arbitro arbitroprincipal,string provincia, string localidad, string direccion_encuentro, DateTime fecha_encuentro)
        {
            Jornada = jornada;
            EquipoLocal = equipolocal;
            EquipoVisitante = equipovisitante;
            ArbitroPrincipal = arbitroprincipal;
            Provincia = provincia;
            Localidad = localidad;
            Direccion_Encuentro = direccion_encuentro;
            Fecha_Encuentro = fecha_encuentro;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
