﻿using AutoMapper;
using BackEndAPI.DTOs;
using BackEndAPI.Models;
using System.Globalization;

namespace BackEndAPI.Utilidades
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            #region Departamento
            CreateMap<Departamento,DepartamentoDTO>().ReverseMap();
            #endregion
            #region Cargo
            CreateMap<Cargo,CargoDTO>().ReverseMap();
            #endregion
            #region Empleado
            CreateMap<Empleado, EmpleadoDTO>()
                .ForMember(destino => destino.NombreDepartamento,
                opt => opt.MapFrom(origen => origen.IdDepartamentoNavigation.Nombre)
                )
                .ForMember(destino => destino.NombreCargo,
                opt => opt.MapFrom(origen => origen.IdCargoNavigation.Nombre)
                )
                .ForMember(destino => destino.FechaContrato,
                opt => opt.MapFrom(origen => origen.FechaContrato.Value.ToString("dd/MM/yyyy"))
                );

            CreateMap<EmpleadoDTO, Empleado>()
                .ForMember(destino => destino.IdDepartamentoNavigation,
                opt => opt.Ignore()
                )
                .ForMember(destino => destino.IdCargoNavigation,
                opt => opt.Ignore()
                )
                .ForMember(destino => destino.FechaContrato,
                opt => opt.MapFrom(origen => DateTime.ParseExact(origen.FechaContrato, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                );
                
            #endregion
            #region Vacacion
            CreateMap<Vacacion,VacacionDTO>()
                .ForMember(destino => destino.NombreEmpleado,
                opt => opt.MapFrom(origen => origen.IdEmpleadoNavigation.NombreCompleto)
                )
                .ForMember(destino => destino.FechaInicio,
                opt => opt.MapFrom(origen => origen.FechaInicio.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(destino => destino.FechaFin,
                opt => opt.MapFrom(origen => origen.FechaFin.Value.ToString("dd/MM/yyyy"))
                );

            CreateMap<VacacionDTO, Vacacion>()
               .ForMember(destino => destino.IdEmpleadoNavigation,
               opt => opt.Ignore()
               )
               .ForMember(destino => destino.FechaInicio,
               opt => opt.MapFrom(origen => DateTime.ParseExact(origen.FechaInicio, "dd/MM/yyyy", CultureInfo.InvariantCulture))
               )
               .ForMember(destino => destino.FechaFin,
               opt => opt.MapFrom(origen => DateTime.ParseExact(origen.FechaFin, "dd/MM/yyyy", CultureInfo.InvariantCulture))
               );
            #endregion
        }
    }
}
