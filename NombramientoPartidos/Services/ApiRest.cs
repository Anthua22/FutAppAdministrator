using Newtonsoft.Json;
using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;

namespace NombramientoPartidos.Services
{
    public static class ApiRest
    {
        
        public static Administrador RescatarAdministrador(string dni)
        {
            string url = "http://192.168.0.132/liga/administradores/" + dni;
            var json = new WebClient().DownloadString(url);
            Administrador x = JsonConvert.DeserializeObject<List<Administrador>>(json).First();
            return x;
        }

        public static ObservableCollection<Arbitro> RescatarArbitros()
        {
            string url = "http://192.168.0.132/liga/arbitros/";
            var json = new WebClient().DownloadString(url);
            ObservableCollection<Arbitro> arbitros = new ObservableCollection<Arbitro>(JsonConvert.DeserializeObject<List<Arbitro>>(json));
            return arbitros;
        }

        public static bool UpdateArbitro(Arbitro arbitro)
        {
            if (Utils.ControlCamposUpdate(arbitro))
            {
                var json = JsonConvert.SerializeObject(arbitro);
                var bytes = Encoding.UTF8.GetBytes(json);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.0.132/liga/arbitros/" + arbitro.Id);
                request.Method = "PUT";
                request.ContentType = "application/json";
                using(var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
            
                if(!response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    throw new UpdateException("Error al modificar el dato");
                }
                return true;
            }
            return false;
          
            
        }
    }
}
