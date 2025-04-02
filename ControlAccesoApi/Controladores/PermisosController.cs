using ControlAccesoApi.Modelos;
using ControlAccesoApi.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlAccesoApi.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisosController : ControllerBase
    {
        private readonly PermisosRepositorio _permisosRepositorio;

        public PermisosController(PermisosRepositorio permisosRepositorio)
        {
            _permisosRepositorio = permisosRepositorio;
        }

        [HttpGet("Get")]
        public async Task<IEnumerable<Permisos>> Get() => await _permisosRepositorio.ObtenerTodosAsync();

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Permisos>> Get(string id)
        {
            var permiso = await _permisosRepositorio.ObtenerPorIdAsync(id);
            if (permiso == null)
                return NotFound();
            return permiso;
        }

        [HttpPost("Post")]
        public async Task<IActionResult> Post([FromBody] PermisosDto permisosDto)
        {
            var permiso = new Permisos
            {
                UsuarioId = permisosDto.UsuarioId,
                PuertasId = permisosDto.PuertasId,
                Puesto = permisosDto.Puesto,
                Dias = permisosDto.Dias,
                Horas = permisosDto.Horas
            };

            await _permisosRepositorio.InsertarAsync(permiso);
            return CreatedAtAction(nameof(Get), new { id = permiso.Id }, permiso);
        }

        [HttpPut("Put/{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] PermisosDto permisosDto)
        {
            var existente = await _permisosRepositorio.ObtenerPorIdAsync(id);
            if (existente == null)
                return NotFound();

            // Actualizar solo los campos proporcionados en el DTO
            if (permisosDto.UsuarioId != null)
                existente.UsuarioId = permisosDto.UsuarioId;
            if (permisosDto.PuertasId != null)
                existente.PuertasId = permisosDto.PuertasId;
            if (permisosDto.Puesto != null)
                existente.Puesto = permisosDto.Puesto;
            if (permisosDto.Dias != null)
                existente.Dias = permisosDto.Dias;
            if (permisosDto.Horas != null)
                existente.Horas = permisosDto.Horas;

            await _permisosRepositorio.ActualizarAsync(id, existente);
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var permiso = await _permisosRepositorio.ObtenerPorIdAsync(id);
            if (permiso == null)
                return NotFound();

            await _permisosRepositorio.EliminarAsync(id);
            return NoContent();
        }
    }
}