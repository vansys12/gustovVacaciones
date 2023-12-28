namespace BackEndAPI.DTOs
{
    public class VacacionDTO
    {
        public int IdVacaciones { get; set; }

        public string? Detalle { get; set; }

        public int? IdEmpleado { get; set; }
        public string? NombreEmpleado { get; set; }

        public string? FechaInicio { get; set; }

        public string? FechaFin { get; set; }

        public string? Estado { get; set; }

        public string? Gestion { get; set; }
    }
}
