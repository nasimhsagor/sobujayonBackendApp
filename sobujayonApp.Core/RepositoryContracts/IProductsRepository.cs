using sobujayonApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sobujayonApp.Core.RepositoryContracts
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product?> GetProductById(int id);
        Task<Product?> GetProductBySlug(string slug);
        Task<Product> AddProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);
    }
}
