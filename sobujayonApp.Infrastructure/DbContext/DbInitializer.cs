using sobujayonApp.Core.Entities;
using sobujayonApp.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sobujayonApp.Infrastructure.DbContext
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            // Seed Categories
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Indoor Plants", Slug = "indoor-plants", Description = "Plants perfect for your home interior.", Image = "https://example.com/indoor.jpg" },
                    new Category { Name = "Outdoor Plants", Slug = "outdoor-plants", Description = "Beautify your garden or balcony.", Image = "https://example.com/outdoor.jpg" },
                    new Category { Name = "Succulents", Slug = "succulents", Description = "Low maintenance water storing plants.", Image = "https://example.com/succulent.jpg" },
                    new Category { Name = "Flowering Plants", Slug = "flowering-plants", Description = "Plants with beautiful blooms.", Image = "https://example.com/flower.jpg" }
                };
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            // Seed Products
            if (!context.Products.Any())
            {
                var indoorCat = context.Categories.FirstOrDefault(c => c.Slug == "indoor-plants");
                var outdoorCat = context.Categories.FirstOrDefault(c => c.Slug == "outdoor-plants");
                
                if (indoorCat != null && outdoorCat != null)
                {
                    var products = new List<Product>
                    {
                        new Product { Name = "Monstera Deliciosa", Slug = "monstera-deliciosa", Price = 1200, OriginalPrice = 1500, Stock = 20, Description = "Famous Split-leaf Philodendron.", CategoryId = indoorCat.Id, Image = "https://example.com/monstera.jpg", Rating = 4.8, ReviewsCount = 120, Badge = "Bestseller", BadgeColor = "secondary" },
                        new Product { Name = "Snake Plant", Slug = "snake-plant", Price = 800, Stock = 50, Description = "Hard to kill, air purifying.", CategoryId = indoorCat.Id, Image = "https://example.com/snake.jpg", Rating = 4.9, ReviewsCount = 200, Badge = "Beginner Friendly", BadgeColor = "success" },
                        new Product { Name = "Fiddle Leaf Fig", Slug = "fiddle-leaf-fig", Price = 2500, Stock = 10, Description = "Trendy statement plant.", CategoryId = indoorCat.Id, Image = "https://example.com/fiddle.jpg", Rating = 4.5, ReviewsCount = 80 },
                        new Product { Name = "Rose Plant", Slug = "rose-plant", Price = 400, Stock = 100, Description = "Classic red roses.", CategoryId = outdoorCat.Id, Image = "https://example.com/rose.jpg", Rating = 4.6, ReviewsCount = 150 }
                    };
                    await context.Products.AddRangeAsync(products);
                    await context.SaveChangesAsync();
                }
            }

            // Seed Delivery Areas
            if (!context.DeliveryAreas.Any())
            {
                var areas = new List<DeliveryArea>
                {
                    new DeliveryArea { Name = "Dhanmondi", DeliveryFee = 60 },
                    new DeliveryArea { Name = "Gulshan", DeliveryFee = 80 },
                    new DeliveryArea { Name = "Banani", DeliveryFee = 80 },
                    new DeliveryArea { Name = "Mirpur", DeliveryFee = 50 },
                    new DeliveryArea { Name = "Uttara", DeliveryFee = 100 }
                };
                await context.DeliveryAreas.AddRangeAsync(areas);
                await context.SaveChangesAsync();
            }

            // Seed Blogs
            if (!context.Blogs.Any())
            {
                var blogs = new List<Blog>
                {
                    new Blog { Title = "Top 10 Indoor Plants", Slug = "top-10-indoor-plants", CreatedAt = DateTime.UtcNow.AddDays(-10), Summary = "Best plants for your living room.", Content = "Here is the list of top 10 indoor plants...", Thumbnail = "https://example.com/blog1.jpg", Author = "Admin", Tags = "indoor,decoration" },
                    new Blog { Title = "How to Care for Succulents", Slug = "how-to-care-succulents", CreatedAt = DateTime.UtcNow.AddDays(-5), Summary = "Watering and light tips.", Content = "Succulents need less water...", Thumbnail = "https://example.com/blog2.jpg", Author = "Gardener", Tags = "tips,succulents" }
                };
                await context.Blogs.AddRangeAsync(blogs);
                await context.SaveChangesAsync();
            }

            // Seed Nav Items
            if (!context.NavItems.Any())
            {
                var navItems = new List<NavItem>
                {
                    new NavItem { NameEn = "Home", NameBn = "হোম", Slug = "/", Order = 1 },
                    new NavItem { NameEn = "Shop", NameBn = "শপ", Slug = "/products", Order = 2 },
                    new NavItem { NameEn = "Blogs", NameBn = "ব্লগ", Slug = "/blogs", Order = 3 },
                    new NavItem { NameEn = "About Us", NameBn = "আমাদের সম্পর্কে", Slug = "/about", Order = 4 }
                };
                await context.NavItems.AddRangeAsync(navItems);
                await context.SaveChangesAsync();
            }
        }
    }
}
