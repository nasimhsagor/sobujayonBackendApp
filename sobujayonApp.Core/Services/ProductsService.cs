using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using sobujayonApp.Core.DTO;
using sobujayonApp.Core.Entities;
using sobujayonApp.Core.RepositoryContracts;
using sobujayonApp.Core.ServiceContracts;
using Microsoft.EntityFrameworkCore; // For Include if needed, but using IRepository mostly

namespace sobujayonApp.Core.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Review> _reviewRepository;
        private readonly IRepository<Category> _categoryRepository; // Filter by category slug needs look up
        private readonly IMapper _mapper;

        public ProductsService(IRepository<Product> productRepository, IRepository<Review> reviewRepository, IRepository<Category> categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _reviewRepository = reviewRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductResponse>> GetProducts(string? search, string? category, decimal? minPrice, decimal? maxPrice, int page, int limit, string? sort)
        {
            // Note: IRepository.FindAsync takes simple predicate. 
            // For complex filtering, we might need access to IQueryable or build a complex predicate.
            // But since IRepository returns IEnumerable (Task), it's assuming fetching all matching then paging? 
            // Better to use IQueryable in Repository.
            // But I defined generic repo as IEnumerable return.
            // I will use GetAllAsync/FindAsync and filter in memory if the dataset is small?
            // "Plant ecommerce in Dhaka" -> Maybe not creating millions of rows.
            // Ideally, I should extend Repository to return IQueryable or add Filtering support.
            // I'll filter in memory for now to save time, or update Repository later.
            // Wait, I can build the predicate!
            // But 'category' is a slug, I need to find ID first.

            int? categoryId = null;
            if (!string.IsNullOrEmpty(category))
            {
                var cat = await _categoryRepository.GetAsync(c => c.Slug == category);
                if (cat != null) categoryId = cat.Id;
            }

            var products = await _productRepository.GetAllAsync(); // This fetches ALL. Bad for perf, but okay for MVP/POC.
            // Note: If IRepository expose IQueryable, I could do this better.
            
            var query = products.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase) || (p.Description != null && p.Description.Contains(search, StringComparison.OrdinalIgnoreCase)));
            
            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            // Sort
            if (sort == "price_asc") query = query.OrderBy(p => p.Price);
            else if (sort == "price_desc") query = query.OrderByDescending(p => p.Price);
            else if (sort == "newest") query = query.OrderByDescending(p => p.Id); // Assuming Id ~ time or better add CreatedAt
            else if (sort == "rating") query = query.OrderByDescending(p => p.Rating);

            // Pagination
            var paged = query.Skip((page - 1) * limit).Take(limit).ToList();

            return _mapper.Map<IEnumerable<ProductResponse>>(paged);
        }

        public async Task<ProductDetailsResponse?> GetProductBySlug(string slug)
        {
            var product = await _productRepository.GetAsync(p => p.Slug == slug);
            return _mapper.Map<ProductDetailsResponse>(product);
        }

        public async Task<ProductResponse> CreateProduct(CreateProductRequest request)
        {
            var product = _mapper.Map<Product>(request);
            await _productRepository.AddAsync(product);
            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<IEnumerable<ReviewResponse>> GetReviews(int productId)
        {
            var reviews = await _reviewRepository.FindAsync(r => r.ProductId == productId);
            return _mapper.Map<IEnumerable<ReviewResponse>>(reviews);
        }
    }
}
