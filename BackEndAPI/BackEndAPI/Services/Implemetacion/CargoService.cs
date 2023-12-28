using Microsoft.EntityFrameworkCore;
using BackEndAPI.Models;
using BackEndAPI.Services.Contrato;

namespace BackEndAPI.Services.Implemetacion
{
    public class CargoService : ICargoService
    {
        private DbgustovContext _dbcontext;

        public CargoService(DbgustovContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<Cargo>> GetList()
        {
            try
            {
                List<Cargo> lista = new List<Cargo>();
                lista = await _dbcontext.Cargos.ToListAsync();
                return lista;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
    }
}
