// Models/Entities/Modifier.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deBurgas.Models.Entities 
{
    public class Modifier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ModifierId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty; // Ej: "Tipo de Queso"

        [Required]
        [MaxLength(50)]
        public string SelectionType { get; set; } = string.Empty; // Ej: "single" o "multiple"

        // Propiedad de navegación a las opciones
        public ICollection<Option> Options { get; set; } = new List<Option>();
    }
}