using System.Collections.Generic;
using System.Threading.Tasks;
using sobujayonApp.Core.DTO;

namespace sobujayonApp.Core.ServiceContracts
{
    public interface ICategoriesService
    {
        Task<IEnumerable<CategoryResponse>> GetAllCategories();
        Task<IEnumerable<ProductResponse>> GetCategoryProducts(string slug);
        Task<CategoryResponse> CreateCategory(CreateCategoryRequest request);
    }
}
