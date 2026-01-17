using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using sobujayonApp.Core.DTO;
using sobujayonApp.Core.Entities;
using sobujayonApp.Core.RepositoryContracts;
using sobujayonApp.Core.ServiceContracts;

namespace sobujayonApp.Core.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IRepository<WishlistItem> _wishlistRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public WishlistService(IRepository<WishlistItem> wishlistRepository, IRepository<Product> productRepository, IMapper mapper)
        {
            _wishlistRepository = wishlistRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task AddToWishlist(Guid userId, string productId)
        {
            int pid = int.Parse(productId);
            var exists = await _wishlistRepository.GetAsync(w => w.UserId == userId && w.ProductId == pid);
            if (exists == null)
            {
                await _wishlistRepository.AddAsync(new WishlistItem { UserId = userId, ProductId = pid });
            }
        }

        public async Task<IEnumerable<ProductResponse>> GetWishlist(Guid userId)
        {
            var items = await _wishlistRepository.FindAsync(w => w.UserId == userId);
            var products = new List<Product>();
            foreach(var item in items)
            {
                var p = await _productRepository.GetByIdAsync(item.ProductId);
                if(p != null) products.Add(p);
            }
            return _mapper.Map<IEnumerable<ProductResponse>>(products);
        }
    }
}
