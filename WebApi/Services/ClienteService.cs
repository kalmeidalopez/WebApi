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
        public async Task<ResponseWebApi> SaveAsync(ClienteInsertDTO cliente)
        {
            ResponseWebApi response = new ResponseWebApi();
            try
            {
                response.StatusCode = System.Net.HttpStatusCode.OK;
                int maxid = 1;
                if (_clienteContext.Clientes.Any())
                    maxid = _clienteContext.Clientes.Max(p => p.Id) + 1;
                Cliente model = new Cliente();
                model.Id = maxid;
                model.Contrasenia = cliente.Contrasenia;
                model.Direccion = cliente.Direccion;
                model.Edad = cliente.Edad;
                model.Estado = true;
                model.Genero = cliente.Genero;
                model.Identificacion = cliente.Identificacion;
                model.Nombre = cliente.Nombre;
                model.Telefono = cliente.Telefono;
                model.FechaIngreso = DateTime.Now;
                response.Id = model.Id;
                await _clienteContext.AddAsync(model);
                await _clienteContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                response.Message = ex.Message.ToString();
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return await Task.FromResult(response);
        }
        public async Task<ResponseWebApi> UpdateAsync(ClienteUpdateDTO cliente)
        {
            ResponseWebApi response = new ResponseWebApi();
            try
            {
                response.Id = cliente.Id;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                var clienteup = _clienteContext.Clientes.Find(cliente.Id);
                if(clienteup != null)
                {
                    clienteup.Nombre = cliente.Nombre;
                    clienteup.Direccion = cliente.Direccion;
                    clienteup.Telefono = cliente.Telefono;
                    clienteup.Identificacion = cliente.Identificacion;
                    clienteup.Contrasenia = cliente.Contrasenia;
                    clienteup.Edad = cliente.Edad;
                    clienteup.Genero = cliente.Genero;

                    await _clienteContext.SaveChangesAsync();
                }
                else
                {
                    response.Message = "cliente no existe";
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
        public async Task<ResponseWebApi> DeleteAsync(int clienteid)
        {
            ResponseWebApi response = new ResponseWebApi();
            try
            {
                response.Id = clienteid;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                var clientedel = _clienteContext.Clientes.Find(clienteid);
                if (clientedel != null)
                {
                    clientedel.Estado = false;
                    //_clienteContext.Remove(clientedel);
                    await _clienteContext.SaveChangesAsync();
                }
                else
                {
                    response.Message = "Cliente no existe";
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

        public Task<List<Cliente>> GetAsync()
        {
            var cliente = _clienteContext.Clientes.ToList();
            return Task.FromResult(cliente);
        }
    }
}
