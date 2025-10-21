// DTOs/OrderStatusUpdateDto.cs
public record OrderStatusUpdateDto(
    string NewStatus // Ej: "EN_PREPARACION", "LISTO_PARA_RETIRO", "CANCELADO"
);