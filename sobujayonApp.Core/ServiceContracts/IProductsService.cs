using sobujayonApp.Core.Entities;
using sobujayonApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sobujayonApp.Core.ServiceContracts
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductResponse>> GetProducts(string? search, string? category, decimal? minPrice, decimal? maxPrice, int page, int limit, string? sort);
        Task<ProductDetailsResponse?> GetProductBySlug(string slug);
        Task<ProductResponse> CreateProduct(CreateProductRequest request);
        Task<IEnumerable<ReviewResponse>> GetReviews(int productId); // productId as int internal
    }
}
