using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DAL;
using WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Services
{
    public class MovimientoService : IMovimientoService
    {
        private readonly IConfiguration _configuration;
        Context context;
        public MovimientoService(Context dbcontext, IConfiguration configuration)
        {
            context = dbcontext;
            _configuration = configuration;
        }
        public async Task DeleteAsync(int idmovimiento)
        {
            var movimientodel = context.Movimientos.Find(idmovimiento);
            if (movimientodel != null)
            {
                context.Remove(movimientodel);
                await context.SaveChangesAsync();
            }
        }

        public Task<Movimiento> GetAsync(int idmovimiento)
        {
            return context.Movimientos.Include("Cuenta").SingleOrDefaultAsync(p=>p.IdMovimiento == idmovimiento);            
        }

        public Task<List<Movimiento>> GetEstadoCuentaAsync(int clienteid, DateTime fechaInicio, DateTime fechaFin)
        {
            //var movimientos = context.Movimientos.Where(p=> 
            //    p.Cuenta.Clienteid == clienteid &&
            //    p.Fecha.Date >= fechaInicio.Date && p.Fecha.Date <= fechaFin.Date).GroupBy(p=> 
            //        p.Cuenta
            //        );
            List<Movimiento> movimientos = new List<Movimiento>();
            return Task.FromResult(movimientos.ToList());
        }

        public async Task<ResponseWebApi> SaveAsync(MovimientoInsertDTO movimiento)
        {
            ResponseWebApi response = new ResponseWebApi();
            try
            {
                var cuenta = context.Cuentas.Find(movimiento.IdCuenta);
                if (cuenta != null)
                {
                    decimal valorRetirado = 0;
                    decimal limiteDiario = 0;
                    int maxid = 1;
                    if (context.Movimientos.Any())
                        maxid = context.Movimientos.Max(p => p.IdMovimiento) + 1;   
                    if(movimiento.TipoMovimiento == "Debito")
                    {
                        limiteDiario = _configuration.GetValue<decimal>("LimiteDiario");
                        valorRetirado = context.Movimientos.Where(p => p.IdCuenta == cuenta.IdCuenta && p.Fecha.Date == DateTime.Now.Date && p.TipoMovimiento == "Debito").Sum(p => p.Valor);
                        valorRetirado += movimiento.Valor;
                        if ((limiteDiario - valorRetirado) < 0)
                        {
                            response.Message = "Cupo diario Excedido";
                        }
                        else if (cuenta.SaldoInicial - movimiento.Valor < 0 && movimiento.TipoMovimiento == "Debito")
                        {
                            response.Message = "Saldo no disponible";
                        }
                        else if (cuenta.SaldoInicial - movimiento.Valor < 0 && movimiento.TipoMovimiento == "Debito")
                        {
                            response.Message = "Saldo no disponible";
                        }
                    }
                    if(string.IsNullOrEmpty(response.Message))
                    {
                        Movimiento model = new Movimiento();
                        model.IdMovimiento = maxid;
                        model.IdCuenta = movimiento.IdCuenta;
                        model.Saldo = cuenta.SaldoInicial;
                        model.TipoMovimiento = movimiento.TipoMovimiento;
                        model.Valor = movimiento.Valor;
                        model.Fecha = DateTime.Now;
                        response.Id = model.IdMovimiento;
                        if (model.TipoMovimiento == "Credito")
                            cuenta.SaldoInicial += movimiento.Valor;
                        else
                            cuenta.SaldoInicial -= movimiento.Valor;

                        await context.AddAsync(model);
                        await context.SaveChangesAsync();
                    }
                }
                else
                {
                    response.Message = "Cuenta no existe";
                }      
            }
            catch (Exception ex)
            {
                response.Message = ex.Message.ToString();
            }
            return await Task.FromResult(response);
        }

        //public async Task UpdateAsync(Movimiento movimiento)
        //{
        //    var movimientoup = context.Movimientos.Find(movimiento.IdMovimiento);
        //    if (movimientoup != null)
        //    {
        //        movimientoup.Valor = movimiento.Valor;
        //        movimientoup.Saldo = movimiento.Saldo;
        //        await context.SaveChangesAsync();
        //    }
        //}
    }
}
