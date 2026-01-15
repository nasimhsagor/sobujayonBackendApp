using Microsoft.EntityFrameworkCore;
using sobujayonApp.Core.Entities;
using sobujayonApp.Core.RepositoryContracts;
using sobujayonApp.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sobujayonApp.Infrastructure.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoriesRepository(ApplicationDbContext db) => _db = db;

        public async Task<IEnumerable<Category>> GetAllCategories()
            => await _db.Categories.ToListAsync();

        public async Task<Category?> GetCategoryById(int id)
            => await _db.Categories.FindAsync(id);

        public async Task<Category> AddCategory(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category == null) return false;
            _db.Categories.Remove(category);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
