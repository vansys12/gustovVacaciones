using BackEndAPI.Models;
namespace BackEndAPI.Services.Contrato
{
    public interface IEmpleadoService
    {
        Task<List<Empleado>> GetList();
        Task<Empleado> Get(int idEmpleado);
        Task<Empleado> Add(Empleado modelo);
        Task<bool> Update(Empleado modelo);
        Task<bool> Delete(Empleado modelo);
        Task<int> CalcularDiasVacaciones(int idEmpleado, DateTime fechaInicio, DateTime fechaFin);
    }
}
