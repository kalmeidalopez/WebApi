using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        public static List<Cliente> ListClientes = new List<Cliente>();
        private readonly IClienteService clienteService;
        public ClientesController(IClienteService clienteService)
        {
            this.clienteService = clienteService;
        }


        [HttpGet(Name = "GetClientes")]
        public async Task<IActionResult> Get()
        {
            var cliente = await clienteService.GetAsync();
            if (cliente != null)
            {
                return Ok(cliente);
            }
            return NotFound();
        }

        [HttpGet("{clienteid}")]
        public async Task<IActionResult> GetAsync(int clienteid)
        {
            var cliente = await clienteService.GetAsync(clienteid);

            if (cliente != null)
            {
                return Ok(cliente);
            }
            return NotFound();
        }

        private Cliente GetCliente(int clienteid)
        {
            var cliente = clienteService.Get(clienteid);

            if (cliente != null)
            {
                return cliente;
            }
            return null;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ResponseWebApi), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(ResponseWebApi), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PostAsync([FromBody] ClienteInsertDTO cliente)
        {
            ResponseWebApi response = new ResponseWebApi();
            try
            {
                if(string.IsNullOrEmpty(cliente.Nombre))
                {
                    response.Message = "Debe Ingresar Nombre";
                    return Conflict(response);
                }
                Cliente model = new Cliente();
                model.Id = 0;
                model.Contrasenia = cliente.Contrasenia;
                model.Direccion = cliente.Direccion;
                model.Edad = cliente.Edad;
                model.Estado = true;
                model.Genero = cliente.Genero;
                model.Identificacion = cliente.Identificacion;
                model.Nombre = cliente.Nombre;
                model.Telefono = cliente.Telefono;
                model.FechaIngreso = DateTime.Now;
                await clienteService.SaveAsync(model);
                response.Id = model.Id;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] ClienteUpdateDTO cliente)
        {
            ResponseWebApi response = new ResponseWebApi();
            try
            {
                response.Id = cliente.Id;
                var clientedel = GetCliente(cliente.Id);
                if (clientedel != null)
                {
                    Cliente model = new Cliente();
                    model.Nombre = cliente.Nombre;
                    model.Direccion = cliente.Direccion;
                    model.Telefono = cliente.Telefono;
                    model.Identificacion = cliente.Identificacion;
                    model.Contrasenia = cliente.Contrasenia;
                    model.Edad = cliente.Edad;
                    model.Genero = cliente.Genero;
                    model.Id = cliente.Id;
                    await clienteService.UpdateAsync(model);
                }
                else
                {
                    response.Message = "cliente no existe";
                    return Conflict(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message.ToString();
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, response);
            }
            return Ok(response);
        }

        [HttpDelete("{clienteid}")]
        public async Task<IActionResult> DeleteAsync(int clienteid)
        {
            ResponseWebApi response = new ResponseWebApi();
            try
            {
                response.Id = clienteid;
                var clientedel = GetCliente(clienteid);
                if (clientedel != null)
                {
                    await clienteService.DeleteAsync(clienteid);
                }
                else
                {
                    response.Message = "Cliente no existe";
                    return Conflict(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, response);
            }
            return Ok(response);
        }
    }
}