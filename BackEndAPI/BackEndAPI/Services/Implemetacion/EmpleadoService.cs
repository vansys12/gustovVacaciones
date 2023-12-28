using Microsoft.EntityFrameworkCore;
using BackEndAPI.Models;
using BackEndAPI.Services.Contrato;

namespace BackEndAPI.Services.Implemetacion
{
    public class EmpleadoService : IEmpleadoService
    {
        private DbgustovContext _dbContext;

        public EmpleadoService(DbgustovContext dbContext)
        {
                _dbContext=dbContext;
        }

        public async Task<List<Empleado>> GetList()
        {
            try
            {
                List<Empleado> lista = new List<Empleado>();
                lista = await _dbContext.Empleados
                    .Include(dpt => dpt.IdDepartamentoNavigation)
                    .Include (crg=>crg.IdCargoNavigation)
                    .ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Empleado> Get(int idEmpleado)
        {
            try
            {
                Empleado? encontrado = new Empleado();
                encontrado=await _dbContext.Empleados
                    .Include(dpt=>dpt.IdDepartamentoNavigation)
                    .Include(crg=>crg.IdCargoNavigation)
                    .Where(e=>e.IdEmpleado==idEmpleado).FirstOrDefaultAsync();

                return encontrado;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Empleado> Add(Empleado modelo)
        {
            try
            {
                _dbContext.Empleados.Add(modelo);
                await _dbContext.SaveChangesAsync();
                return modelo;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
               
        public async Task<bool> Update(Empleado modelo)
        {
            try
            {
                _dbContext.Empleados.Update(modelo);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> Delete(Empleado modelo)
        {
            try
            {
                _dbContext.Empleados.Remove(modelo);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> CalcularDiasVacaciones(int idEmpleado, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                // Obtener el empleado por su ID
                var empleado = await _dbContext.Empleados.FindAsync(idEmpleado);

                if (empleado == null)
                {
                    throw new Exception("Empleado no encontrado.");
                }

                // Calcular los años de servicio
                int aniosServicio = DateTime.Now.Year - empleado.FechaContrato.Value.Year;
                if (DateTime.Now.Month < empleado.FechaContrato.Value.Month ||
                   (DateTime.Now.Month == empleado.FechaContrato.Value.Month && DateTime.Now.Day < empleado.FechaContrato.Value.Day))
                {
                    aniosServicio--;
                }

                // Calcular los días de vacaciones en base a los años de servicio
                int diasVacaciones = CalcularDiasDeVacaciones(aniosServicio);

                // Calcular los días hábiles entre las fechas de inicio y fin, excluyendo feriados y domingos
                int diasHabiles = ContarDiasHabiles(fechaInicio, fechaFin);

                // Limitar los días de vacaciones a los días hábiles calculados
                if (diasHabiles < diasVacaciones)
                {
                    diasVacaciones = diasHabiles;
                }

                return diasVacaciones;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error al calcular los días de vacaciones: " + ex.Message);
            }
        }

        private int CalcularDiasDeVacaciones(int aniosServicio)
        {
            if (aniosServicio >= 1 && aniosServicio <= 5)
            {
                return 15; // 1-5 años de servicio
            }
            else if (aniosServicio >= 6 && aniosServicio <= 10)
            {
                return 20; // 6-10 años de servicio
            }
            else if (aniosServicio > 10)
            {
                return 30; // Más de 10 años de servicio
            }

            return 0; // Para casos en los que el empleado tenga menos de 1 año de servicio
        }

        private int ContarDiasHabiles(DateTime fechaInicio, DateTime fechaFin)
        {
            int diasHabiles = 0;
            DateTime fechaActual = fechaInicio;

            while (fechaActual <= fechaFin)
            {
                if (EsDiaHabil(fechaActual))
                {
                    diasHabiles++;
                }
                fechaActual = fechaActual.AddDays(1);
            }

            return diasHabiles;
        }

        private bool EsDiaHabil(DateTime fecha)
        {
            return fecha.DayOfWeek != DayOfWeek.Sunday && !EsFeriadoNacional(fecha);
        }

        private bool EsFeriadoNacional(DateTime fecha)
        {
            // Lista de feriados nacionales con sus respectivas fechas
            Dictionary<DateTime, string> feriadosNacionales = new Dictionary<DateTime, string>
    {
        { new DateTime(fecha.Year, 1, 1), "Año Nuevo" },
        { new DateTime(fecha.Year, 1, 22), "Día del Estado Plurinacional" },
        { new DateTime(fecha.Year, 2, 28), "Carnaval" },
        { new DateTime(fecha.Year, 3, 1), "Carnaval" },
        { new DateTime(fecha.Year, 4, 15), "Viernes Santo" },
        { new DateTime(fecha.Year, 5, 1), "Día del Trabajo" },
        { new DateTime(fecha.Year, 5, 2), "Feriado del Día del Trabajo" },
        { new DateTime(fecha.Year, 6, 16), "Corpus Christi" },
        { new DateTime(fecha.Year, 6, 21), "Año Nuevo Aymara" },
        { new DateTime(fecha.Year, 8, 6), "Día de la Independencia" },
        { new DateTime(fecha.Year, 11, 2), "Día de Todos los Difuntos" },
        { new DateTime(fecha.Year, 12, 25), "Navidad" },
        { new DateTime(fecha.Year, 12, 26), "Feriado de Navidad" }
    };

            // Verificar si la fecha proporcionada está en la lista de feriados nacionales
            if (feriadosNacionales.ContainsKey(fecha.Date))
            {
                return true;
            }

            return false;
        }

        //public async Task<int> CalcularDiasVacaciones(int idEmpleado)
        //{
        //    try
        //    {
        //        // Obtener el empleado por su ID
        //        var empleado = await _dbContext.Empleados.FindAsync(idEmpleado);

        //        if (empleado == null)
        //        {
        //            throw new Exception("Empleado no encontrado.");
        //        }

        //        // Calcular los años de servicio
        //        int aniosServicio = DateTime.Now.Year - empleado.FechaContrato.Value.Year;
        //        if (DateTime.Now.Month < empleado.FechaContrato.Value.Month ||
        //           (DateTime.Now.Month == empleado.FechaContrato.Value.Month && DateTime.Now.Day < empleado.FechaContrato.Value.Day))
        //        {
        //            aniosServicio--;
        //        }

        //        // Calcular los días de vacaciones en base a los años de servicio
        //        if (aniosServicio >= 1 && aniosServicio <= 5)
        //        {
        //            return 15; // 1-5 años de servicio
        //        }
        //        else if (aniosServicio >= 6 && aniosServicio <= 10)
        //        {
        //            return 20; // 6-10 años de servicio
        //        }
        //        else if (aniosServicio > 10)
        //        {
        //            return 30; // Más de 10 años de servicio
        //        }

        //        return 0; // Para casos en los que el empleado tenga menos de 1 año de servicio
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de excepciones
        //        // Podrías optar por manejar la excepción de manera diferente dependiendo de tus necesidades
        //        throw new Exception("Error al calcular los días de vacaciones: " + ex.Message);
        //    }
        //}



    }
}
