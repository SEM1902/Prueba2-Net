using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PruebaNet.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [DisplayName("Nombre")]
        public string Name { get; set; }

        [DisplayName("Descripción")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        [DisplayName("Precio")]
        public decimal Price { get; set; }

        [DisplayName("Fecha de Creación")]
        public DateTime CreatedDate { get; set; }
    }
}
