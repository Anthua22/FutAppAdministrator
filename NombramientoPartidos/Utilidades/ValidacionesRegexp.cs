using System.Text.RegularExpressions;

namespace NombramientoPartidos.Utilidades
{
    public static class ValidacionesRegexp
    {
        private static readonly Regex patronUser = new Regex(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");
        private static readonly Regex patronPass = new Regex(@"^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040])(?=.*[A-Z])(?=.*[a-z])\S{8,16}$");
        private static readonly Regex patronFecha = new Regex(@"^\d{4}([\-/.])(0?[1-9]|1[1-2])\1(3[01]|[12][0-9]|0?[1-9])$");
        private static readonly Regex patronTelefono = new Regex(@"^\d{9}$");
        private static readonly Regex patronIdentidad = new Regex(@"^\w{9}$");

        public static bool ValidarEmail(string email)
        {
            if (patronUser.IsMatch(email) && !string.IsNullOrWhiteSpace(email))
            {
                return true;
            }
            return false;
        }

        public static bool ValidarPass(string pass)
        {
            if (patronPass.IsMatch(pass) && !string.IsNullOrWhiteSpace(pass))
            {
                return true;
            }
            else
            {
                throw new PassException("La contraseña debe contener al menos 8 carácteres incluyendo una mayúscula y un carácter especial");
            }
        }

        public static bool ValidarFecha(string fecha)
        {
            if (patronFecha.IsMatch(fecha))
            {
                return true;
            }
            else
            {
                throw new FechaException("La fecha no tiene el formato correcto: 'aaaa-mm-dd', 'aaaa/mm/dd','aaaa.mm.dd'");
            }
        }

        public static bool ValidarTelefono(string numero)
        {
            if (patronTelefono.IsMatch(numero))
            {
                return true;
            }
            else
            {
                throw new TelefonoException("El número de teléfono solo tiene que tener número y una longitud de 9 números");
            }
        }

        public static bool ValidarDniNie(string dniNie)
        {
            if (patronIdentidad.IsMatch(dniNie))
            {
                return true;
            }
            else
            {
                throw new TelefonoException("El DNI o NIE tiene que tener una longitud de 9 caracteres");
            }
        }

        public static bool ComprobarCategoria(string categoria)
        {
            if (Utils.Categorias.Contains(categoria))
            {
                return true;
            }
            throw new CategoriaException("La categoría introducida no existe");   
        }
    }
}
