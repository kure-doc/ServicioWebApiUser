using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServicioWebApiUser.DTOS;
using ServicioWebApiUser.Models;
using static Azure.Core.HttpHeader;

namespace ServicioWebApiUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenPedidoesController : ControllerBase
    {
        DbContextInnovaTel db = new DbContextInnovaTel();

        private readonly DbContextInnovaTel _context;

        public OrdenPedidoesController(DbContextInnovaTel context)
        {
            _context = context;
        }

        [HttpGet("ListadodePedidosporCodCliente/{codcli}")]
        public ActionResult<IEnumerable<dtoListadoPeddosPorCodCliente>> ConsultaPedido(string? codcli)
        {
            var query = from o in db.OrdenPedidos
                        where o.IdCliente == codcli
                        select new dtoListadoPeddosPorCodCliente
                        {
                            IdPedido = o.IdPedido,
                            IdCliente = o.IdCliente,
                            IdProducto = o.IdProducto,
                            FechaPedido = o.FechaPedido.Value,
                            MetodoPago = o.MetodoPago,
                            Cantidad = o.Cantidad,
                            Precio = o.Precio,
                            TotalDescuento = o.TotalDescuento
                        };
            return query.ToList();
        }

        [HttpGet("MesesdeVenta")]
        public ActionResult<IEnumerable<dtoMesesdeVenta>> Meses()
        {
            var query = (from p in db.OrdenPedidos
                         select new dtoMesesdeVenta
                         {
                             Nromes = p.FechaPedido.Value.Month,
                             NombreMes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(p.FechaPedido.Value.Month)
                         }).Distinct().OrderBy(q => q.Nromes);
            return query.ToList();
        }
        [HttpGet("PedidoporMes/{nromes}")]
        public ActionResult<IEnumerable<dtoPedidosporMes>> Consulta(int? nromes)
        {
            var query = from p in db.OrdenPedidos
                        where p.FechaPedido.Value.Month == nromes
                        select new dtoPedidosporMes
                        {
                            IdPedido = p.IdPedido,
                            IdCliente = p.IdCliente,
                            IdProducto = p.IdProducto,
                            NombreMes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(p.FechaPedido.Value.Month),
                            MetodoPago = p.MetodoPago,
                            Cantidad = p.Cantidad,
                            Precio = p.Precio,
                            TotalDescuento = p.TotalDescuento,
                            ConfirmarPedido = p.ConfirmarPedido
                        };
            return query.ToList();
        }

       

        [HttpPost("DisminuirCantidad")]
        public async Task<IActionResult> DisminuirCantidad([FromBody] dtoVenta venta)
        {
            var producto = await _context.Productos.FindAsync(venta.IdProducto);
            if (producto == null)
            {
                return NotFound();
            }

            if (producto.Cantidad < venta.Cantidad)
            {
                return BadRequest("No hay suficiente cantidad en stock.");
            }

            producto.Cantidad -= venta.Cantidad;
            await _context.SaveChangesAsync();
            return Ok();
        }

        // GET: api/OrdenPedidoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenPedido>> GetOrdenPedido(int id)
        {
            var ordenPedido = await _context.OrdenPedidos.FindAsync(id);

            if (ordenPedido == null)
            {
                return NotFound();
            }

            return ordenPedido;
        }

        // GET: api/OrdenPedidoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenPedido>>> GetOrdenPedidos()
        {
            return await _context.OrdenPedidos.ToListAsync();
        }

        [HttpPost("ActualizarPedido")]
        public async Task<IActionResult> ActualizarPedido([FromBody] dtoPedido pedidoDto)
        {
            if (pedidoDto == null)
            {
                return BadRequest("El cuerpo de la solicitud es inválido.");
            }

            var pedido = await _context.OrdenPedidos.FindAsync(pedidoDto.IdPedido);
            if (pedido == null)
            {
                return NotFound("Pedido no encontrado.");
            }

            pedido.ConfirmarPedido = pedidoDto.ConfirmarPedido;
            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenPedidoExists(pedidoDto.IdPedido))
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


        // POST: api/OrdenPedidoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdenPedido>> PostOrdenPedido(OrdenPedido ordenPedido)
        {
            _context.OrdenPedidos.Add(ordenPedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrdenPedido", new { id = ordenPedido.IdPedido }, ordenPedido);
        }

        // DELETE: api/OrdenPedidoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.OrdenPedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _context.OrdenPedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdenPedidoExists(int id)
        {
            return _context.OrdenPedidos.Any(e => e.IdPedido == id);
        }
    }
}
