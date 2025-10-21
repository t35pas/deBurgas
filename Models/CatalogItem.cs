// Catálogo: src/Models/CatalogItem.cs
public record CatalogItem(
    Guid ProductId,
    string Name,
    string ProductType, // Ej: "BURGER"
    decimal BasePrice,
    string? Description,
    string? ImageUrl,
    List<Guid> ModifierIds // IDs de los modificadores que aplican
);

// Catálogo: src/Models/Modifier.cs
public record Modifier(
    Guid ModifierId,
    string Name,        // Ej: "Tipo de Queso"
    string SelectionType, // Ej: "single" o "multiple"
    List<Guid> OptionIds // IDs de las opciones disponibles
);

// Catálogo: src/Models/Option.cs
public record Option(
    Guid OptionId,
    string Name,        // Ej: "Cheddar"
    decimal AdditionalPrice // Ej: 0.50m
);