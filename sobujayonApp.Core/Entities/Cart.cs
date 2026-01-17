using System;
using System.Collections.Generic;

namespace sobujayonApp.Core.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        
        public virtual ApplicationUser? User { get; set; }
        public virtual ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }

    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Cart? Cart { get; set; }
        public virtual Product? Product { get; set; }
    }
}
