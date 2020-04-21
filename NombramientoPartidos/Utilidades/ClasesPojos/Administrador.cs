using Newtonsoft.Json;
using System.ComponentModel;

namespace NombramientoPartidos.Utilidades.ClasesPojos
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Administrador: INotifyPropertyChanged
    {
        [JsonProperty(PropertyName = "dni")]
        public string Dni { get; set; }

        [JsonProperty(PropertyName = "telefono")]
        public string Telefono { get; set; }

        [JsonProperty(PropertyName = "pass")]
        public string Pass { get; set;}

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "nombre_completo")]
        public string Nombre_Completo { get; set; }

        public Administrador()
        {
        }

        public Administrador(string dni, string telefono, string pass, string email, string nombre_completo)
        {
            Dni = dni;
            Telefono = telefono;
            Pass = pass;
            Email = email;
            Nombre_Completo = nombre_completo;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
