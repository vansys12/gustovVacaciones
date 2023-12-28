﻿using System;
using System.Collections.Generic;

namespace BackEndAPI.Models;

public partial class Departamento
{
    public int IdDepartamento { get; set; }

    public string? Nombre { get; set; }

    public string? Estado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Empleado> Empleados { get; } = new List<Empleado>();
}
