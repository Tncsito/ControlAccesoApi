﻿using ControlAccesoApi.Modelos;
using ControlAccesoApi.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlAccesoApi.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccesoController : ControllerBase
    {
        private readonly AccesoRepositorio _accesoRepositorio;

        public AccesoController(AccesoRepositorio accesoRepositorio)
        {
            _accesoRepositorio = accesoRepositorio;
        }

        [HttpGet("Get")]
        public async Task<IEnumerable<Accesos>> Get() => await _accesoRepositorio.ObtenerTodosAsync();

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Accesos>> Get(string id)
        {
            var acceso = await _accesoRepositorio.ObtenerPorIdAsync(id);
            if (acceso == null)
                return NotFound();
            return acceso;
        }

        [HttpPost("Post")]
        public async Task<IActionResult> Post([FromBody] AccesosDto accesoDto)
        {
            var acceso = new Accesos
            {
                UsuarioId = accesoDto.UsuarioId,
                Fecha = accesoDto.Fecha,
                Metodo = accesoDto.Metodo,
                Estado = accesoDto.Estado,
                PuertasId = accesoDto.PuertasId,
                PermisosId = accesoDto.PermisosId
            };

            await _accesoRepositorio.InsertarAsync(acceso);
            return CreatedAtAction(nameof(Get), new { id = acceso.Id }, acceso);
        }

        [HttpPut("Put/{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] AccesosDto accesoDto)
        {
            var existente = await _accesoRepositorio.ObtenerPorIdAsync(id);
            if (existente == null)
                return NotFound();

            // Actualizar solo los campos proporcionados en el DTO
            if (accesoDto.UsuarioId != null)
                existente.UsuarioId = accesoDto.UsuarioId;
            existente.Fecha = accesoDto.Fecha;
            if (accesoDto.Metodo != null)
                existente.Metodo = accesoDto.Metodo;
            existente.Estado = accesoDto.Estado;
            if (accesoDto.PuertasId != null)
                existente.PuertasId = accesoDto.PermisosId;
            if (accesoDto.PermisosId != null)
                existente.PermisosId = accesoDto.PermisosId;

            await _accesoRepositorio.ActualizarAsync(id, existente);
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
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