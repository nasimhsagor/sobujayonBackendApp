using System;
using System.Collections.Generic;

namespace sobujayonApp.Core.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? Thumbnail { get; set; }
        public string? Summary { get; set; }
        public string? Content { get; set; }
        public string? Author { get; set; }
        public string? Tags { get; set; } // CSV string for simplicity
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
