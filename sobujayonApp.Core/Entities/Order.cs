using System;
using System.Collections.Generic;

namespace sobujayonApp.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string AddressId { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!; // "ONLINE", "COD"
        public string Status { get; set; } = "Pending";
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ApplicationUser? User { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } // Price at the time of order

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
