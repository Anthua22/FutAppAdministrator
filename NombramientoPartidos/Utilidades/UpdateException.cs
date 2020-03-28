using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NombramientoPartidos.Utilidades
{
    public class UpdateException:Exception
    {
        string mensaje;
        public UpdateException(string mensaje) : base(mensaje)
        {
            this.mensaje = mensaje;
        }
    }
}
