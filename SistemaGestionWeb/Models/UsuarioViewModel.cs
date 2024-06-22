using System.ComponentModel.DataAnnotations;

namespace SistemaGestionWeb.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(70)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(60)]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Contrasenia { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Mail { get; set; } = string.Empty;
    }
}
