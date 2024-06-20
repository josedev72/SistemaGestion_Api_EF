using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionEntities
{
    public class Usuario
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
