using System;
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
    public class ClientesController : ControllerBase
    {
        DbContextInnovaTel db = new DbContextInnovaTel();

        private readonly DbContextInnovaTel _context;

        public ClientesController(DbContextInnovaTel context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(string id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(string id, Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            var codigo = (from c in _context.Clientes select c.IdCliente).Max().ToString();
                codigo = "000" + (int.Parse(codigo.Substring(codigo.Length-3))+1).ToString();
                codigo = "C" + codigo.Substring(codigo.Length-3);
                cliente.IdCliente = codigo;

            _context.Clientes.Add(cliente);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (ClienteExists(cliente.IdCliente))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCliente", new { id = cliente.IdCliente }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(string id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(string id)
        {
            return _context.Clientes.Any(e => e.IdCliente == id);
        }

        [HttpGet("ListadeClientes")]
        public ActionResult<IEnumerable<dtoCliente>> Cliente()
        {
            var query = from c in db.Clientes
                        select new dtoCliente
                        {
                            IdCliente = c.IdCliente,
                            NombreCli = c.NombreCliente
                        };
            return query.ToList();
        }
        [HttpGet("PedidoPorBusquedaDeCliente/{id}")]
        public ActionResult<IEnumerable<dtoCli>> Consulta(string? id)
        {
            var query = from c in db.Clientes
                        where c.IdCliente == id
                        select new dtoCli
                        {
                            id = c.IdCliente,
                            nombre = c.NombreCliente,
                            dirreccion = c.DirreccionCliente,
                            telefono = c.TelefonoCliente,
                            email = c.Email
                        };
            return query.ToList();
        }
    }
}
