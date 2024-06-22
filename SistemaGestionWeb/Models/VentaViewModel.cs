using System.ComponentModel.DataAnnotations;

namespace SistemaGestionWeb.Models
{
    public class VentaViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Comentarios { get; set; } = string.Empty;

        [Required]
        public int IdUsuario { get; set; } = 0;
    }
}
