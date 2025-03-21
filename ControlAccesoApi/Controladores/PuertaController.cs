﻿using ControlAccesoApi.Modelos;
using ControlAccesoApi.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlAccesoApi.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuertaController : ControllerBase
    {
        private readonly PuertaRepositorio _puertaRepositorio;

        public PuertaController(PuertaRepositorio puertaRepositorio)
        {
            _puertaRepositorio = puertaRepositorio;
        }

        [HttpGet]
        public async Task<IEnumerable<Puerta>> Get() => await _puertaRepositorio.ObtenerTodasAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Puerta>> Get(string id)
        {
            var puerta = await _puertaRepositorio.ObtenerPorIdAsync(id);
            if (puerta == null)
                return NotFound();
            return puerta;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Puerta puerta)
        {
            await _puertaRepositorio.InsertarAsync(puerta);
            return CreatedAtAction(nameof(Get), new { id = puerta.Id }, puerta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Puerta puerta)
        {
            var existente = await _puertaRepositorio.ObtenerPorIdAsync(id);
            if (existente == null)
                return NotFound();

            puerta.Id = id;
            await _puertaRepositorio.ActualizarAsync(id, puerta);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var puerta = await _puertaRepositorio.ObtenerPorIdAsync(id);
            if (puerta == null)
                return NotFound();

            await _puertaRepositorio.EliminarAsync(id);
            return NoContent();
        }
    }
}
