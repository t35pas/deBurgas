// Models/Entities/CatalogItemModifier.cs
// Esta clase representa la tabla de unión (M:N) entre productos y modificadores.

namespace deBurgas.Models.Entities 
{
    public class CatalogItemModifier
    {
        // Claves Primarias Compuestas (PK) y Claves Foráneas (FK)
        public Guid ProductId { get; set; }
        public Guid ModifierId { get; set; }

        // Propiedades de navegación
        public CatalogItem CatalogItem { get; set; } = null!;
        public Modifier Modifier { get; set; } = null!;
    }
}