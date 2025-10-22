// Models/Entities/Option.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deBurgas.Models.Entities 
{
    public class Option
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OptionId { get; set; }

        [Required]
        public Guid ModifierId { get; set; } // Clave Foránea

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty; // Ej: "Cheddar"

        [Column(TypeName = "numeric(10, 2)")]
        public decimal AdditionalPrice { get; set; }

        // Propiedad de navegación al Modificador (si es necesario)
        public Modifier? Modifier { get; set; }
    }
}