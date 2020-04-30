using Newtonsoft.Json;
using System.ComponentModel;

namespace NombramientoPartidos.Utilidades.ClasesPojos
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Partido : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [JsonProperty(PropertyName = "idPartido")]
        public int IdPartido { get; set; }

        [JsonProperty(PropertyName = "jornada")]
        public string Jornada { get; set; }

        [JsonProperty(PropertyName = "equipolocal")]
        public int EquipoLocal { get; set; }

        [JsonProperty(PropertyName = "equipovisitante")]
        public int EquipoVisitante { get; set; }

        [JsonProperty(PropertyName = "provincia")]
        public string Provincia { get; set; }

        [JsonProperty(PropertyName = "localidad")]
        public string Localidad { get; set; }

        [JsonProperty(PropertyName = "arbitroprincipal")]
        public int ArbitroPrincipal { get; set; }

        [JsonProperty(PropertyName = "arbitrosecundario")]
        public int ArbitroSecundario { get; set; }

        [JsonProperty(PropertyName = "cronometrador")]
        public int Cronometrador { get; set; }

        [JsonProperty(PropertyName = "tercer_arbitro")]
        public int Tercer_Arbitro { get; set; }

        [JsonProperty(PropertyName = "direccion_encuentro")]
        public string Direccion_Encuentro { get; set; }

        [JsonProperty(PropertyName = "fecha_encuentro")]
        public string Fecha_Encuentro { get; set; }

        [JsonProperty(PropertyName = "resultado")]
        public string Resultado { get; set; }

        [JsonProperty(PropertyName = "disputado")]
        public int Disputado { get; set; }

        [JsonProperty(PropertyName = "categoria")]
        public string Categoria { get; set; }

        public Partido()
        {

        }

        public Partido(string jornada, int equipolocal, int equipovisitante, int arbitroprincipal,string provincia, string localidad, string direccion_encuentro, string fecha_encuentro)
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
