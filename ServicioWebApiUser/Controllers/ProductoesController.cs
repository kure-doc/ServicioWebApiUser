using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using ServicioWebApiUser.DTOS;
using ServicioWebApiUser.Models;

namespace ServicioWebApiUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoesController : ControllerBase
    {
        DbContextInnovaTel db = new DbContextInnovaTel();

        private readonly DbContextInnovaTel _context;

        public ProductoesController(DbContextInnovaTel context)
        {
            _context = context;
        }

        // GET: api/Productoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        // GET: api/Productoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // PUT: api/Productoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
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

        // POST: api/Productoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new { id = producto.IdProducto }, producto);
        }

        // DELETE: api/Productoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.IdProducto == id);
        }

        [HttpGet("ListadeProductos")]
        public ActionResult<IEnumerable<dtoProducto>> Producto()
        {
            var query = (from p in db.Productos
                        select new dtoProducto
                        {
                            codigoPro = p.CodigoProducto
                        }).Distinct();
            return query.ToList();
        }
        [HttpGet("ListaDeProductosNom")]
        public ActionResult<IEnumerable<dtoProductoNom>> ProductoNom()
        {
            var query = from p in db.Productos
                        select new dtoProductoNom
                        {
                            IdProducto = p.IdProducto,
                            NombreProducto = p.NombreProducto
                        };
            return query.ToList();
        }

        [HttpGet("ListaDeProductosID")]
        public ActionResult<IEnumerable<dtoProductoID>> ProductoID()
        {
            var query = from p in db.Productos
                        select new dtoProductoID
                        {
                            IdProducto = p.IdProducto,
                            NombreProducto = p.NombreProducto,
                            PrecioProducto = p.PrecioProducto
                        };
            return query.ToList();
        }

        [HttpGet("PedidoPorBusquedadeCodigo/{codigo}")]
        public IActionResult PedidoPorBusquedadeCodigo(string codigo)
        {
            // Tu lógica para buscar el producto por código
            var producto = _context.Productos.Where(p => p.CodigoProducto == codigo).ToList();

            if (producto == null || !producto.Any())
            {
                return NotFound();
            }

            return Ok(producto);
        }

    }
}
