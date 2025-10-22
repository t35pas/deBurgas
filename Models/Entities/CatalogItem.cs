// Models/Entities/CatalogItem.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deBurgas.Models.Entities 
{ 
    public class CatalogItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProductId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string ProductType { get; set; } = string.Empty; // Ej: "BURGER"

        [Column(TypeName = "numeric(10, 2)")]
        public decimal BasePrice { get; set; }

        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        // Propiedad de navegación para la relación M:N con Modifiers
        public ICollection<CatalogItemModifier> ItemModifiers { get; set; } = new List<CatalogItemModifier>();
    }
}