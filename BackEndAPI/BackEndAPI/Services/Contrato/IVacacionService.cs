using BackEndAPI.Models;

namespace BackEndAPI.Services.Contrato
{
    public interface IVacacionService
    {
        Task<List<Vacacion>> GetList();
        Task<Empleado> Get(int idEmpleado);
        Task<Vacacion> Add(Vacacion modelo);
        Task<bool> Update(Vacacion modelo);
        Task<bool> Delete(Vacacion modelo);
        Task<int> GetDiasVacacionesEmpleado(int idEmpleado);
    }
}
