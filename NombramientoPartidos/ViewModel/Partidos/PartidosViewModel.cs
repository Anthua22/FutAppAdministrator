using NombramientoPartidos.Services;
using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
using NombramientoPartidos.ViewModel.Equipos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NombramientoPartidos.ViewModel.Partidos
{

    public enum AccionCategoria
    {
        PrimeraySegunda,
        TerceraySegundaB,
        RegionalyPreferente,
        FutbolBase
    }

   

    class PartidosViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public Partido PartidoUso { get; set; }

        public ObservableCollection<Equipo> EquiposLocales { get; set; }

        public ObservableCollection<Equipo> EquiposVisitantes { get; set; }

        public ObservableCollection<string> Categorias { get; set; }

        public ObservableCollection<Arbitro> ArbitrosPrincipales { get; set; }

        public ObservableCollection<Arbitro> ArbitrosSecundarios { get; set; }

        public ObservableCollection<Arbitro> Cronometradores { get; set; }

        public ObservableCollection<Arbitro> ArbitrosTerceros { get; set; }

        public ObservableCollection<EquiposTemplate> PartidosTemplates { get; set; }

        public AccionCategoria AccionCategoriaTrabajo { get; set; }

        public ObservableCollection<Partido> Partidos { get; set; }

        public ObservableCollection<string> Jornadas { get; set; }

        public Accion AccionAsignarmodificar { get; set; }

        public ObservableCollection<string> Provincias { get; set; }

        public string Hora { get; set; }

        public string Minutos { get; set; }

        public int GolesLocales { get; set; }

        public int GolesVisitantes { get; set; }

        public string Categoria { get; set; }

        public EquiposTemplate EquipoTemplate { get; set; }

        public Equipo EquipoLocal { get; set; }

        public Equipo EquipoVisitante { get; set; }

        public Arbitro ArbitroPrincipal { get; set; }

        public Arbitro ArbitroSecundario { get; set; }

        public Arbitro Cronometrador { get; set; }

        public Arbitro TercerArbitro { get; set; }

        public DateTime Fecha { get; set; }

        public PartidosViewModel()
        {
            ArbitroPrincipal = new Arbitro();
            ArbitroSecundario = new Arbitro();
            Cronometrador = new Arbitro();
            TercerArbitro = new Arbitro();
            EquipoLocal = new Equipo();
            EquipoVisitante = new Equipo();
            EquiposLocales = new ObservableCollection<Equipo>();
            ArbitrosPrincipales = new ObservableCollection<Arbitro>();
            ArbitrosSecundarios = new ObservableCollection<Arbitro>();
            Cronometradores = new ObservableCollection<Arbitro>();
            ArbitrosTerceros = new ObservableCollection<Arbitro>();
            EquiposVisitantes = new ObservableCollection<Equipo>();
            PartidosTemplates = new ObservableCollection<EquiposTemplate>();
            Partidos = new ObservableCollection<Partido>();
            Jornadas = new ObservableCollection<string>()
            {
                "1","2","3","4","5","6","7","8","9","10","11","12","13","14","15","16","17","18","19","20","21","22","23","24","25","26","27","28","29","30","31","32","33","34","35","36","37","38","39","40"
                
            };
            AccionAsignarmodificar = Accion.Nuevo;
            PartidoUso = new Partido();
            Fecha = DateTime.Now;
            Hora = "00";
            Minutos = "00";
            Categorias = Utils.Categorias;
            Provincias = Utils.Provincias;
            GolesLocales = 0;
            GolesVisitantes = 0;
       
        }

        public void ObtenerEquiposyArbitrosPrincipales()
        {
            if(Categoria!= null)
            {
                if (Categoria.Equals("1º División") || Categoria.Equals("2º División"))
                {
                    AccionCategoriaTrabajo = AccionCategoria.PrimeraySegunda;
                }
                else if (Categoria.Equals("Regional") || Categoria.Equals("Preferente"))
                {
                    AccionCategoriaTrabajo = AccionCategoria.RegionalyPreferente;
                }
                else if (Categoria.Equals("3º División") || Categoria.Equals("2ºB División"))
                {
                    AccionCategoriaTrabajo = AccionCategoria.TerceraySegundaB;
                }
                else
                {
                    AccionCategoriaTrabajo = AccionCategoria.FutbolBase;
                }

                if (AccionAsignarmodificar == Accion.Editar || AccionAsignarmodificar == Accion.Borrar)
                {
                    EquiposLocales = Utils.FiltroEquipos(Categoria);
                    EquiposVisitantes = Utils.FiltroEquipos(Categoria);
                    ArbitrosPrincipales = Utils.FiltroArbitros(Categoria);
                }
                else
                {
                    EquiposLocales = Utils.FiltroEquipos(Categoria);
                    ArbitrosPrincipales = Utils.FiltroArbitros(Categoria);
                }
            }
           
           
            
        }


        public void ObtenerArbitrosSecundarios()
        {
            if(ArbitroPrincipal!=null)
             ArbitrosSecundarios = new ObservableCollection<Arbitro>(Utils.FiltroArbitros(Categoria).Where(x => x.Dni != ArbitroPrincipal.Dni));
        }

        public void ObtenerPartidos()
        {
           
            if(AccionAsignarmodificar == Accion.Editar ||AccionAsignarmodificar == Accion.Borrar)
            { 
                Partidos = new ObservableCollection<Partido>(ApiRest.RescartarPartidos().Where(x => x.Categoria.Equals(Categoria)));
                foreach(Partido m in Partidos)
                {
                    PartidosTemplates.Add(new EquiposTemplate(EquiposLocales.Where(x => x.IdEquipo == m.EquipoLocal).First(), EquiposVisitantes.Where(y => y.IdEquipo == m.EquipoVisitante).First()));
                }

            }
            else
            {
                 Partidos = new ObservableCollection<Partido>(ApiRest.RescartarPartidos().Where(x => x.Categoria.Equals(Categoria)));
            }
            
        }
  
        public void CambioAccionCategorias()
        {
            switch (Categoria)
            {
                case "1º División":
                    AccionCategoriaTrabajo = AccionCategoria.PrimeraySegunda;
                    break;
                case "2º División":
                    AccionCategoriaTrabajo = AccionCategoria.PrimeraySegunda;
                    break;
                case "2ºB División":
                    AccionCategoriaTrabajo = AccionCategoria.TerceraySegundaB;
                    break;
                case "3º División":
                    AccionCategoriaTrabajo = AccionCategoria.TerceraySegundaB;
                    break;
                case "Preferente":
                    AccionCategoriaTrabajo = AccionCategoria.RegionalyPreferente;
                    break;
                case "Fútbol Base":
                    AccionCategoriaTrabajo = AccionCategoria.FutbolBase;
                    break;
                case "Regional":
                    AccionCategoriaTrabajo = AccionCategoria.RegionalyPreferente;
                    break;
   
            }
        }

        public void ObtenerCronometradores()
        {
            if (ArbitroPrincipal != null)
            {
                switch (AccionCategoriaTrabajo)
                {
                    case AccionCategoria.PrimeraySegunda:
                        if (ArbitroSecundario != null)
                        {
                            Cronometradores = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x => x.Categoria != null && !x.Categoria.Equals("Preferente") && !x.Categoria.Equals("Regional") && !x.Categoria.Equals("Fútbol Base") && !x.Categoria.Equals("3º División") && x.Dni != ArbitroPrincipal.Dni && x.Dni != ArbitroSecundario.Dni).OrderByDescending(y => y.Categoria));
                            if(Cronometrador != null)
                            {
                                ArbitrosTerceros = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x => x.Categoria != null && x.Categoria.Equals("2ºB División")));
                            }
                        }
                       
                        break;

                    case AccionCategoria.TerceraySegundaB:
                        
                        Cronometradores = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x => x.Categoria != null && !x.Categoria.Equals("Fútbol Base") && !x.Categoria.Equals("Preferente")).OrderByDescending(y => y.Categoria));
                        ArbitrosTerceros = new ObservableCollection<Arbitro>();
                        break;
                    case AccionCategoria.RegionalyPreferente:
                        Cronometradores = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x => x.Categoria != null && x.Categoria.Equals("Fútbol Base")).OrderBy(y => y.Nombre_Completo));
                        ArbitrosTerceros = new ObservableCollection<Arbitro>();
                        break;
                    case AccionCategoria.FutbolBase:
                        Cronometradores = new ObservableCollection<Arbitro>();
                        ArbitrosTerceros = new ObservableCollection<Arbitro>();
                        break;
                }
            }
           
        }

        public void ObtenerEquiposVisitantes()
        {
            if(EquipoLocal!=null)
                EquiposVisitantes = new ObservableCollection<Equipo>(Utils.FiltroEquipos(Categoria).Where(x => x.Nombre != EquipoLocal.Nombre));
        }

        public int Excecute()
        { 
            AsignarValorPartido();
            switch (AccionAsignarmodificar)
            {
                case Accion.Nuevo:
                   
                    ComprobarExisteJornada();
                    PonerBienFecha();   
                    ApiRest.InsertPartido(PartidoUso);
                    LimpiarCampos();
                    return 1;
                case Accion.Editar:
                    PonerBienFecha();
                    CambiarResultado();
                    ApiRest.UpdatePartido(PartidoUso);
                    LimpiarCampos();
                    return 2;
                case Accion.Borrar:
                    MessageBoxResult messageresult = MessageBox.Show("Esta seguro que desea el partido de la Jornada: " + PartidoUso.Jornada + " del equipo local " + EquipoLocal.Nombre + " vs " +EquipoTemplate.EquipoVisitante.Nombre, "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (messageresult == MessageBoxResult.Yes)
                    {
                        ApiRest.DeletePartido(PartidoUso.IdPartido);
                        LimpiarCampos();
                        return 3;
                    }
                    return -1;
                default:
                    LimpiarCampos();
                    return -1; 
                  
            }     
            
        }

        private void AsignarValorPartido()
        {
            if(AccionAsignarmodificar == Accion.Nuevo)
            {
                PartidoUso.EquipoLocal = EquipoLocal.IdEquipo;
                PartidoUso.EquipoVisitante = EquipoVisitante.IdEquipo;
            }else if(AccionAsignarmodificar == Accion.Editar || AccionAsignarmodificar == Accion.Borrar)
            {
                PartidoUso.EquipoLocal = EquipoTemplate.EquipoLocal.IdEquipo;
                PartidoUso.EquipoVisitante = EquipoTemplate.EquipoVisitante.IdEquipo;
            }
           
            PartidoUso.ArbitroPrincipal = ArbitroPrincipal.Id;
            PartidoUso.ArbitroSecundario = ArbitroSecundario.Id;
            PartidoUso.Tercer_Arbitro = TercerArbitro.Id;
            PartidoUso.Cronometrador = Cronometrador.Id;
            PartidoUso.Tercer_Arbitro = TercerArbitro.Id;
            PartidoUso.Provincia = EquipoLocal.Provincia;
            PartidoUso.Localidad = EquipoLocal.Localidad;
            PartidoUso.Direccion_Encuentro = EquipoLocal.Direccion_Campo;
            PartidoUso.Categoria = Categoria;
        }

        private void PonerBienFecha()
        {
           
            string[] numero = Fecha.ToShortDateString().Split('/');
            PartidoUso.Fecha_Encuentro = numero[2]+'-'+numero[1]+'-'+numero[0];
            string horaencuentro = Hora + ":" + Minutos;
            ValidacionesRegexp.ComprobarHoraMinutos(horaencuentro);
            PartidoUso.Fecha_Encuentro += " " + horaencuentro+ ":00";
        }

        private void ObtenerFecha()
        {
            string[] fechahora = PartidoUso.Fecha_Encuentro.Split(' ');
            string[] diamesaño = fechahora[0].Split('-');
            string[] horaminutos = fechahora[1].Split(':');

            Fecha = new DateTime(int.Parse(diamesaño[0]), int.Parse(diamesaño[1]), int.Parse(diamesaño[2]));
            Hora = horaminutos[0];
            Minutos = horaminutos[1];
        }

        public void ObtenerDatosPartidoUpdate()
        {
            PartidoUso = Partidos.Where(x => x.EquipoLocal == EquipoTemplate.EquipoLocal.IdEquipo && x.EquipoVisitante == EquipoTemplate.EquipoVisitante.IdEquipo).First(); 
            EquipoLocal = EquiposLocales.Where(x => x.IdEquipo == PartidoUso.EquipoLocal).First();
            EquipoLocal.Localidad = PartidoUso.Localidad; 
            EquipoLocal.Direccion_Campo = PartidoUso.Direccion_Encuentro;   
            ArbitroPrincipal = ArbitrosPrincipales.Where(x => x.Id == PartidoUso.ArbitroPrincipal).First();
            if(AccionCategoriaTrabajo == AccionCategoria.PrimeraySegunda)
            {
                ArbitroSecundario = ArbitrosSecundarios.Where(x => x.Id == PartidoUso.ArbitroSecundario).First();
                Cronometrador = Cronometradores.Where(x => x.Id == PartidoUso.Cronometrador).First();
                TercerArbitro = ArbitrosTerceros.Where(x => x.Id == PartidoUso.Tercer_Arbitro).First();
            }else if(AccionCategoriaTrabajo == AccionCategoria.TerceraySegundaB)
            {
                ArbitroSecundario = ArbitrosSecundarios.Where(x => x.Id == PartidoUso.ArbitroSecundario).First();
                Cronometrador = Cronometradores.Where(x => x.Id == PartidoUso.Cronometrador).First();
            }else if(AccionCategoriaTrabajo == AccionCategoria.RegionalyPreferente)
            {
                Cronometrador = Cronometradores.Where(x => x.Id == PartidoUso.Cronometrador).First();
            }
            ObtenerFecha();
            ObtenerResultado();


        }

        public void LimpiarCampos()
        {
            ArbitroPrincipal = new Arbitro();
            ArbitroSecundario = new Arbitro();
            Cronometrador = new Arbitro();
            TercerArbitro = new Arbitro();
            EquipoLocal = new Equipo();
            EquipoVisitante = new Equipo();
            EquipoTemplate = new EquiposTemplate();
            PartidoUso = new Partido();
            Categoria = null;
            EquiposLocales.Clear();
            ArbitrosPrincipales.Clear();
            ArbitrosSecundarios.Clear();
            Cronometradores.Clear();
            ArbitrosTerceros.Clear();
            EquiposVisitantes.Clear();
            PartidosTemplates.Clear();
            Partidos.Clear();
            Fecha = DateTime.Now;
            Hora = "00";
            Minutos = "00";            
        }

        private void ComprobarExisteJornada()
        {

            if(Partidos.Where(x=>x.EquipoLocal == PartidoUso.EquipoLocal && x.EquipoVisitante == PartidoUso.EquipoVisitante && x.Jornada == PartidoUso.Jornada || x.EquipoLocal == PartidoUso.EquipoLocal && x.EquipoVisitante == PartidoUso.EquipoVisitante).Count() > 0)
            {
                throw new Exception("El partido a asignar ya se ha asignado");
            }
        }

        private void ObtenerResultado()
        {
            if(!string.IsNullOrWhiteSpace(PartidoUso.Resultado))
            {
                string[] resultado = PartidoUso.Resultado.Split('-');
                GolesLocales = int.Parse(resultado[0]);
                GolesVisitantes = int.Parse(resultado[1]);
            }
            
        }

        private void CambiarResultado()
        {
            PartidoUso.Resultado = GolesLocales + "-" + GolesVisitantes;
        }


    }
}
 