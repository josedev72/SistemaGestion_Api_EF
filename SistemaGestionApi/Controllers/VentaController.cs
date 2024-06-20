using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionApi.Models;
using SistemaGestionEntities;

namespace SistemaGestionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly SistemaGestionContext _context;

        public VentaController(SistemaGestionContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("crear")]
        public async Task<IActionResult> CrearVenta(Venta venta)
        {
            await _context.Ventas.AddAsync(venta);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<IEnumerable<Venta>>> listaVentas()
        {
            var ventas = await _context.Ventas.ToListAsync();
            return Ok(ventas);
        }

        [HttpGet]
        [Route("ver")]
        public async Task<IActionResult> verVenta(int id)
        {
            Venta venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            return Ok(venta);
        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> EditarVentao(int id, Venta venta)
        {
            var ventaActual = await _context.Ventas.FindAsync(id);
            // Id, Comentarios, IdUsuario
            ventaActual!.Comentarios = venta.Comentarios;
            ventaActual!.IdUsuario = venta.IdUsuario;
            
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("eliminar")]
        public async Task<IActionResult> EliminarVentao(int id)
        {
            var ventaActual = await _context.Ventas.FindAsync(id);

            _context.Ventas.Remove(ventaActual);

            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}