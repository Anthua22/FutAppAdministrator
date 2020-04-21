using Newtonsoft.Json;
using System.ComponentModel;

namespace NombramientoPartidos.Utilidades.ClasesPojos
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Arbitro:INotifyPropertyChanged
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "dni")]
        public string Dni { get; set; } 

        [JsonProperty(PropertyName = "pass")]
        public string Pass { get; set; }

        [JsonProperty(PropertyName = "foto")]
        public string Foto { get; set; }

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
        public string Cp { get; set; }

        [JsonProperty(PropertyName = "categoria")]
        public string Categoria { get; set; }

        [JsonProperty(PropertyName = "telefono")]
        public string Telefono { get; set; }

        public Arbitro()
        {

        }

        public Arbitro(string dni, string pass, string nombre_completo, string email, string fecha_nacimiento, string provincia, string localidad, string cp, string categoria, string telefono)
        {
            Dni = dni;
            Pass = Utils.EncriptarEnSHA1(pass);
            Nombre_Completo = nombre_completo;
            Email = email;
            Fecha_Nacimiento = fecha_nacimiento;
            Categoria = categoria;
            Telefono = telefono;
            Provincia = provincia;
            Localidad = localidad;
            Cp = cp;
        }

        public Arbitro(string dni, string contraseña, string fecha_nacimiento, string email, string categoria, string provincia)
        {
            Dni = dni;
            Pass = Utils.EncriptarEnSHA1(contraseña); 
            Fecha_Nacimiento = fecha_nacimiento;
            Email = email;
            Categoria = categoria;
            Provincia = provincia;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
