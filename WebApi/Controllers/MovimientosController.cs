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
    public class MovimientosController : ControllerBase
    {
        private readonly IMovimientoService movimientoService;
        public MovimientosController(IMovimientoService movimientoService)
        {
            this.movimientoService = movimientoService;
        }

        [HttpGet("{idmovimiento}")]
        public async Task<IActionResult> Get(int idmovimiento)
        {
            var movimiento = await movimientoService.GetAsync(idmovimiento);

            if (movimiento != null)
            {
                return Ok(movimiento);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MovimientoInsertDTO movimiento)
        {
            ResponseWebApi response = await movimientoService.SaveAsync(movimiento);
            return Ok(response);
        }

        //[HttpPut]
        //public async Task<IActionResult> Put([FromBody] MovimientoInsertDTO movimiento)
        //{
        //    await movimientoService.UpdateAsync(movimiento);
        //    return Ok();
        //}

        //[HttpDelete("{idmovimiento}")]
        //public async Task<IActionResult> Delete(int idmovimiento)
        //{
        //    await movimientoService.DeleteAsync(idmovimiento);
        //    return Ok();
        //}
    }
}
