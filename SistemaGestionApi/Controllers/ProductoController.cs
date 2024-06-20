using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionApi.Models;

namespace SistemaGestionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly SistemaGestionContext _context;

        public ProductoController(SistemaGestionContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("crear")]
        public async Task<IActionResult> CrearProducto(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<IEnumerable<Producto>>> listaProductos()
        {
            var productos = await _context.Productos.ToListAsync();
            return Ok(productos);
        }

        [HttpGet]
        [Route("ver")]
        public async Task<IActionResult> verProducto(int id)
        {
            Producto producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> EditarProducto(int id, Producto producto)
        {
            var productoActual = await _context.Productos.FindAsync(id);
            // Id, Descripcion, Costo, PrecioVenta, Stock, IdUsuario
            productoActual!.Descripcion = producto.Descripcion;
            productoActual!.Costo = producto.Costo;
            productoActual!.PrecioVenta = producto.PrecioVenta;
            productoActual!.Stock = producto.Stock;
            productoActual!.IdUsuario = producto.IdUsuario;

            await _context.SaveChangesAsync();
            
            return Ok();
        }

        [HttpDelete]
        [Route("eliminar")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var productoActual = await _context.Productos.FindAsync(id);
            
           _context.Productos.Remove(productoActual);

            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
