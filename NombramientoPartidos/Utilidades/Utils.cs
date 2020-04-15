﻿using NombramientoPartidos.Utilidades.ClasesPojos;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System;
using NombramientoPartidos.Services;

namespace NombramientoPartidos.Utilidades
{
    public static class Utils
    {
        public static ObservableCollection<string> Categorias { get; }
  

        static Utils()
        {
            Categorias = new ObservableCollection<string>()
            {
                "1º División",
                "2º División",
                "2ºB División",
                "3º División",
                "Regional",
                "Preferente",
                "Fútbol Base"
            };
        }

       
        public static string EncriptarEnSHA1(string str)
        {
            SHA1 sha1 = SHA1Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            StringBuilder sb = new StringBuilder();
            byte[] stream = sha1.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
       
        public static bool ControlCampos(Arbitro arbitro)
        {
            if (arbitro.Pass.Length != 40 && ValidacionesRegexp.ValidarPass(arbitro.Pass))
            {
                string v = EncriptarEnSHA1(arbitro.Pass);
                arbitro.Pass = v;
            } 
            if(ValidacionesRegexp.ComprobarCategoria(arbitro.Categoria)
                && ValidacionesRegexp.ValidarFecha(arbitro.Fecha_Nacimiento)
                && ValidacionesRegexp.ValidarEmail(arbitro.Email)
                && ValidacionesRegexp.ValidarTelefono(arbitro.Telefono)
                && ValidacionesRegexp.ValidarDniNie(arbitro.Dni))
            {
                return true;
            }
           
            return false;
        }


        public static bool HayCamposVacios(string dni, string contraseña, string fecha_nacimiento, string email, string provincia, string cp)
        {
            return (string.IsNullOrWhiteSpace(dni) || string.IsNullOrWhiteSpace(contraseña) || string.IsNullOrWhiteSpace(fecha_nacimiento) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(provincia) || string.IsNullOrWhiteSpace(cp));
        }

    
        public static string ObtenerRutaFichero()
        {
            OpenFileDialog dialogoImagen = new OpenFileDialog();
            string ruta = "";
            dialogoImagen.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            dialogoImagen.Filter = "Imágenes JPG (*.jpg)|*.jpg|Imágenes PNG (*.png)|*.png|Todos los archivos (*.*)|*.*";

            dialogoImagen.FilterIndex = 3;

            dialogoImagen.RestoreDirectory = true;

            if (dialogoImagen.ShowDialog() == DialogResult.OK)
            {

                ruta = dialogoImagen.FileName;

            }
            return ruta;
        }
        
        public static string ObtenerCategoriaJugador(DateTime dateTime, int añoinicioTemporada)
        {
            if(dateTime != DateTime.Now && dateTime.Year < DateTime.Now.Year)
            {
                if(añoinicioTemporada - dateTime.Year >= 8 && añoinicioTemporada - dateTime.Year <= 9)
                {
                    return "Benjamin";
                }
                else if(añoinicioTemporada - dateTime.Year >= 10 && añoinicioTemporada - dateTime.Year <= 11)
                {
                    return "Alevin";
                }
                else if (añoinicioTemporada - dateTime.Year >=12 && añoinicioTemporada - dateTime.Year <= 13)
                {
                    return "Infantil";
                }
                else if (añoinicioTemporada - dateTime.Year >= 14 && añoinicioTemporada - dateTime.Year <= 15)
                {
                    return "Cadete";
                }
                else if (añoinicioTemporada - dateTime.Year >= 16 && añoinicioTemporada - dateTime.Year <= 18)
                {
                    return "Juvenil";
                }
                else
                {
                    return "Senior";
                }
            }
            return null;
            
        }

        public static ObservableCollection<Equipo> FiltroEquipos(string categoria)
        {
            ObservableCollection<Equipo> Equipos;
            switch (categoria)
            {
                 case "1º División":
                    Equipos = new ObservableCollection<Equipo>(ApiRest.RescatarEquipos().Where(x => x.Categoria.Equals("1º División")).OrderBy(y => y.Nombre));
                    break;
                case "2º División":
                    Equipos = new ObservableCollection<Equipo>(ApiRest.RescatarEquipos().Where(x => x.Categoria.Equals("2º División")).OrderBy(y => y.Nombre));
                    break;
                case "2ºB División":
                    Equipos = new ObservableCollection<Equipo>(ApiRest.RescatarEquipos().Where(x => x.Categoria.Equals("2ºB División")).OrderBy(y => y.Nombre));
                    break;
                case "3º División":
                    Equipos = new ObservableCollection<Equipo>(ApiRest.RescatarEquipos().Where(x => x.Categoria.Equals("3º División")).OrderBy(y => y.Nombre));
                    break;
                case "Preferente":
                    Equipos = new ObservableCollection<Equipo>(ApiRest.RescatarEquipos().Where(x => x.Categoria.Equals("Preferente")).OrderBy(y => y.Nombre));
                    break;
                case "Fútbol Base":
                    Equipos = new ObservableCollection<Equipo>(ApiRest.RescatarEquipos().Where(x => x.Categoria.Equals("Fútbol Base")).OrderBy(y => y.Nombre));
                    break;
                case "Regional":
                    Equipos = new ObservableCollection<Equipo>(ApiRest.RescatarEquipos().Where(x => x.Categoria.Equals("Regional")).OrderBy(y => y.Nombre));
                    break;
                default:
                    Equipos = new ObservableCollection<Equipo>();
                    break;
            }
            return Equipos;
        }
    }
}
