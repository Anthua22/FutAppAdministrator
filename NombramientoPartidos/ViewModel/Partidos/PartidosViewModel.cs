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

        public AccionCategoria AccionCategoriaTrabajo { get; set; }

        public ObservableCollection<string> Provincias { get; set; }


        public string Categoria { get; set; }

        public PartidosViewModel()
        {
            EquiposLocales = new ObservableCollection<Equipo>();
            EquiposVisitantes = new ObservableCollection<Equipo>();
            PartidoUso = new Partido()
            {
                ArbitroPrincipal = new Arbitro(),
                ArbitroSecundario = new Arbitro(),
                Cronometrador = new Arbitro(),
                Tercer_Arbitro = new Arbitro(),
                EquipoVisitante = new Equipo(),
                EquipoLocal = new Equipo(),
                Fecha_Encuentro = DateTime.Now
                
            }; 
            Categorias = Utils.Categorias;
            Provincias = Utils.Provincias;
        }

        public void ObtenerEquiposyArbitrosPrincipales()
        {
            if(Categoria.Equals("1º División") || Categoria.Equals("2º División"))
            {
                AccionCategoriaTrabajo = AccionCategoria.PrimeraySegunda;
            }
            else if(Categoria.Equals("Regional") || Categoria.Equals("Preferente"))
            {
                AccionCategoriaTrabajo = AccionCategoria.RegionalyPreferente;
            }
            else if(Categoria.Equals("3º División") || Categoria.Equals("2ºB División"))
            {
                AccionCategoriaTrabajo = AccionCategoria.TerceraySegundaB;
            }
            else
            {
                AccionCategoriaTrabajo = AccionCategoria.FutbolBase;
            }
            EquiposLocales = Utils.FiltroEquipos(Categoria);
            ArbitrosPrincipales = Utils.FiltroArbitros(Categoria);
            
        }


        public void ObtenerArbitrosSecundarios()
        {
            ArbitrosSecundarios = new ObservableCollection<Arbitro>(Utils.FiltroArbitros(Categoria).Where(x => x.Dni != PartidoUso.ArbitroPrincipal.Dni));
        }

        public void CambiaAccionCategoria(AccionCategoria accionCategoria)
        {
            AccionCategoriaTrabajo = accionCategoria;
        }

        public void ObtenerCronometradores()
        {
            switch (AccionCategoriaTrabajo)
            {
                case AccionCategoria.PrimeraySegunda:
                    Cronometradores = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x => !x.Categoria.Equals("Preferente") && !x.Categoria.Equals("Regional") && !x.Categoria.Equals("Fútbol Base") && !x.Categoria.Equals("3º División") && x.Dni!=PartidoUso.ArbitroPrincipal.Dni && x.Dni!=PartidoUso.ArbitroSecundario.Dni).OrderByDescending(y=>y.Categoria));
                    int u =PartidoUso.Fecha_Encuentro.Hour;
                    break;

                case AccionCategoria.TerceraySegundaB:
                    Cronometradores = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x => !x.Categoria.Equals("Fútbol Base") && !x.Categoria.Equals("Preferente")).OrderByDescending(y=>y.Categoria));
                    break;
                case AccionCategoria.RegionalyPreferente:
                    Cronometradores = new ObservableCollection<Arbitro>(ApiRest.RescatarArbitros().Where(x => x.Categoria.Equals("Fútbol Base")).OrderBy(y=>y.Nombre_Completo));
                    break;
                case AccionCategoria.FutbolBase:
                    Cronometradores = new ObservableCollection<Arbitro>();
                    break;
            }
        }

        public void ObtenerEquiposVisitantes()
        {
            EquiposVisitantes = new ObservableCollection<Equipo>(Utils.FiltroEquipos(Categoria).Where(x => x.Nombre != PartidoUso.EquipoLocal.Nombre));
        }
    }
}
