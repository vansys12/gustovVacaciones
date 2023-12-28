using BackEndAPI.Models;

namespace BackEndAPI.Services.Contrato
{
    public interface ICargoService
    {
        Task<List<Cargo>> GetList();
    }
}
