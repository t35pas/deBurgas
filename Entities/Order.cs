using deBurgas.Models;

namespace deBurgas.Entities { 
    public class Order
    {
        public Guid OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string DeliveryAddress { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.PENDIENTE;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<OrderItem> Items { get; set; } = new();
    }

    // Entities/OrderItem.cs
    public class OrderItem
    {
        public Guid OrderItemId { get; set; }
        public Guid OrderId { get; set; } // Foreign Key
        public Guid ProductMenuId { get; set; }

        // Aquí guardaremos el JSONB
        public string ConfigurationDetails { get; set; }
    }
}