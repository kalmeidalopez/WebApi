using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly ICuentaService cuentaService;
        public CuentasController(ICuentaService cuentaService)
        {
            this.cuentaService = cuentaService;
        }

        [HttpGet("{idcuenta}")]
        public async Task<IActionResult> GetAsync(int idcuenta)
        {
            var cuenta = await cuentaService.GetAsync(idcuenta);

            if (cuenta != null)
            {
                return Ok(cuenta);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CuentaInsertDTO cuenta)
        {
            ResponseWebApi response = await cuentaService.SaveAsync(cuenta);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] CuentaUpdateDTO cuenta)
        {
            ResponseWebApi response = await cuentaService.UpdateAsync(cuenta);
            return Ok(response);
        }

        [HttpDelete("{idcuenta}")]
        public async Task<IActionResult> DeleteAsync(int idcuenta)
        {
            ResponseWebApi response = await cuentaService.DeleteAsync(idcuenta);
            return Ok(response);
        }
    }
}
