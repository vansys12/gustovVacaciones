using System;
using System.Collections.Generic;
using BackEndAPI.Models;

namespace BackEndAPI.Models;

public partial class Vacacion
{
    public int IdVacaciones { get; set; }

    public string? Detalle { get; set; }

    public int? IdEmpleado { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public string? Estado { get; set; }
      
    public string? Gestion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public int CalcularDiasDeVacaciones()
    {
        if (IdEmpleadoNavigation != null && IdEmpleadoNavigation.FechaContrato.HasValue)
        {
            DateTime fechaIngreso = IdEmpleadoNavigation.FechaContrato.Value;
            DateTime fechaActual = DateTime.Now;
            int añosDeTrabajo = fechaActual.Year - fechaIngreso.Year;

            // Verificar si el empleado tiene menos de 1 año de antigüedad
            if (añosDeTrabajo < 1)
            {
                return 0;
            }
            // Verificar si el empleado tiene de 1 a 5 años de antigüedad
            else if (añosDeTrabajo >= 1 && añosDeTrabajo <= 5)
            {
                return 15; // 15 días de vacaciones
            }
            // Verificar si el empleado tiene de 6 a 10 años de antigüedad
            else if (añosDeTrabajo >= 6 && añosDeTrabajo <= 10)
            {
                return 20; // 20 días de vacaciones
            }
            // Si el empleado tiene más de 10 años de antigüedad
            else
            {
                return 30; // 30 días de vacaciones
            }
        }
        else
        {
            // No se pudo calcular debido a datos faltantes
            return 0;
        }
    }
}
