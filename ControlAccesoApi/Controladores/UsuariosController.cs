using ControlAccesoApi.Modelos;
using ControlAccesoApi.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlAccesoApi.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(UsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        public async Task<IEnumerable<Usuario>> Get() => await _usuarioRepositorio.ObtenerTodosAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(string id)
        {
            var usuario = await _usuarioRepositorio.ObtenerPorIdAsync(id);
            if (usuario == null)
                return NotFound();
            return usuario;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuarioDto usuarioDto)
        {
            var usuario = new Usuario
            {
                Nombre = usuarioDto.Nombre,
                Pin = Convert.ToInt32(usuarioDto.Pin),
                Rol = usuarioDto.Rol
            };

            await _usuarioRepositorio.CrearAsync(usuario);
            return CreatedAtAction(nameof(Get), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] UsuarioDto usuarioDto)
        {
            var existente = await _usuarioRepositorio.ObtenerPorIdAsync(id);
            if (existente == null)
                return NotFound();

            // Actualizar solo los campos proporcionados en el DTO
            if (usuarioDto.Nombre != null)
                existente.Nombre = usuarioDto.Nombre;
            if (usuarioDto.Pin.HasValue)
                existente.Pin = usuarioDto.Pin.Value;
            if (usuarioDto.Rol != null)
                existente.Rol = usuarioDto.Rol;
            if (usuarioDto.UltimoAcceso.HasValue)
                existente.UltimoAcceso = usuarioDto.UltimoAcceso.Value;

            await _usuarioRepositorio.ActualizarAsync(id, existente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var usuario = await _usuarioRepositorio.ObtenerPorIdAsync(id);
            if (usuario == null)
                return NotFound();

            await _usuarioRepositorio.EliminarAsync(id);
            return NoContent();
        }
    }
}
