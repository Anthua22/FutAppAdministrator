using Newtonsoft.Json;
using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;

namespace NombramientoPartidos.Services
{
    public static class ApiRest
    {
        private static string Urlbase { get; set; }

        static ApiRest()
        {
            Urlbase = "http://pdam13b.iesdoctorbalmis.info/liga/";
        }

        public static Administrador RescatarAdministrador(string dni)
        {
            string url = Urlbase + "Administradores/" + dni;
            var json = new WebClient().DownloadString(url);
            Administrador x = JsonConvert.DeserializeObject<List<Administrador>>(json).First();
            return x;
        }

        public static ObservableCollection<Arbitro> RescatarArbitros()
        {
            string url = Urlbase+"Arbitros/";
            var json = new WebClient().DownloadString(url);
            ObservableCollection<Arbitro> arbitros = new ObservableCollection<Arbitro>(JsonConvert.DeserializeObject<List<Arbitro>>(json));
            return arbitros;
        }

        public static ObservableCollection<Equipo> RescatarEquipos()
        {
            string url = Urlbase+"Equipos/";
            var json = new WebClient().DownloadString(url);
            ObservableCollection<Equipo> equipos = new ObservableCollection<Equipo>(JsonConvert.DeserializeObject<List<Equipo>>(json));
            return equipos;
        }

        public static ObservableCollection<Jugador> RescatarJugadores()
        {
            string url = Urlbase + "Jugadores";
            var json = new WebClient().DownloadString(url);
            ObservableCollection<Jugador> jugadores = new ObservableCollection<Jugador>(JsonConvert.DeserializeObject<List<Jugador>>(json));
            return jugadores;
        }

        public static ObservableCollection<Staff> RescatarStaffs()
        {
            string url = Urlbase + "Staffs";
            var json = new WebClient().DownloadString(url);
            ObservableCollection<Staff> staffs = new ObservableCollection<Staff>(JsonConvert.DeserializeObject<List<Staff>>(json));
            return staffs;
        }

        public static ObservableCollection<Partido> RescartarPartidos()
        {
            string url = Urlbase + "Partidos";
            var json = new WebClient().DownloadString(url);
            ObservableCollection<Partido> partidos = new ObservableCollection<Partido>(JsonConvert.DeserializeObject<List<Partido>>(json));
            return partidos;
        }

        public static bool UpdateAdministrador(Administrador administrador)
        {
            var json = JsonConvert.SerializeObject(administrador);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Urlbase + "Administradores/" + administrador.Dni);
            request.Method = "PUT";
            request.ContentType = "application/json";
            using (var streamwriter = new StreamWriter(request.GetRequestStream()))
            {
                streamwriter.Write(json);
                streamwriter.Flush();
            }
            var response = (HttpWebResponse)request.GetResponse();

            if (!response.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new CRUDException("Error al modificar el dato");
            }
            return true;
        }

        public static bool UpdateArbitro(Arbitro arbitro)
        {
            if (Utils.ControlCampos(arbitro))
            {
                var json = JsonConvert.SerializeObject(arbitro);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Urlbase+"Arbitros/" + arbitro.Id);
                request.Method = "PUT";
                request.ContentType = "application/json";
                using (var streamwriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamwriter.Write(json);
                    streamwriter.Flush();
                }
                var response = (HttpWebResponse)request.GetResponse();
            
                if(!response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    throw new CRUDException("Error al modificar el dato");
                }
                return true;
            }
            return false;
          
            
        }

        public static bool InsertArbitro(Arbitro arbitro)
        {
            if (Utils.ControlCampos(arbitro))
            {
                var json = JsonConvert.SerializeObject(arbitro);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Urlbase+"Arbitros");
                request.Method = "POST";
                request.ContentType = "application/json";

                using (var streamwriter = new StreamWriter(request.GetRequestStream()))
                {  
                    streamwriter.Write(json);
                    streamwriter.Flush();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (!response.StatusCode.Equals(HttpStatusCode.Created))
                {
                    throw new CRUDException("Error al insertar el dato");
                }
                return true;
            }
            return false;
        }

        public static void DeleteteArbitro(int id) 
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Urlbase+"Arbitros/"+id);
            request.Method = "DELETE";
            request.Accept = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (!response.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new CRUDException("Error al borral el registro");
            }

        }

        public static void InsertEquipo(Equipo equipo)
        {
            var json = JsonConvert.SerializeObject(equipo);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Urlbase+"Equipos");
            request.Method = "POST";
            request.ContentType = "application/json";

            using (var streamwriter = new StreamWriter(request.GetRequestStream()))
            {
                streamwriter.Write(json);
                streamwriter.Flush();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (!response.StatusCode.Equals(HttpStatusCode.Created))
            {
                throw new CRUDException("Error al insertar el dato");
            }
        }

        public static void UpdateEquipo(Equipo equipo)
        {
            var json = JsonConvert.SerializeObject(equipo);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Urlbase+"Equipos/" + equipo.IdEquipo);
            request.Method = "PUT";
            request.ContentType = "application/json";
            using (var streamwriter = new StreamWriter(request.GetRequestStream()))
            {
                streamwriter.Write(json);
                streamwriter.Flush();
            }
            var response = (HttpWebResponse)request.GetResponse();

            if (!response.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new CRUDException("Error al modificar el dato");
            }
        }

        public static void DeleteEquipo(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Urlbase+"Equipos/" + id);
            request.Method = "DELETE";
            request.Accept = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (!response.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new CRUDException("Error al borral el registro");
            }
          
        }

        public static bool InsertJugador(Jugador jugador)
        {
            var json = JsonConvert.SerializeObject(jugador);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Urlbase + "Jugadores");
            request.Method = "POST";
            request.ContentType = "application/json";

            using (var streamwriter = new StreamWriter(request.GetRequestStream()))
            {
                streamwriter.Write(json);
                streamwriter.Flush();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (!response.StatusCode.Equals(HttpStatusCode.Created))
            {
                throw new CRUDException("Error al insertar el dato");
            }
            return true;
        }

        public static bool UpdateJugador(Jugador jugador)
        {
            var json = JsonConvert.SerializeObject(jugador);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Urlbase + "Jugadores/" + jugador.Id);
            request.Method = "PUT";
            request.ContentType = "application/json";
            using (var streamwriter = new StreamWriter(request.GetRequestStream()))
            {
                streamwriter.Write(json);
                streamwriter.Flush();
            }
            var response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                return true;
            }
            else
            {
                throw new CRUDException("Error al modificar el dato");
            }

        }

        public static void DeleteteJugador(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Urlbase + "Jugadores/" + id);
            request.Method = "DELETE";
            request.Accept = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (!response.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new CRUDException("Error al borral el registro");
            }

        }

        public static bool InsertStaff(Staff staff)
        {
            var json = JsonConvert.SerializeObject(staff);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Urlbase + "Staffs");
            request.Method = "POST";
            request.ContentType = "application/json";

            using (var streamwriter = new StreamWriter(request.GetRequestStream()))
            {
                streamwriter.Write(json);
                streamwriter.Flush();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (!response.StatusCode.Equals(HttpStatusCode.Created))
            {
                throw new CRUDException("Error al insertar el dato");
            }
            return true;
        }
        
        public static bool UpdateStaff(Staff staff)
        {
            var json = JsonConvert.SerializeObject(staff);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Urlbase + "Staffs/" + staff.Id);
            request.Method = "PUT";
            request.ContentType = "application/json";
            using (var streamwriter = new StreamWriter(request.GetRequestStream()))
            {
                streamwriter.Write(json);
                streamwriter.Flush();
            }
            var response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                return true;
            }
            else
            {
                throw new CRUDException("Error al modificar el dato");
            }
        }
    
        public static void DeleteStaff(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Urlbase + "Staffs/" + id);
            request.Method = "DELETE";
            request.Accept = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (!response.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new CRUDException("Error al borral el registro");
            }
        }

        public static void InsertPartido(Partido partido)
        {
            var json = JsonConvert.SerializeObject(partido);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Urlbase + "Partidos");
            request.Method = "POST";
            request.ContentType = "application/json";

            using (var streamwriter = new StreamWriter(request.GetRequestStream()))
            {
                streamwriter.Write(json);
                streamwriter.Flush();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (!response.StatusCode.Equals(HttpStatusCode.Created))
            {
                throw new CRUDException("Error al insertar el dato");
            }
       
        }

        public static void UpdatePartido(Partido partido)
        {
            var json = JsonConvert.SerializeObject(partido);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Urlbase + "Partidos/" + partido.IdPartido);
            request.Method = "PUT";
            request.ContentType = "application/json";
            using (var streamwriter = new StreamWriter(request.GetRequestStream()))
            {
                streamwriter.Write(json);
                streamwriter.Flush();
            }
            var response = (HttpWebResponse)request.GetResponse();

            if (!response.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new CRUDException("Error al modificar el dato");
            }
        }

        public static void DeletePartido(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Urlbase + "Partidos/" + id);
            request.Method = "DELETE";
            request.Accept = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (!response.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new CRUDException("Error al borral el registro");
            }
       
        }
    }

}
