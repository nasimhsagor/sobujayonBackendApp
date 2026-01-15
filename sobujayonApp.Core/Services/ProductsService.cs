using sobujayonApp.Core.Entities;
using sobujayonApp.Core.RepositoryContracts;
using sobujayonApp.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sobujayonApp.Core.Services
{
    internal class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productsRepository.GetAllProducts();
        }

        public async Task<Product?> GetProductBySlug(string slug)
        {
            return await _productsRepository.GetProductBySlug(slug);
        }

        public async Task<Product> CreateProduct(Product product)
        {
            // Business Logic: Automatically generate a URL-friendly slug
            if (string.IsNullOrWhiteSpace(product.Slug))
            {
                product.Slug = product.Name.ToLower()
                    .Replace(" ", "-")
                    .Replace("'", "");
            }

            return await _productsRepository.AddProduct(product);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            return await _productsRepository.DeleteProduct(id);
        }
    }
}
