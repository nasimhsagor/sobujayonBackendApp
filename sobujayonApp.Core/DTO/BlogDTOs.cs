using System;
using System.Collections.Generic;

namespace sobujayonApp.Core.DTO
{
    public class BlogResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string? Thumbnail { get; set; }
        public string? Summary { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class BlogDetailsResponse : BlogResponse
    {
        public string? Content { get; set; }
        public string? Author { get; set; }
        public List<string> Tags { get; set; } = new();
    }

    public class CreateBlogRequest
    {
        public string Id { get; set; } // optional for update
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public string? Thumbnail { get; set; }
        public string? Summary { get; set; }
        public List<string> Tags { get; set; } = new();
    }
}
