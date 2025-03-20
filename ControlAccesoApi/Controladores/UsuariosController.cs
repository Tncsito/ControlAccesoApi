using ControlAccesoApi.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace ControlAccesoApi.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;

        public UsuariosController(UsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var Usuarios = await _usuarioRepositorio.ObtenerTodosAsync();
            return Ok(Usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerUsuarioPorId(string id)
        {
            var Usuario = await _usuarioRepositorio.ObtenerPorIdAsync(id);
            if (Usuario == null) return NotFound();
            return Ok(Usuario);
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] Usuario Usuario)
        {
            if (Usuario == null) return BadRequest();
            await _usuarioRepositorio.CrearAsync(Usuario);
            return CreatedAtAction(nameof(ObtenerUsuarioPorId), new { id = Usuario.Id }, Usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(string id, [FromBody] Usuario usuarioActualizado)
        {
            var usuarioExistente = await _usuarioRepositorio.ObtenerPorIdAsync(id);
            if (usuarioExistente == null) return NotFound();

            usuarioActualizado.Id = id;
            await _usuarioRepositorio.ActualizarAsync(id, usuarioActualizado);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(string id)
        {
            var usuarioExistente = await _usuarioRepositorio.ObtenerPorIdAsync(id);
            if (usuarioExistente == null) return NotFound();

            await _usuarioRepositorio.EliminarAsync(id);
            return NoContent();
        }
    }
}
