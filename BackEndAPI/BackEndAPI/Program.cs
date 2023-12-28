using BackEndAPI.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using BackEndAPI.Services.Contrato;
using BackEndAPI.Services.Implemetacion;
using AutoMapper;
using BackEndAPI.DTOs;
using BackEndAPI.Utilidades;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Server =.; DataBase = DBGustov; User ID = sa; Password = 1844; Trusted_Connection = true; TrustServerCertificate = true

builder.Services.AddDbContext<DbgustovContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});

builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
builder.Services.AddScoped<ICargoService, CargoService>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
builder.Services.AddScoped<IVacacionService, VacacionService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Get lista de departamentos
app.MapGet("/departamento/lista", async (
    IDepartamentoService _departamentoServicio,
    IMapper _mapper
    ) =>
{
    var listaDepartamento = await _departamentoServicio.GetList();
    var listaDepartamentoDTO = _mapper.Map<List<DepartamentoDTO>>(listaDepartamento);
    if (listaDepartamentoDTO.Count>0)
    {
        return Results.Ok(listaDepartamentoDTO);
    }
    else
    {
        return Results.NotFound();
    }
});
//Get lista de cargos
app.MapGet("/cargo/lista", async (
    ICargoService _cargoServicio,
    IMapper _mapper
    ) =>
{
    var listaCargo = await _cargoServicio.GetList();
    var listaCargoDTO = _mapper.Map<List<CargoDTO>>(listaCargo);
    if (listaCargoDTO.Count > 0)
    {
        return Results.Ok(listaCargoDTO);
    }
    else
    {
        return Results.NotFound();
    }
});
//Get lista de Empleados
app.MapGet("/empleado/lista", async (
    IEmpleadoService _empleadoServicio,
    IMapper _mapper
    ) =>
{
    var listaEmpleado = await _empleadoServicio.GetList();
    var listaEmpleadoDTO = _mapper.Map<List<EmpleadoDTO>>(listaEmpleado);
    if (listaEmpleadoDTO.Count > 0)
    {
        return Results.Ok(listaEmpleadoDTO);
    }
    else
    {
        return Results.NotFound();
    }
});
//get lista de vacaciones
app.MapGet("/vacacion/lista", async (
    IVacacionService _vacacionServicio,
    IMapper _mapper
    ) =>
{
    var listaVacacion = await _vacacionServicio.GetList();
    var listaVacacionDTO = _mapper.Map<List<VacacionDTO>>(listaVacacion);
    if (listaVacacionDTO.Count > 0)
    {
        return Results.Ok(listaVacacionDTO);
    }
    else
    {
        return Results.NotFound();
    }
});
//get Detalle empleado
app.MapGet("/empleado/{idEmpleado}", async (
    int idEmpleado, 
    IEmpleadoService _empleadoServicio) =>
{
    try
    {
        var empleado = await _empleadoServicio.Get(idEmpleado);

        if (empleado is null)
        {
            return Results.NotFound("Empleado no encontrado.");
        }

        return Results.Ok(empleado);
    }
    catch (Exception ex)
    {
        return Results.Problem("Error interno del servidor: " + ex.Message);
    }
});
//post Vacacion
app.MapPost("/vacacion/guardar", async (
    VacacionDTO modelo,
    IVacacionService _vServicio,
    IMapper _m
    ) =>
{
    var _vacacion = _m.Map<Vacacion>(modelo);
    var _vacacionCreado = await _vServicio.Add(_vacacion);

    if (_vacacionCreado.IdEmpleado != 0)
        return Results.Ok(_m.Map<VacacionDTO>(_vacacionCreado));
    else
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
});
// Post Empleado
app.MapPost("/empleado/guardar", async (
    EmpleadoDTO modelo,
    IEmpleadoService _empleadoServicio,
    IMapper _mapper
    ) =>
{
    var _empleado = _mapper.Map<Empleado>(modelo);
    var _empleadoCreado = await _empleadoServicio.Add(_empleado);

    if (_empleadoCreado.IdEmpleado != 0)
        return Results.Ok(_mapper.Map<EmpleadoDTO>(_empleadoCreado));
    else
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
});
//Update Empleado
app.MapPut("/empleado/actualizar/{idEmpleado}", async (
    int idEmpleado,
    EmpleadoDTO modelo,
    IEmpleadoService _empleadoServicio,
    IMapper _mapper
    ) =>
{
    var _e = await _empleadoServicio.Get(idEmpleado);
    if (_e is null) return Results.NotFound();
    var _empleado = _mapper.Map<Empleado>(modelo);

    _e.NombreCompleto = _empleado.NombreCompleto;
    _e.IdDepartamento = _empleado.IdDepartamento;
    _e.IdCargo = _empleado.IdCargo;
    _e.Sueldo = _empleado.Sueldo;
    _e.FechaContrato=_empleado.FechaContrato;

    var respuesta = await _empleadoServicio.Update(_e);

    if (respuesta)
    {
        return Results.Ok(_mapper.Map<EmpleadoDTO>(_e));
    }
    else
    {
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }
    
});
// Delet Empleado
app.MapDelete("/empleado/eliminar/{idEmpleado}", async (
    int idEmpleado,
    IEmpleadoService _empleadoServicio
    ) => {

        var _e = await _empleadoServicio.Get(idEmpleado);

        if (_e is null) return Results.NotFound();

        var respuesta = await _empleadoServicio.Delete(_e);

        if (respuesta)
            return Results.Ok();
        else
            return Results.StatusCode(StatusCodes.Status500InternalServerError);

    });

app.MapGet("/empleado/getDiasVacacionesTomados/{idEmpleado}", async (
    int idEmpleado,
    IVacacionService _vacacionServicio,
    IMapper _mapper
    ) =>
{
    var listaVacacion = await _vacacionServicio. GetDiasVacacionesEmpleado(idEmpleado);
   // var listaVacacionDTO = _mapper.Map<List<VacacionDTO>>(listaVacacion);
    if (listaVacacion >0)
    {
        return Results.Ok(listaVacacion);
    }
    else
    {
        return Results.NotFound("No se encontraron vacaciones para el empleado especificado.");
    }
});

app.MapGet("/empleado/getDiasVacacion/{idEmpleado}", async (
    int idEmpleado, 
    DateTime fechaInicio,
    DateTime fechaFin,
    IEmpleadoService _empleadoServicio
    ) =>
{
    try
    {
        var diasVacaciones = await _empleadoServicio.CalcularDiasVacaciones(idEmpleado,fechaInicio,fechaFin);
        return Results.Ok(new { IdEmpleado = idEmpleado, DiasVacaciones = diasVacaciones });
    }
    catch (Exception ex)
    {
        return Results.Problem("Error interno del servidor: " + ex.Message);
    }
});



app.UseCors("NuevaPolitica");
app.Run();

