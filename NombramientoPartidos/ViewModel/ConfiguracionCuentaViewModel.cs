using NombramientoPartidos.Services;
using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NombramientoPartidos.ViewModel
{
    public enum AccionAdministrar
    {
        ContraseñaCambiada,
        SinContraseñaCambiada
    }
    class ConfiguracionCuentaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Administrador AdministradorActual{ get; set; }

        public string ContraseñaNueva { get; set; }

        public string ContraseñaConfirmar { get; set; }

        public string ContraseñaActual { get; set; }

        public AccionAdministrar Accion { get; set; }

        public ConfiguracionCuentaViewModel(Administrador administrador)
        {
            this.AdministradorActual = administrador;
            Accion = AccionAdministrar.SinContraseñaCambiada;
        }

        public bool UpdateAdministrador()
        {
            if(Accion == AccionAdministrar.ContraseñaCambiada)
            {
                ComprobarContraseña();
                return ApiRest.UpdateAdministrador(AdministradorActual);
            }
            else
            {
                return ApiRest.UpdateAdministrador(AdministradorActual);
            }
           
        }
        private bool ComprobarContraseña()
        {
            if (Utils.EncriptarEnSHA1(ContraseñaActual) == AdministradorActual.Pass)
            {
                if (ContraseñaNueva == ContraseñaConfirmar)
                {
                    ValidacionesRegexp.ValidarPass(ContraseñaNueva);
                    AdministradorActual.Pass = Utils.EncriptarEnSHA1(ContraseñaNueva);
                    return true;
                }
                else
                {
                    throw new CRUDException("La contraseña nueva introducida no coincide con la contraseña introducida para confirmar");
                }
            }
            else
            {
                throw new CRUDException("La contraseña del " + AdministradorActual.Nombre_Completo + " escrita no es correcta");
            }
        }
    }
}
