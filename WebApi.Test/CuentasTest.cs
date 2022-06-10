using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.DAL;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Test
{
    [TestClass]
    public class CuentasTest
    {
        private Cuenta cuenta;
        public CuentasTest()
        {
            cuenta = new Cuenta()
            {
                NumeroCuenta = "478758",
                TipoCuenta = "Ahorro",
                SaldoInicial = 2000,
                Clienteid = 1
            };
        }
        private IServiceProvider CreateContext(string nameDb)
        {
            var services = new ServiceCollection();
            services.AddDbContext<Context>(options => options.UseInMemoryDatabase(databaseName: nameDb),
                ServiceLifetime.Scoped,
                ServiceLifetime.Scoped);

            return services.BuildServiceProvider();
        }


        [TestMethod]
        public async Task GetAsyncReturnOk()
        {
            //Arrage
            var nameDb = Guid.NewGuid().ToString();
            var serviceProvider = CreateContext(nameDb);

            var db = serviceProvider.GetService<Context>();
            db.Add(cuenta);

            var cuentasService = new CuentaService(db);

            var result = await cuentasService.GetAsync(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetAsyncReturnError()
        {
            //Arrage
            var nameDb = Guid.NewGuid().ToString();
            var serviceProvider = CreateContext(nameDb);

            var db = serviceProvider.GetService<Context>();

            var cuentasService = new CuentaService(db);

            var result = await cuentasService.GetAsync(0);
            Assert.IsNotNull(result);
        }
    }
}
