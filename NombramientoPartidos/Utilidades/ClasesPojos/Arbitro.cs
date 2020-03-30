using Newtonsoft.Json;

namespace NombramientoPartidos.Utilidades.ClasesPojos
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Arbitro
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "dni")]
        public string Dni { get; set; } 

        [JsonProperty(PropertyName = "pass")]
        public string Pass { get; set; }

        [JsonProperty(PropertyName = "foto")]
        public byte[] Foto { get; set; }

        [JsonProperty(PropertyName = "nombre_completo")]
        public string Nombre_Completo { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "fecha_nacimiento")]
        public string Fecha_Nacimiento { get; set; }

        [JsonProperty(PropertyName = "provincia")]
        public string Provincia { get; set; }

        [JsonProperty(PropertyName = "localidad")]
        public string Localidad { get; set; }

        [JsonProperty(PropertyName = "cp")]
        public int Cp { get; set; }

        [JsonProperty(PropertyName = "categoria")]
        public string Categoria { get; set; }

        [JsonProperty(PropertyName = "telefono")]
        public string Telefono { get; set; }

        public Arbitro()
        {

        }

        public Arbitro(string dni, string pass, byte[] foto, string nombre_completo, string email, string fecha_nacimiento, string provincia, string localidad, int cp, string categoria, string telefono)
        {
            Dni = dni;
            Pass = pass;
            Foto = foto;
            Nombre_Completo = nombre_completo;
            Email = email;
            Fecha_Nacimiento = fecha_nacimiento;
            Categoria = categoria;
            Telefono = telefono;
            Provincia = provincia;
            Localidad = localidad;
            Cp = cp;
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
