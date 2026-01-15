using sobujayonApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sobujayonApp.Core.ServiceContracts
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product?> GetProductBySlug(string slug);
        Task<Product> CreateProduct(Product product);
        Task<bool> DeleteProduct(int id);
    }
}
