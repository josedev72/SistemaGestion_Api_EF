using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionApi.Models;
using SistemaGestionEntities;

namespace SistemaGestionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoVendidoController : ControllerBase
    {
        private readonly SistemaGestionContext _context;

        public ProductoVendidoController(SistemaGestionContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("crear")]
        public async Task<IActionResult> CrearProductoVendido(ProductoVendido productoVendido)
        {
            await _context.ProductosVendidos.AddAsync(productoVendido);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<IEnumerable<ProductoVendido>>> listaProductosVendidos()
        {
            var productosVendidos = await _context.ProductosVendidos.ToListAsync();
            return Ok(productosVendidos);
        }

        [HttpGet]
        [Route("ver")]
        public async Task<IActionResult> verProductoVendido(int id)
        {
            ProductoVendido productoVendido = await _context.ProductosVendidos.FindAsync(id);
            if (productoVendido == null)
            {
                return NotFound();
            }
            return Ok(productoVendido);
        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> EditarProductoVendido(int id, ProductoVendido productoVendido)
        {
            var productoVendidoActual = await _context.ProductosVendidos.FindAsync(id);
            // Id, IdProducto, Stock, IdVenta
            productoVendidoActual!.IdProducto = productoVendido.IdProducto;
            productoVendidoActual!.Stock = productoVendido.Stock;
            productoVendidoActual!.IdVenta = productoVendido.IdVenta;
            

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("eliminar")]
        public async Task<IActionResult> EliminarProductoVendido(int id)
        {
            var productoVendidoActual = await _context.ProductosVendidos.FindAsync(id);

            _context.ProductosVendidos.Remove(productoVendidoActual);

            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}