using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sobujayonApp.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!; // Error 3 was here
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        // Navigation property
        public virtual Category? Category { get; set; }
    }
}