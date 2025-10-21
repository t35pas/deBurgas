// Controllers/OrdersController.cs
using Microsoft.AspNetCore.Mvc;
using System.Text.Json; // Importante para la serialización

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    // Aquí deberías inyectar tu DbContext
    // private readonly AppDbContext _context;
    // public OrdersController(AppDbContext context) { _context = context; }

    [HttpPost]
    public IActionResult CreateOrder([FromBody] OrderRequestDto orderRequest)
    {
        // --- 1. Validación Básica ---
        if (orderRequest.Items == null || !orderRequest.Items.Any())
        {
            return BadRequest("El pedido no puede estar vacío.");
        }

        // --- 2. Lógica para crear las entidades ---

        // Calcular el total
        var total = orderRequest.Items.Sum(item =>
            item.BasePrice * item.Quantity + item.Modifiers.Sum(m => m.Price * item.Quantity)
        );

        // Crear la entidad Order principal
        var newOrder = new Order
        {
            OrderId = Guid.NewGuid(),
            CustomerName = orderRequest.CustomerName,
            CustomerPhone = orderRequest.CustomerPhone,
            DeliveryAddress = orderRequest.DeliveryAddress,
            TotalAmount = total
        };

        // Crear las entidades OrderItem
        foreach (var itemDto in orderRequest.Items)
        {
            var orderItem = new OrderItem
            {
                OrderItemId = Guid.NewGuid(),
                OrderId = newOrder.OrderId,
                ProductMenuId = itemDto.ProductId,

                // Serializar el detalle del ítem a JSON para guardarlo en el campo JSONB
                ConfigurationDetails = JsonSerializer.Serialize(itemDto)
            };
            newOrder.Items.Add(orderItem);
        }

        // --- 3. Guardar en la Base de Datos (simulado) ---
        // En un proyecto real, aquí llamarías a tu DbContext:
        // _context.Orders.Add(newOrder);
        // await _context.SaveChangesAsync();

        Console.WriteLine($"Pedido recibido de: {newOrder.CustomerName}. Total: {newOrder.TotalAmount}");
        Console.WriteLine($"Detalle: {JsonSerializer.Serialize(newOrder.Items)}");

        // --- 4. Devolver una respuesta exitosa ---
        // El código 201 Created es el estándar para la creación de un recurso.
        return CreatedAtAction(nameof(CreateOrder), new { id = newOrder.OrderId }, newOrder);
    }
}