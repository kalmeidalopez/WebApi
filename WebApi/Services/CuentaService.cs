using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DAL;
using WebApi.Models;

namespace WebApi.Services
{
    public class CuentaService : ICuentaService
    {
        Context context;
        public CuentaService(Context dbcontext)
        {
            context = dbcontext;
        }      
        public Task<Cuenta> GetAsync(int idcuenta)
        {
            var cuenta = context.Cuentas.Find(idcuenta);
            return Task.FromResult(cuenta);
        }
        public async Task<ResponseWebApi> SaveAsync(CuentaInsertDTO cuenta)
        {
            ResponseWebApi response = new ResponseWebApi();
            try
            {
                response.StatusCode = System.Net.HttpStatusCode.OK;
                int maxid = 1;
                if (context.Cuentas.Any())
                    maxid = context.Cuentas.Max(p => p.IdCuenta) + 1;
                Cuenta model = new Cuenta();
                model.IdCuenta = maxid;
                model.Clienteid = cuenta.Clienteid;
                model.Estado = true;
                model.NumeroCuenta = cuenta.NumeroCuenta;
                model.SaldoInicial = cuenta.SaldoInicial;
                model.TipoCuenta = cuenta.TipoCuenta;
                model.FechaIngreso = DateTime.Now;
                response.Id = model.IdCuenta;
                await context.AddAsync(model);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message.ToString();
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return await Task.FromResult(response);
        }
        public async Task<ResponseWebApi> UpdateAsync(CuentaUpdateDTO cuenta)
        {
            ResponseWebApi response = new ResponseWebApi();
            try
            {
                response.Id = cuenta.IdCuenta;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                var cuentaup = context.Cuentas.Find(cuenta.IdCuenta);
                if (cuentaup != null)
                {
                    cuentaup.SaldoInicial = cuenta.SaldoInicial;
                    await context.SaveChangesAsync();
                }
                else
                {
                    response.Message = "Cuenta no existe";
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message.ToString();
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return await Task.FromResult(response);
}
        public async Task<ResponseWebApi> DeleteAsync(int idcuenta)
        {
            ResponseWebApi response = new ResponseWebApi();
            try
            {
                response.Id = idcuenta;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                var cuentasdel = context.Cuentas.Find(idcuenta);
                if (cuentasdel != null)
                {
                    cuentasdel.Estado = false;
                    //context.Remove(cuentasdel);
                    await context.SaveChangesAsync();
                }
                else
                {
                    response.Message = "Cuenta no existe";
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message.ToString();
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return await Task.FromResult(response);
}
    }
}
