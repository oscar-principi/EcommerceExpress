using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Entities.Productos
{
    public class Producto
    {
        [Key]
        [Required(ErrorMessage = "El ID del producto es obligatorio.")]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del producto no puede exceder los 100 caracteres.")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "La descripcion del producto es obligatorio.")]
        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public required string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio del producto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El stock del producto es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "La imagen del producto es obligatorio.")]
        [Url(ErrorMessage = "La URL de la imagen no es válida.")]
        public required string ImagenUrl { get; set; }

        [Required(ErrorMessage = "La fecha de creación es obligatoria.")]
        public DateTime FechaCreacion { get; set; }

        [Required(ErrorMessage = "Debe especificar si el producto está activo.")]
        public bool EstaActivo { get; set; }


        [Required(ErrorMessage = "El ID de la categoría es obligatorio.")]
        public int CategoriaId { get; set; }

        public required Categoria Categoria { get; set; }
    }
}
