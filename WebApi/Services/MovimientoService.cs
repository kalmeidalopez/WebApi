using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DAL;
using WebApi.Models;

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
            var cuenta = context.Movimientos.Find(idmovimiento);
            return Task.FromResult(cuenta);
        }

        public async Task<ResponseWebApi> SaveAsync(MovimientoInsertDTO movimiento)
        {
            ResponseWebApi response = new ResponseWebApi();
            try
            {
                response.StatusCode = System.Net.HttpStatusCode.OK;   
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
                            response.StatusCode = System.Net.HttpStatusCode.NotFound;
                        }
                        else if (cuenta.SaldoInicial - movimiento.Valor < 0 && movimiento.TipoMovimiento == "Debito")
                        {
                            response.Message = "Saldo no disponible";
                            response.StatusCode = System.Net.HttpStatusCode.NotFound;
                        }
                        else if (cuenta.SaldoInicial - movimiento.Valor < 0 && movimiento.TipoMovimiento == "Debito")
                        {
                            response.Message = "Saldo no disponible";
                            response.StatusCode = System.Net.HttpStatusCode.NotFound;
                        }
                    }    
                    else
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
