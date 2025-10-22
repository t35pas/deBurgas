// Services/MetaNotificationService.cs
using deBurgas.Models;

public class MetaNotificationService
{
    // Simula el envío de un mensaje de WhatsApp
    public Task SendStatusUpdateAsync(Guid orderId, string customerPhone, OrderStatus newStatus)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n[META API MOCK]: Notificación enviada a {customerPhone}.");
        Console.WriteLine($"  - Pedido: {orderId}");
        Console.WriteLine($"  - Estado: El estado de su pedido ha cambiado a '{newStatus}'");
        Console.ResetColor();
        return Task.CompletedTask;
    }
}