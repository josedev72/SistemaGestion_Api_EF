using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionApi.Models;
using SistemaGestionEntities;

namespace SistemaGestionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly SistemaGestionContext _context;

        public UsuarioController(SistemaGestionContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("crear")]
        public async Task<IActionResult> CrearUsuario(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<IEnumerable<Usuario>>> listaUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        [HttpGet]
        [Route("ver")]
        public async Task<IActionResult> verUsuario(int id)
        {
            Usuario usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpGet]
        [Route("traernombre")]
        public async Task<IActionResult> traerNombre(int id)
        {
            Usuario usuario = await _context.Usuarios.FindAsync(id);
            
            if (usuario == null)
            {
                return NotFound();
            }

            string nombre = usuario.Nombre;
            return Ok(nombre);
        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> EditarUsuario(int id, Usuario usuario)
        {
            var usuarioActual = await _context.Usuarios.FindAsync(id);
            // Id, Nombre, Apellido, NombreUsuario, Contrasenia, Mail
            usuarioActual!.Nombre = usuario.Nombre;
            usuarioActual!.Apellido = usuario.Apellido;
            usuarioActual!.NombreUsuario = usuario.NombreUsuario;
            usuarioActual!.Contrasenia = usuario.Contrasenia;
            usuarioActual!.Mail = usuario.Mail;
                
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("eliminar")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuarioActual = await _context.Usuarios.FindAsync(id);

            _context.Usuarios.Remove(usuarioActual);

            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}