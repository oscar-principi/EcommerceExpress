using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Entities.Productos
{
    public class Categoria
    {
        [Key]
        [Required(ErrorMessage = "El ID de la categoría es obligatorio.")]
        public int? CategoriaId { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre de la categoría no puede exceder los 100 caracteres.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "La descripcion de la categoría es obligatoria.")]
        [StringLength(500, ErrorMessage = "La descripcion de la categoría no puede exceder los 500 caracteres.")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "Debe especificar si el producto está activo.")]
        public bool? EstaActivo { get; set; }

        public ICollection<Producto>? Productos { get; set; } // Relación uno a muchos
    }
}
