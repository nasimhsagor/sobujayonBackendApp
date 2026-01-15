using sobujayonApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sobujayonApp.Core.RepositoryContracts
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category?> GetCategoryById(int id);
        Task<Category> AddCategory(Category category);
        Task<bool> DeleteCategory(int id);
    }
}
