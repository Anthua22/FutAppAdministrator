﻿using NombramientoPartidos.Services;
using NombramientoPartidos.Utilidades;
using NombramientoPartidos.Utilidades.ClasesPojos;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace NombramientoPartidos.ViewModel.JugadoresStaff
{
    class InsertarStaffViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> Cargos { get; set; }

        public ObservableCollection<string> Categorias { get; set; }

        public ObservableCollection<Equipo> Equipos { get; set; }

        public Staff StaffInsert { get; set; }

        public Equipo EquipoStaff { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public InsertarStaffViewModel()
        {
            Cargos = Utils.CargosStaff;
            FechaNacimiento = DateTime.Now;
            Categorias = Utils.Categorias;
            Equipos = new ObservableCollection<Equipo>();
            EquipoStaff = new Equipo();
            StaffInsert = new Staff();
        }
    
        public void Filtro(string categoria)
        {
            Equipos = Utils.FiltroEquipos(categoria);
        }

        public bool Execute()
        {
            StaffInsert.Equipo = EquipoStaff.IdEquipo;
            StaffInsert.Dni = StaffInsert.Dni.ToUpper();
            StaffInsert.Fecha_Nacimiento = FechaNacimiento.Year + "-" + FechaNacimiento.Month + "-" + FechaNacimiento.Day;
            return ApiRest.InsertStaff(StaffInsert);
        }

    }

}
