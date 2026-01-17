using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using sobujayonApp.Core.DTO;
using sobujayonApp.Core.Entities;
using sobujayonApp.Core.RepositoryContracts;
using sobujayonApp.Core.ServiceContracts;

namespace sobujayonApp.Core.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public CategoriesService(IRepository<Category> categoryRepository, IRepository<Product> productRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryResponse>>(categories);
        }

        public async Task<IEnumerable<ProductResponse>> GetCategoryProducts(string slug)
        {
            var category = await _categoryRepository.GetAsync(c => c.Slug == slug);
            if (category == null) return new List<ProductResponse>();

            var products = await _productRepository.FindAsync(p => p.CategoryId == category.Id);
            return _mapper.Map<IEnumerable<ProductResponse>>(products);
        }

        public async Task<CategoryResponse> CreateCategory(CreateCategoryRequest request)
        {
            var category = _mapper.Map<Category>(request);
            await _categoryRepository.AddAsync(category);
            return _mapper.Map<CategoryResponse>(category);
        }
    }
}
