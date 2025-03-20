using ControlAccesoApi.Modelos;
using ControlAccesoApi.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlAccesoApi.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroController : ControllerBase
    {
        private readonly RegistroRepositorio _registroRepositorio;

        public RegistroController(RegistroRepositorio registroRepositorio)
        {
            _registroRepositorio = registroRepositorio;
        }

        [HttpGet]
        public async Task<IEnumerable<Registro>> Get() => await _registroRepositorio.ObtenerTodosAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Registro>> Get(string id)
        {
            var registro = await _registroRepositorio.ObtenerPorIdAsync(id);
            if (registro == null)
                return NotFound();
            return registro;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Registro registro)
        {
            await _registroRepositorio.InsertarAsync(registro);
            return CreatedAtAction(nameof(Get), new { id = registro.Id }, registro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Registro registro)
        {
            var existente = await _registroRepositorio.ObtenerPorIdAsync(id);
            if (existente == null)
                return NotFound();

            registro.Id = id;
            await _registroRepositorio.ActualizarAsync(id, registro);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var registro = await _registroRepositorio.ObtenerPorIdAsync(id);
            if (registro == null)
                return NotFound();

            await _registroRepositorio.EliminarAsync(id);
            return NoContent();
        }
    }
}
