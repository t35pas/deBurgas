// DTOs/OrderRequest.cs

// Este DTO representa un ítem individual en el carrito
public record CartItemDto(
    Guid ProductId,
    string Name,
    int Quantity,
    decimal BasePrice,
    List<SelectedModifierDto> Modifiers
);

public record SelectedModifierDto(
    string Name,
    string Option,
    decimal Price
);

// Este es el DTO principal que recibirá el endpoint
public record OrderRequestDto(
    string CustomerName,
    string CustomerPhone,
    string DeliveryAddress,
    List<CartItemDto> Items
);