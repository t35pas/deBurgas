// Controllers/AdminController.cs
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    // Inyectamos servicios:
    // private readonly AppDbContext _context; // Para la DB
    private readonly MetaNotificationService _metaService;
    // private readonly PmvService _pmvService; // Para el PMV (si lo implementas)

    public AdminController(MetaNotificationService metaService /*, AppDbContext context, PmvService pmvService */)
    {
        _metaService = metaService;
        // _context = context;
        // _pmvService = pmvService;
    }

    [HttpPut("orders/{id:guid}/status")]
    public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] OrderStatusUpdateDto statusUpdate)
    {
        // --- 1. Buscar y Validar el Pedido (Simulación) ---
        // En una app real: var order = await _context.Orders.FindAsync(id);
        var order = new Order
        {
            OrderId = id,
            CustomerPhone = "54911xxxxxxxx",
            Status = OrderStatus.PENDIENTE
        };

        if (order == null)
        {
            return NotFound($"Pedido con ID {id} no encontrado.");
        }

        // --- 2. Lógica de Negocio y Persistencia ---

        OrderStatus oldStatus = order.Status;

        // En una app real: _context.Entry(order).State = EntityState.Modified;
        // await _context.SaveChangesAsync();

        // --- 3. Desencadenar Acciones (WhatsApp y PMV) ---

        // A. Notificación por WhatsApp
        if (order.Status != oldStatus)
        {
            await _metaService.SendStatusUpdateAsync(
                order.OrderId,
                order.CustomerPhone,
                order.Status
            );
        }

        // B. Lógica del PMV (Ejemplo: si pasa a "LISTO_PARA_RETIRO")
        if (order.Status == OrderStatus.LISTO_PARA_RETIRO)
        {
            // En una app real: await _pmvService.SendNewReadyOrder(order.OrderId);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[PMV MOCK]: Se dispara la actualización para mostrar el Pedido {id} en el panel.");
            Console.ResetColor();
        }

        return Ok(new { OrderId = id, NewStatus = order.Status, Message = "Estado actualizado y acciones disparadas." });
    }

    [HttpGet("orders")]
    public async Task<IActionResult> GetOrdersByStatus([FromQuery] string[] status)
    {
        // 1. Empezamos con una consulta base a todos los pedidos.
        // IQueryable permite construir la consulta dinámicamente.
        // var query = _context.Orders.AsQueryable();

        // Simulación con datos en memoria para la prueba
        var allOrders = new List<Order> {
            new Order { OrderId = Guid.NewGuid(), Status = OrderStatus.PENDIENTE },
            new Order { OrderId = Guid.NewGuid(), Status = OrderStatus.CONFIRMADO },
            new Order { OrderId = Guid.NewGuid(), Status = OrderStatus.EN_PREPARACION },
            new Order { OrderId = Guid.NewGuid(), Status = OrderStatus.PENDIENTE },
        }.AsQueryable();
        var query = allOrders;


        // 2. Si el cliente especificó estados, aplicamos el filtro.
        if (status != null && status.Length > 0)
        {
            // Convertimos los strings del query a nuestra enum para una comparación segura.
            var statusEnums = new List<OrderStatus>();
            foreach (var s in status)
            {
                if (Enum.TryParse<OrderStatus>(s.ToUpper(), out var parsedStatus))
                {
                    statusEnums.Add(parsedStatus);
                }
            }

            if (statusEnums.Any())
            {
                // Filtramos donde el estado del pedido esté en la lista de estados solicitados.
                query = query.Where(o => statusEnums.Contains(o.Status));
            }
        }

        // Opcional: Podrías añadir paginación y ordenamiento aquí.
        // query = query.OrderByDescending(o => o.CreatedAt).Skip(page * size).Take(size);

        // 3. Ejecutamos la consulta y devolvemos los resultados.
        // var orders = await query.ToListAsync();
        var orders = query.ToList(); // Usamos ToList() para la simulación en memoria

        return Ok(orders);
    }
}