using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.DAL;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Test
{
    [TestClass]
    public class ClientesTest
    {
        private readonly ClientesController _controller;
        private readonly ClienteService _Service;
        private ClienteInsertDTO clienteInsert;
        private Cliente cliente;
        public ClientesTest()
        {
            cliente = new Cliente()
            {
                Nombre = "Marianela Montalvo",
                Direccion = "Amazonas y NNUU",
                Edad = 0,
                Identificacion = "",
                Genero = "",
                Telefono = "097548965",
                Contrasenia = "5678"
            };
            clienteInsert = new ClienteInsertDTO()
            {
                Nombre = "Marianela Montalvo",
                Direccion = "Amazonas y NNUU",
                Edad = 0,
                Identificacion = "",
                Genero = "",
                Telefono = "097548965",
                Contrasenia = "5678"
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
            db.Add(cliente);

            var clientesService = new ClienteService(db);
            var clientesController = new ClientesController(clientesService);

            var result = await clientesService.GetAsync(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetAsyncReturnError()
        {
            //Arrage
            var nameDb = Guid.NewGuid().ToString();
            var serviceProvider = CreateContext(nameDb);
            var db = serviceProvider.GetService<Context>();

            var clientesService = new ClienteService(db);
            var clientesController = new ClientesController(clientesService);

            var result = await clientesService.GetAsync(0);
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetCreateOk()
        {
            //Arrage
            var nameDb = Guid.NewGuid().ToString();
            var serviceProvider = CreateContext(nameDb);

            var db = serviceProvider.GetService<Context>();

            var clientesService = new ClienteService(db);
            var clientesController = new ClientesController(clientesService);

            var result = await clientesService.SaveAsync(clienteInsert);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
