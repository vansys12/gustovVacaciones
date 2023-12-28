using Microsoft.EntityFrameworkCore;
using BackEndAPI.Models;
using BackEndAPI.Services.Contrato;

namespace BackEndAPI.Services.Implemetacion
{

    public class DepartamentoService : IDepartamentoService
    {
        //Contexto hacia la base de datos
        private DbgustovContext _dbContext;
        public DepartamentoService(DbgustovContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Departamento>> GetList()
        {
            try
            {
                List<Departamento> lista = new List<Departamento>();
                lista= await _dbContext.Departamentos.ToListAsync();
                return lista;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
    }
}
