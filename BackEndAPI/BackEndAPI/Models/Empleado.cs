using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackEndAPI.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string? NombreCompleto { get; set; }

    public int? IdDepartamento { get; set; }

    public int? IdCargo { get; set; }

    public int? Sueldo { get; set; }

    public string? Estado { get; set; }

    public DateTime? FechaContrato { get; set; }

    public DateTime? FechaCreacion { get; set; }
    [JsonIgnore]
    public virtual Cargo? IdCargoNavigation { get; set; }
    [JsonIgnore]
    public virtual Departamento? IdDepartamentoNavigation { get; set; }

    public virtual ICollection<Vacacion> Vacacions { get; } = new List<Vacacion>();



    
}
