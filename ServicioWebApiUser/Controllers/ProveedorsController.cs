using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServicioWebApiUser.DTOS;
using ServicioWebApiUser.Models;

namespace ServicioWebApiUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorsController : ControllerBase
    {
        DbContextInnovaTel db = new DbContextInnovaTel();

        private readonly DbContextInnovaTel _context;

        public ProveedorsController(DbContextInnovaTel context)
        {
            _context = context;
        }

        // GET: api/Proveedors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedores()
        {
            return await _context.Proveedores.ToListAsync();
        }

        // GET: api/Proveedors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> GetProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
            {
                return NotFound();
            }

            return proveedor;
        }

        [HttpGet("DetallesPedido/{idPedido}")]
        public ActionResult<dtoSeguimientoOrdenCompra> DetallesPedido(int idPedido)
        {
            var pedidoDetalle = (from o in _context.OrdenPedidos
                                 join c in _context.Clientes on o.IdCliente equals c.IdCliente
                                 join p in _context.Productos on o.IdProducto equals p.IdProducto
                                 where o.IdPedido == idPedido
                                 select new dtoSeguimientoOrdenCompra
                                 {
                                     IdPedido = o.IdPedido,
                                     IdCliente = o.IdCliente,
                                     NombreCliente = c.NombreCliente,
                                     IdProducto = o.IdProducto,
                                     NombreProducto = p.NombreProducto,
                                     FechaPedido = o.FechaPedido,
                                     FechaEntrega = o.ConfirmarPedido == "Confirmado" ? DateTime.UtcNow.Date : (DateTime?)null,
                                     Cantidad = o.Cantidad,
                                     TotalDescuento = o.TotalDescuento,
                                     ConfirmarPedido = o.ConfirmarPedido
                                 }).FirstOrDefault();

            if (pedidoDetalle == null)
            {
                return NotFound();
            }

            return pedidoDetalle;
        }


        // PUT: api/Proveedors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedor(int id, Proveedor proveedor)
        {
            if (id != proveedor.IdProveedor)
            {
                return BadRequest();
            }

            _context.Entry(proveedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorExists(id))
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

        // POST: api/Proveedors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Proveedor>> PostProveedor(Proveedor proveedor)
        {
            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProveedor", new { id = proveedor.IdProveedor }, proveedor);
        }

        [HttpPost("ConfirmarCompra/{idPedido}")]
        public async Task<IActionResult> ConfirmarCompra(int idPedido)
        {
            var pedido = await _context.OrdenPedidos.FindAsync(idPedido);

            if (pedido == null)
            {
                return NotFound();
            }

            // Cambia el estado del pedido
            pedido.ConfirmarPedido = "Compra Realizada";

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            // Retorna un mensaje de éxito
            return Ok(new { message = "Compra realizada" });
        }

        [HttpPost("RechazarCompra/{idPedido}")]
        public async Task<IActionResult> RechazarCompra(int idPedido)
        {
            var pedido = await _context.OrdenPedidos.FindAsync(idPedido);

            if (pedido == null)
            {
                return NotFound();
            }

            // Cambia el estado del pedido a "Compra Rechazada" o lo que necesites
            pedido.ConfirmarPedido = "Compra Rechazada";

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            // Retorna un mensaje de éxito
            return Ok(new { message = "Compra rechazada" });
        }



        // DELETE: api/Proveedors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("SeguimientoOrdenCompra/{idCliente}")]
        public ActionResult<IEnumerable<dtoSeguimientoOrdenCompra>> Seguimiento(string? idCliente)
        {
            var today = DateTime.UtcNow.Date;

            var query = from o in db.OrdenPedidos
                        join c in db.Clientes
                        on o.IdCliente equals c.IdCliente
                        join p in db.Productos
                        on o.IdProducto equals p.IdProducto
                        where o.IdCliente == idCliente
                        select new dtoSeguimientoOrdenCompra
                        {
                            IdPedido = o.IdPedido,
                            IdCliente = o.IdCliente,
                            NombreCliente = c.NombreCliente,
                            IdProducto = p.IdProducto,
                            NombreProducto = p.NombreProducto,
                            FechaPedido = o.FechaPedido,
                            FechaEntrega = (o.ConfirmarPedido == "Confirmado" || o.ConfirmarPedido == "Compra Realizada") ? DateTime.UtcNow.Date : (DateTime?)null,
                            Cantidad = o.Cantidad,
                            TotalDescuento = o.TotalDescuento,
                            ConfirmarPedido = o.ConfirmarPedido
                        };
            return query.ToList();
        }

        private bool ProveedorExists(int id)
        {
            return _context.Proveedores.Any(e => e.IdProveedor == id);
        }
    }
}
