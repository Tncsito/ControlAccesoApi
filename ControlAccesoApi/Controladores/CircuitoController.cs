using ControlAccesoApi.Modelos;
using ControlAccesoApi.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlAccesoApi.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class CircuitoController : Controller
    {
        private readonly CircuitoRepositorio _circuitoRepositorio;

        public CircuitoController(CircuitoRepositorio circuitoRepositorio)
        {
            _circuitoRepositorio = circuitoRepositorio;
        }

        [HttpGet("Get")]
        public async Task<IEnumerable<Circuitos>> Get() => await _circuitoRepositorio.ObtenerTodasAsync();

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Circuitos>> Get(string id)
        {
            var circuito = await _circuitoRepositorio.ObtenerPorIdAsync(id);
            if (circuito == null)
                return NotFound();
            return circuito;
        }

        [HttpPost("Post")]
        public async Task<IActionResult> Post([FromBody] CircuitosDto circuitosDto)
        {
            var circuito = new Circuitos
            {
                PermisoId = circuitosDto.PermisoId,
                Estado = circuitosDto.Estado,
                Fecha = circuitosDto.Fecha
            };

            await _circuitoRepositorio.InsertarAsync(circuito);
            return CreatedAtAction(nameof(Get), new { id = circuito.Id }, circuito);
        }

        [HttpPut("Put/{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] CircuitosDto circuitosDto)
        {
            var existente = await _circuitoRepositorio.ObtenerPorIdAsync(id);
            if (existente == null)
                return NotFound();

            // Actualizar solo los campos proporcionados en el DTO
            if (circuitosDto.PermisoId != null)
                existente.PermisoId = circuitosDto.PermisoId;
            if (circuitosDto.Estado != null)
                existente.Estado = circuitosDto.Estado;
            if (circuitosDto.Fecha != null)
                existente.Fecha = circuitosDto.Fecha;

            await _circuitoRepositorio.ActualizarAsync(id, existente);
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var circuito = await _circuitoRepositorio.ObtenerPorIdAsync(id);
            if (circuito == null)
                return NotFound();

            await _circuitoRepositorio.EliminarAsync(id);
            return NoContent();
        }
    }
}
