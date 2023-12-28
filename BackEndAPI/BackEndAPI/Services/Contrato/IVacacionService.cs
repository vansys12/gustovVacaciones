using BackEndAPI.Models;

namespace BackEndAPI.Services.Contrato
{
    public interface IVacacionService
    {
        Task<List<Vacacion>> GetList();
        Task<Vacacion> Get(int idVacacion);
        Task<Vacacion> Add(Vacacion modelo);
        Task<bool> Update(Vacacion modelo);
        Task<bool> Delete(Vacacion modelo);
        Task<int> GetDiasVacacionesEmpleado(int idVacacion);
    }
}
