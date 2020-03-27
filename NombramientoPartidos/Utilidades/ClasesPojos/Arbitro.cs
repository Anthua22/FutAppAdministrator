﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NombramientoPartidos.Utilidades.ClasesPojos
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Arbitro
    {
        [JsonProperty(PropertyName = "dni")]
        public string Dni { get; set; } 

        [JsonProperty(PropertyName = "pass")]
        public string Pass { get; set; } 
        
        [JsonProperty(PropertyName = "nombre_completo")]
        public string Nombre_Completo { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "edad")]
        public int Edad { get; set; }

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

        public Arbitro(string dni, string pass, string nombre_completo, string email, int edad, string provincia, string localidad, int cp, string categoria, string telefono)
        {
            Dni = dni;
            Pass = pass;
            Nombre_Completo = nombre_completo;
            Email = email;
            Edad = edad;
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
