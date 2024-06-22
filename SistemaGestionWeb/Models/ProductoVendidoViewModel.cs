using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestionWeb.Models
{
    public class ProductoVendidoViewModel
    {
        public int Id { get; set; }

        [Required]
        public int IdProducto { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Stock { get; set; } = 0;

        [Required]
        public int IdVenta { get; set; } = 0;
    }
}
