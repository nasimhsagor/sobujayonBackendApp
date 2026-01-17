using System.Collections.Generic;

namespace sobujayonApp.Core.DTO
{
    public class CategoryResponse
    {
        public string Id { get; set; } // int to string for spec
        public string Slug { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
    }

    public class CreateCategoryRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
