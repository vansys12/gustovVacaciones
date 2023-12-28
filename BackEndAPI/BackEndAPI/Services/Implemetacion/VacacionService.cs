using Microsoft.EntityFrameworkCore;
using BackEndAPI.Models;
using BackEndAPI.Services.Contrato;
using Microsoft.AspNetCore.Routing.Tree;
using Microsoft.AspNetCore.Mvc;

namespace BackEndAPI.Services.Implemetacion
{
    public class VacacionService : IVacacionService
    {

        private DbgustovContext _dbcontext;
        public VacacionService(DbgustovContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<Vacacion>> GetList()
        {
            try
            {
                List<Vacacion> lista = new List<Vacacion>();
                lista = await _dbcontext.Vacacions.Include(mpl => mpl.IdEmpleadoNavigation).ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Vacacion> Get(int idVacacion)
        {
            try
            {
                Vacacion? encontrado = new Vacacion();
                encontrado = await _dbcontext.Vacacions.Include(mpl => mpl.IdEmpleadoNavigation)
                    .Where(e => e.IdVacaciones == idVacacion).FirstOrDefaultAsync();
                return encontrado;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Vacacion> Add(Vacacion modelo)
        {
            try
            {
                _dbcontext.Vacacions.Add(modelo);
                await _dbcontext.SaveChangesAsync();
                return modelo;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> Update(Vacacion modelo)
        {
            try
            {
                _dbcontext.Vacacions.Update(modelo);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> Delete(Vacacion modelo)
        {
            try
            {
                _dbcontext.Vacacions.Remove(modelo);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<int> GetDiasVacacionesEmpleado(int idEmpleado)
        {
            try
            {
                // Obtener todas las vacaciones del empleado especificado
                var vacaciones = await _dbcontext.Vacacions
                    .Where(v => v.IdEmpleado == idEmpleado)
                    .ToListAsync();

                if (vacaciones == null || !vacaciones.Any())
                {
                    return 0;
                }

                // Calcular la suma de los días de vacaciones
                int totalDiasVacaciones = vacaciones.Sum(v => (v.FechaFin.Value.Day - v.FechaInicio.Value.Day));

                

                return  totalDiasVacaciones;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw ex;
            }
        }


    }
}
