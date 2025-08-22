﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServicioWebApiUser.DTOS;
using ServicioWebApiUser.Models;

namespace ServicioWebApiUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilesController : ControllerBase
    {
        private readonly DbContextInnovaTel _context;

        public PerfilesController(DbContextInnovaTel context)
        {
            _context = context;
        }

        // GET: api/Perfiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Perfil>>> GetPerfiles()
        {
            return await _context.Perfiles.ToListAsync();
        }

        // GET: api/Perfiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Perfil>> GetPerfil(int id)
        {
            var perfil = await _context.Perfiles.FindAsync(id);

            if (perfil == null)
            {
                return NotFound();
            }

            return perfil;
        }

        // PUT: api/Perfiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerfil(int id, Perfil perfil)
        {
            if (id != perfil.IdPerfil)
            {
                return BadRequest();
            }

            _context.Entry(perfil).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerfilExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Perfiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Perfil>> PostPerfil(Perfil perfil)
        {
            _context.Perfiles.Add(perfil);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerfil", new { id = perfil.IdPerfil }, perfil);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] dtoLoginViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid client request");
            }

            var perfil = await _context.Perfiles
                .FirstOrDefaultAsync(p => p.Nombre == model.Nombre);

            if (perfil != null && perfil.Contrasenia == model.Contrasenia)
            {
                var loginResponse = new LoginResponse
                {
                    IsAuthenticated = true,
                    IdCliente = perfil.IdCliente // Asegúrate de que esta propiedad existe en el modelo Perfil
                };
                return Ok(loginResponse); // Devolvemos un LoginResponse
            }

            var failedResponse = new LoginResponse
            {
                IsAuthenticated = false
            };
            return Ok(failedResponse); // Devolvemos un LoginResponse
        }


        // DELETE: api/Perfiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerfil(int id)
        {
            var perfil = await _context.Perfiles.FindAsync(id);
            if (perfil == null)
            {
                return NotFound();
            }

            _context.Perfiles.Remove(perfil);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PerfilExists(int id)
        {
            return _context.Perfiles.Any(e => e.IdPerfil == id);
        }
    }
}
