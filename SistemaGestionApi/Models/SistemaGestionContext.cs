using Microsoft.EntityFrameworkCore;
using SistemaGestionEntities;

namespace SistemaGestionApi.Models
{
    public class SistemaGestionContext : DbContext
    {
        public SistemaGestionContext(DbContextOptions<SistemaGestionContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<ProductoVendido> ProductosVendidos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Venta> Ventas { get; set; }
    }
}
