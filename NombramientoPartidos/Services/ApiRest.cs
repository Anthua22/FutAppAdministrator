using Newtonsoft.Json;
using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace NombramientoPartidos.Services
{
    public static class ApiRest
    {
        
        public static Administrador RescatarAdministrador(string dni)
        {
            string url = "http://localhost/liga/administradores/" + dni;
            var json = new WebClient().DownloadString(url);
            Administrador x = JsonConvert.DeserializeObject<List<Administrador>>(json).First();
            return x;
        }

        public static ObservableCollection<Arbitro> RescatarArbitros()
        {
            string url = "http://localhost/liga/arbitros/";
            var json = new WebClient().DownloadString(url);
            ObservableCollection<Arbitro> arbitros = new ObservableCollection<Arbitro>(JsonConvert.DeserializeObject<List<Arbitro>>(json));
            return arbitros;
        }

        public static ObservableCollection<Equipo> RescatarEquipos()
        {
            string url = "http://localhost/liga/equipos/";
            var json = new WebClient().DownloadString(url);
            ObservableCollection<Equipo> equipos = new ObservableCollection<Equipo>(JsonConvert.DeserializeObject<List<Equipo>>(json));
            return equipos;
        }

        public static bool UpdateArbitro(Arbitro arbitro)
        {
            if (Utils.ControlCampos(arbitro))
            {
                var json = JsonConvert.SerializeObject(arbitro);
                var bytes = Encoding.UTF8.GetBytes(json);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost/liga/arbitros/" + arbitro.Id);
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
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost/liga/arbitros");
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
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost/liga/arbitros/"+id);
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
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost/liga/equipos");
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
            var bytes = Encoding.UTF8.GetBytes(json);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost/liga/equipos/" + equipo.IdEquipo);
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

        public static bool DeleteEquipo(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost/liga/equipos/" + id);
            request.Method = "DELETE";
            request.Accept = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (!response.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new CRUDException("Error al borral el registro");
            }
            else
                return true;
        }
    }
}
