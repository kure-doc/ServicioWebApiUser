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
    public class OrdenComprasController : ControllerBase
    {
        private readonly DbContextInnovaTel _context;

        public OrdenComprasController(DbContextInnovaTel context)
        {
            _context = context;
        }

        // GET: api/OrdenCompras/
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenCompra>> GetOrdenCompra(int id)
        {
            var ordenCompra = await _context.OrdenCompras.FindAsync(id);

            if (ordenCompra == null)
            {
                return NotFound();
            }

            ordenCompra.TotalCompra = ordenCompra.Cantidad * ordenCompra.Precio;

            return ordenCompra;
        }

        // GET: api/OrdenCompras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenCompra>>> GetOrdenCompras()
        {
            return await _context.OrdenCompras.ToListAsync();
        }

        [HttpPost("AumentarCantidad")]
        public async Task<IActionResult> AumentarCantidad([FromBody] dtoCompra compra)
        {
            if (compra == null)
            {
                return BadRequest("El objeto de compra es inválido.");
            }

            Console.WriteLine($"IdProducto: {compra.IdProducto}, Cantidad: {compra.Cantidad}");

            // Verifica que IdProducto y Cantidad no sean 0
            if (compra.IdProducto <= 0 || compra.Cantidad <= 0)
            {
                return BadRequest("Invalid product ID or quantity.");
            }

            var producto = await _context.Productos.FindAsync(compra.IdProducto);
            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }

            producto.Cantidad += compra.Cantidad;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Registrar el error en un sistema de log si es necesario
                Console.WriteLine($"Error al aumentar la cantidad: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud.");
            }

            return Ok("Cantidad aumentada exitosamente.");
        }


        [HttpPost("ActualizarCompra")]
        public async Task<IActionResult> ActualizarCompra([FromBody] dtoCompras comprasdto)
        {
            if (comprasdto == null)
            {
                return BadRequest("El cuerpo de la solicitud es inválido.");
            }

            var pedido = await _context.OrdenCompras.FindAsync(comprasdto.IdOrdenCompra);
            if (pedido == null)
            {
                return NotFound("Pedido no encontrado.");
            }

            pedido.ConfirmarPedido = comprasdto.ConfirmarPedido;
            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenCompraExists(comprasdto.IdOrdenCompra))
                {
                    return NotFound("Pedido no encontrado después de la concurrencia.");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }


        // POST: api/OrdenCompras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdenCompra>> PostOrdenCompra(OrdenCompra ordenCompra)
        {
            try
            {
                // Obtener el último número de compra, si existe
                var ultimoCodigo = _context.OrdenCompras
                    .Where(o => o.NroCompra != null)
                    .Select(o => o.NroCompra)
                    .OrderByDescending(c => c)
                    .FirstOrDefault();

                int numero = 1;
                if (ultimoCodigo != null)
                {
                    // Extraer el número del código actual
                    string numeroStr = ultimoCodigo.Substring(2); // Obtener la parte numérica (después de "N°")
                    if (int.TryParse(numeroStr, out int resultado))
                    {
                        numero = resultado + 1; // Incrementar el número
                    }
                }

                // Generar el nuevo código
                string nuevoCodigo = "N°" + numero.ToString("D3"); // Formato con ceros a la izquierda
                ordenCompra.NroCompra = nuevoCodigo;

                _context.OrdenCompras.Add(ordenCompra);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetOrdenCompra", new { id = ordenCompra.IdOrdenCompra }, ordenCompra);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en PostOrdenCompra: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud.");
            }
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompra(int id)
        {
            var pedido = await _context.OrdenCompras.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _context.OrdenCompras.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdenCompraExists(int id)
        {
            return _context.OrdenCompras.Any(e => e.IdOrdenCompra == id);
        }
    }
}
