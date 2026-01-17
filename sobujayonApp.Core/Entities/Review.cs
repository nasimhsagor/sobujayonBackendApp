using System;

namespace sobujayonApp.Core.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Product? Product { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
