﻿using Shared.Models.Entities.Productos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Productos
{
    public class CategoriaDTO
    {
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
