using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IMovimientoService
    {
        Task<Movimiento> GetAsync(int idmovimiento);
        Task<ResponseWebApi> SaveAsync(MovimientoInsertDTO movimiento);
        Task<List<Movimiento>> GetEstadoCuentaAsync(int clienteid, DateTime fechaInicio, DateTime fechaFin);
    }
}
