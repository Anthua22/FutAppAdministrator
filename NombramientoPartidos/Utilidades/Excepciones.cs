using System;

namespace NombramientoPartidos.Utilidades
{
    public class CRUDException:Exception
    {
        string mensaje;
        public CRUDException(string mensaje) : base(mensaje)
        {
            this.mensaje = mensaje;
        }
    }

    public class PassException : Exception
    {
        string mensaje;
        public PassException(string mensaje) : base(mensaje)
        {
            this.mensaje = mensaje;
        }
    }

    public class EmailException : Exception
    {
        string mensaje;
        public EmailException(string mensaje) : base(mensaje)
        {
            this.mensaje = mensaje;
        }
    }

    public class FechaException : Exception
    {
        string mensaje;
        public FechaException(string mensaje) : base(mensaje)
        {
            this.mensaje = mensaje;
        }
    }

    public class CategoriaException : Exception
    {
        string mensaje;
        public CategoriaException(string mensaje) : base(mensaje)
        {
            this.mensaje = mensaje;
        }
    }

    public class TelefonoException : Exception
    {
        string mensaje;
        public TelefonoException(string mensaje) : base(mensaje)
        {
            this.mensaje = mensaje;
        }
    }

    public class DniNie : Exception
    {
        string mensaje;
        public DniNie(string mensaje) : base(mensaje)
        {
            this.mensaje = mensaje;
        }
    }
}
