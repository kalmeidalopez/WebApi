using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public interface ICuentaService
    {
        Task<Cuenta> GetAsync(int idcuenta);
        Task<ResponseWebApi> SaveAsync(CuentaInsertDTO cuenta);
        Task<ResponseWebApi> UpdateAsync(CuentaUpdateDTO cuenta);
        Task<ResponseWebApi> DeleteAsync(int idcuenta);
    }
}
