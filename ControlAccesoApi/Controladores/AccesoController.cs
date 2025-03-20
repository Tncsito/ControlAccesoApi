using ControlAccesoApi.Modelos;
using ControlAccesoApi.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlAccesoApi.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly AccesoRepositorio _accesoRepositorio;

        public AccesoController(AccesoRepositorio accesoRepositorio)
        {
            _accesoRepositorio = accesoRepositorio;
        }

        [HttpGet]
        public async Task<IEnumerable<Accesos>> Get() => await _accesoRepositorio.ObtenerTodosAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Accesos>> Get(string id)
        {
            var acceso = await _accesoRepositorio.ObtenerPorIdAsync(id);
            if (acceso == null)
                return NotFound();
            return acceso;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Accesos acceso)
        {
            await _accesoRepositorio.InsertarAsync(acceso);
            return CreatedAtAction(nameof(Get), new { id = acceso.Id }, acceso);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Accesos acceso)
        {
            var existente = await _accesoRepositorio.ObtenerPorIdAsync(id);
            if (existente == null)
                return NotFound();

            acceso.Id = id;
            await _accesoRepositorio.ActualizarAsync(id, acceso);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var acceso = await _accesoRepositorio.ObtenerPorIdAsync(id);
            if (acceso == null)
                return NotFound();

            await _accesoRepositorio.EliminarAsync(id);
            return NoContent();
        }
    }
}
