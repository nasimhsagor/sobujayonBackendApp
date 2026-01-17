using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sobujayonApp.Core.Entities
{
    using System.Text.Json.Serialization; // Add this

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? Description { get; set; }
        public string? Image { get; set; }

        [JsonIgnore] // This stops the cycle
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
