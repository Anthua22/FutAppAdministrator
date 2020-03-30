using System.Text.RegularExpressions;

namespace NombramientoPartidos.Utilidades
{
    public static class ValidacionesRegexp
    {
        private static readonly Regex patronUser = new Regex(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");
        private static readonly Regex patronPass = new Regex(@"^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040])(?=.*[A-Z])(?=.*[a-z])\S{8,16}$");
        private static readonly Regex patronFecha = new Regex(@"^\d{4}([\-/.])(0?[1-9]|1[1-2])\1(3[01]|[12][0-9]|0?[1-9])$");

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
                throw new FechaException("La fecha no tiene el formato correcto: 'xxxx-xx-xx', 'xxxx/xx/xx','xxxx.xx.xx'");
            }
        }

        public static bool ComprobarCategoria(string categoria)
        {
            switch (categoria)
            {
                case "1º División":
                    return true;
                case "2º División":
                    return true;
                case "2ºB División":
                    return true;
                case "3º División":
                    return true;
                case "Preferente":
                    return true;
                case "Fútbol Base":
                    return true;
                default:
                    throw new CategoriaException("La categoría introducida no existe");
            }
        }
    }
}
