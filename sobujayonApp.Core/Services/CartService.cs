using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using sobujayonApp.Core.DTO;
using sobujayonApp.Core.Entities;
using sobujayonApp.Core.RepositoryContracts;
using sobujayonApp.Core.ServiceContracts;

namespace sobujayonApp.Core.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<CartItem> _cartItemRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public CartService(IRepository<Cart> cartRepository, IRepository<CartItem> cartItemRepository, IRepository<Product> productRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CartResponse> GetCart(Guid userId)
        {
            var cart = await _cartRepository.GetAsync(c => c.UserId == userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                await _cartRepository.AddAsync(cart);
            }
            
            // Need to load items. Generic GetAsync might not include children unless configured.
            // Using FindAsync for items.
            var items = await _cartItemRepository.FindAsync(i => i.CartId == cart.Id);
            cart.Items = items.ToList();

            var resp = _mapper.Map<CartResponse>(cart);
            
            // Calculate total
            decimal total = 0;
            foreach (var item in items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product != null)
                {
                    total += product.Price * item.Quantity;
                }
            }
            resp.Total = total;
            return resp;
        }

        public async Task AddToCart(Guid userId, AddCartItemRequest request)
        {
            var cart = await _cartRepository.GetAsync(c => c.UserId == userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                await _cartRepository.AddAsync(cart);
            }

            // Check if item exists
            var existing = await _cartItemRepository.GetAsync(ci => ci.CartId == cart.Id && ci.ProductId.ToString() == request.ProductId);
            // ProductId in request is string? But in Entity int.
            // Wait, request.ProductId is string (e.g. "1").
            int pid = int.Parse(request.ProductId);
            
             // Re-query with int
            var existingItem = await _cartItemRepository.GetAsync(ci => ci.CartId == cart.Id && ci.ProductId == pid);

            if (existingItem != null)
            {
                existingItem.Quantity += request.Quantity;
                await _cartItemRepository.UpdateAsync(existingItem);
            }
            else
            {
                var newItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = pid,
                    Quantity = request.Quantity
                };
                await _cartItemRepository.AddAsync(newItem);
            }
        }
    }
}
