using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {

        private readonly IMovimientoService movimientoService;
        public ReportesController(IMovimientoService movimientoService)
        {
            this.movimientoService = movimientoService;
        }

        [HttpGet]
        [Route("estadocuenta")]
        public async Task<IActionResult> GetAsync(int clienteid, DateTime fechaInicio, DateTime fechaFin)
        {
            var cliente = await movimientoService.GetEstadoCuentaAsync(clienteid, fechaInicio, fechaFin);

            if (cliente != null)
            {
                return Ok(cliente);
            }
            return NotFound();
        }
    }
}
