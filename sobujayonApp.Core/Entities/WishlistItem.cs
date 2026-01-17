using System;

namespace sobujayonApp.Core.Entities
{
    public class WishlistItem
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        public virtual ApplicationUser? User { get; set; }
        public virtual Product? Product { get; set; }
    }
}
