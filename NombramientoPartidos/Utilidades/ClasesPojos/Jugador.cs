﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NombramientoPartidos.Utilidades.ClasesPojos
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Jugador: INotifyPropertyChanged
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "equipo")]
        public int Equipo { get; set; }

        [JsonProperty(PropertyName = "nombre_completo")]
        public string Nombre_Completo { get; set; }

        [JsonProperty(PropertyName = "foto")]
        public string Foto { get; set; }

        [JsonProperty(PropertyName = "sancionado")]
        public int Sancionado { get; set; }

        [JsonProperty(PropertyName = "goles")]
        public int Goles { get; set; }

        [JsonProperty(PropertyName = "fecha_nacimiento")]
        public string Fecha_Nacimiento { get; set; }

        [JsonProperty(PropertyName = "categoria")]
        public string Categoria { get; set; }

        [JsonProperty(PropertyName = "dni")]
        public string Dni { get; set; }

        public Jugador()
        {

        }

        public Jugador(string nombre_completo, int equipo, string dni, string fecha_nacimiento, string categoria)
        {
            Nombre_Completo = nombre_completo; 
            Dni = dni;
            Fecha_Nacimiento = fecha_nacimiento;
            Equipo = equipo;
            Categoria = categoria;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
