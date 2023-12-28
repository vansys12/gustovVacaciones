﻿namespace BackEndAPI.DTOs
{
    public class EmpleadoDTO
    {
        public int IdEmpleado { get; set; }

        public string? NombreCompleto { get; set; }

        public int? IdDepartamento { get; set; }

        public string? NombreDepartamento { get; set; }

        public int? IdCargo { get; set; }

        public string? NombreCargo { get; set; }

        public int? Sueldo { get; set; }

        public string? Estado { get; set; }

        public string? FechaContrato { get; set; }
    }
}
