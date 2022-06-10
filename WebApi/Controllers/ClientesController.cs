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
            //else 
            //    throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ClienteInsertDTO cliente)
        {
            if(string.IsNullOrEmpty(cliente.Nombre))
            {
                throw new ArgumentNullException("La propiedad Nombre es requerido");
            }
            else
            {
                ResponseWebApi response = await clienteService.SaveAsync(cliente);
                return Ok(response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] ClienteUpdateDTO cliente)
        {
            ResponseWebApi response = await clienteService.UpdateAsync(cliente);
            return Ok(response);
        }

        [HttpDelete("{clienteid}")]
        public async Task<IActionResult> DeleteAsync(int clienteid)
        {
            ResponseWebApi response = await clienteService.DeleteAsync(clienteid);
            return Ok(response);
        }
    }
}