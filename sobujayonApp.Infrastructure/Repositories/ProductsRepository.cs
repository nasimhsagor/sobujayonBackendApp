using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sobujayonApp.Core.Entities;
using sobujayonApp.Core.RepositoryContracts;
using sobujayonApp.Infrastructure.DbContext;


namespace sobujayonApp.Infrastructure.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductsRepository(ApplicationDbContext db) => _db = db;

        public async Task<IEnumerable<Product>> GetAllProducts()
            => await _db.Products.Include(p => p.Category).ToListAsync();

        public async Task<Product?> GetProductById(int id)
            => await _db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

        public async Task<Product?> GetProductBySlug(string slug)
            => await _db.Products.FirstOrDefaultAsync(p => p.Slug == slug);

        public async Task<Product> AddProduct(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            _db.Products.Update(product);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return false;
            _db.Products.Remove(product);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}