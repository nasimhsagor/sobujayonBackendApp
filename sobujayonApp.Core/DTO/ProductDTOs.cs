using System.Collections.Generic;

namespace sobujayonApp.Core.DTO
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public decimal? OriginalPrice { get; set; }
        public string? Image { get; set; }
        public double Rating { get; set; }
        public int ReviewsCount { get; set; }
        public string? Description { get; set; }
        public string? Badge { get; set; }
        public string? BadgeColor { get; set; }
    }

    public class ProductDetailsResponse : ProductResponse
    {
        public int Stock { get; set; }
    }

    public class CreateProductRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; } // Spec says string "cat-1", but internally int. Validation needed. DTO can take string if we want to parse it? The spec example "category_id": "cat-1" suggests string. I'll stick to int or maybe handle conversion. Let's use string to match spec, but backend is int.
        // Actually, let's use string to be safe and parse it in controller/service.
        public string Category_Id { get; set; }
    }
}
