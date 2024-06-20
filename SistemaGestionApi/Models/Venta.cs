﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionEntities
{
    public class Venta
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Comentarios { get; set; } = string.Empty;

        [Required]
        public int IdUsuario { get; set; } = 0;
    }
}
