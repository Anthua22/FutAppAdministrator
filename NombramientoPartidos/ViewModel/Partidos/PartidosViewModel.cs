using NombramientoPartidos.Services;
using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NombramientoPartidos.ViewModel.Partidos
{

    public enum AccionCategoria
    {
        PrimeraySegunda,
        TerceraySegundaB,
        RegionalyPreferente,
        FutbolBase
    }

    public enum Accion
    {
        Asignar,
        Modificar
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

        public ObservableCollection<int> Jornadas { get; set; }

        public Accion AccionAsignarmodificar { get; set; }

        public ObservableCollection<string> Provincias { get; set; }

        public string Hora { get; set; }

        public string Minutos { get; set; }

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
            Jornadas = new ObservableCollection<int>()
            {
                1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40
                
            };
            AccionAsignarmodificar = Accion.Asignar;
            PartidoUso = new Partido();
            Fecha = DateTime.Now;
            Hora = "00";
            Minutos = "00";
            Categorias = Utils.Categorias;
            Provincias = Utils.Provincias;
       
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

                if (AccionAsignarmodificar == Accion.Modificar)
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
           
            if(AccionAsignarmodificar == Accion.Modificar)
            { 
                Partidos = new ObservableCollection<Partido>(ApiRest.RescartarPartidos().Where(x => x.Categoria.Equals(Categoria)));
                foreach(Partido m in Partidos)
                {
                    PartidosTemplates.Add(new EquiposTemplate(EquiposLocales.Where(x => x.IdEquipo == m.EquipoLocal).First(), EquiposVisitantes.Where(y => y.IdEquipo == m.EquipoVisitante).First()));
                }

                
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
            switch (AccionCategoriaTrabajo)
            {
                case AccionCategoria.PrimeraySegunda:
                    Cronometradores = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x => !x.Categoria.Equals("Preferente") && !x.Categoria.Equals("Regional") && !x.Categoria.Equals("Fútbol Base") && !x.Categoria.Equals("3º División") && x.Dni!=ArbitroPrincipal.Dni && x.Dni!=ArbitroSecundario.Dni).OrderByDescending(y=>y.Categoria));
                    ArbitrosTerceros = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x=>x.Categoria.Equals("2ºB División")));
                    break;

                case AccionCategoria.TerceraySegundaB:
                    Cronometradores = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x => !x.Categoria.Equals("Fútbol Base") && !x.Categoria.Equals("Preferente")).OrderByDescending(y=>y.Categoria));
                    ArbitrosTerceros = new ObservableCollection<Arbitro>();
                    break;
                case AccionCategoria.RegionalyPreferente:
                    Cronometradores = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x => x.Categoria.Equals("Fútbol Base")).OrderBy(y=>y.Nombre_Completo));
                    ArbitrosTerceros = new ObservableCollection<Arbitro>();
                    break;
                case AccionCategoria.FutbolBase:
                    Cronometradores = new ObservableCollection<Arbitro>();
                    ArbitrosTerceros = new ObservableCollection<Arbitro>();
                    break;
            }
        }

        public void ObtenerEquiposVisitantes()
        {
            if(EquipoLocal!=null)
                EquiposVisitantes = new ObservableCollection<Equipo>(Utils.FiltroEquipos(Categoria).Where(x => x.Nombre != EquipoLocal.Nombre));
        }

        public int Excecute()
        {
            switch (AccionAsignarmodificar)
            {
                case Accion.Asignar:
                    AsignarValorPartido();
                    PonerBienFecha();   
                    ApiRest.InsertPartido(PartidoUso);
                    LimpiarCampos();
                    return 1;
                case Accion.Modificar:
                    return 2;
                default:
                    LimpiarCampos();
                    return -1; 
                  
            }
           
          
        }

        private void AsignarValorPartido()
        {
            PartidoUso.EquipoLocal = EquipoLocal.IdEquipo;
            PartidoUso.EquipoVisitante = EquipoVisitante.IdEquipo;
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
            PartidoUso = new Partido() { Jornada = 0};
            Categoria = null;
            EquiposLocales.Clear();
            ArbitrosPrincipales.Clear();
            ArbitrosSecundarios.Clear();
            Cronometradores.Clear();
            ArbitrosTerceros.Clear();
            EquiposVisitantes.Clear();
            PartidosTemplates.Clear();
            Partidos.Clear();


            
        }

        private void ComprobarExisteJornada()
        {
            //Partidos.Where(x=>x.)
        }

    }
}
 