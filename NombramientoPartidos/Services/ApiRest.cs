using Newtonsoft.Json;
using NombramientoPartidos.Utilidades.ClasesPojos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
    }
}
