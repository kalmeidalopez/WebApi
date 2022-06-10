using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DAL;
using WebApi.Models;

namespace WebApi.Services
{
    public class ClienteService : IClienteService
    {
        Context _clienteContext;
        public ClienteService(Context dbcontext)
        {
            _clienteContext = dbcontext;
        }
        public async Task<Cliente> GetAsync(int clienteid)
        {
            var cliente = _clienteContext.Clientes.Find(clienteid);
            return await Task.FromResult(cliente);
        }
        public async Task SaveAsync(Cliente cliente)
        {
            int maxid = 1;
            if (_clienteContext.Clientes.Any())
                maxid = _clienteContext.Clientes.Max(p => p.Id) + 1;
            cliente.Id = maxid;
            await _clienteContext.AddAsync(cliente);
            await _clienteContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Cliente cliente)
        {
            var clienteup = _clienteContext.Clientes.Find(cliente.Id);
            clienteup.Nombre = cliente.Nombre;
            clienteup.Direccion = cliente.Direccion;
            clienteup.Telefono = cliente.Telefono;
            clienteup.Identificacion = cliente.Identificacion;
            clienteup.Contrasenia = cliente.Contrasenia;
            clienteup.Edad = cliente.Edad;
            clienteup.Genero = cliente.Genero;
            
            await _clienteContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int clienteid)
        {
            var clientedel = _clienteContext.Clientes.Find(clienteid);
            clientedel.Estado = false;
            await _clienteContext.SaveChangesAsync();
        }

        public Task<List<Cliente>> GetAsync()
        {
            var cliente = _clienteContext.Clientes.ToList();
            return Task.FromResult(cliente);
        }

        public Cliente Get(int clienteid)
        {
            var cliente = _clienteContext.Clientes.Find(clienteid);
            return cliente;
        }
    }
}
