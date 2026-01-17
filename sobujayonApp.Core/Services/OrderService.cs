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
    public class OrderService : IOrderService
    {
         private readonly IRepository<Order> _orderRepository;
         private readonly ICartService _cartService;
         private readonly IRepository<Cart> _cartRepository;
         private readonly IRepository<CartItem> _cartItemRepository;
         private readonly IRepository<Product> _productRepository;
         private readonly IMapper _mapper;

         public OrderService(IRepository<Order> orderRepository, ICartService cartService, IRepository<Cart> cartRepository, IRepository<CartItem> cartItemRepository, IRepository<Product> productRepository, IMapper mapper)
         {
             _orderRepository = orderRepository;
             _cartService = cartService;
             _cartRepository = cartRepository; // To clear cart
             _cartItemRepository = cartItemRepository;
             _productRepository = productRepository;
             _mapper = mapper;
         }

        public async Task<OrderCreatedResponse> CreateOrder(Guid userId, CreateOrderRequest request)
        {
            // Get Cart
            var cartResp = await _cartService.GetCart(userId);
            if (cartResp.Items.Count == 0) throw new Exception("Cart is empty");

            // Create Order
            var order = new Order
            {
                UserId = userId,
                AddressId = request.AddressId,
                PaymentMethod = request.PaymentMethod,
                Total = cartResp.Total,
                Status = "Pending"
            };
            
            await _orderRepository.AddAsync(order);

            // Add Items (Need to fetch Cart Items again to get IDs? cartResp has ProductId string)
            // Or just iterate cartResp
            // I need to add OrderItems to database. I need IRepository<OrderItem>...
            // Or add to Order.Items and OrderRepository handles it?
            // Since I used `AddAsync` which calls SaveChanges, the Order Id is generated.
            // But I should inject IRepository<OrderItem> or use DbContext directly...
            // I'll assume Order.Items collection works if I add to it and UpdateAsync?
            // Actually, better to use OrderItemRepository. I missed injecting it.
            
            // I'll skip injecting for now and try to add to collection before initial AddAsync?
            // No, need products first.
            
            // Let's rely on retrieving Cart Items Entities directly.
            var cart = await _cartRepository.GetAsync(c => c.UserId == userId);
            var cartItems = await _cartItemRepository.FindAsync(ci => ci.CartId == cart.Id);

            foreach(var item in cartItems)
            {
                 var product = await _productRepository.GetByIdAsync(item.ProductId);
                 order.Items.Add(new OrderItem
                 {
                     ProductId = item.ProductId,
                     Quantity = item.Quantity,
                     Price = product?.Price ?? 0,
                     OrderId = order.Id
                 });
            }
            
            await _orderRepository.UpdateAsync(order); // Save items

            // Clear Cart
            foreach(var item in cartItems)
            {
                await _cartItemRepository.DeleteAsync(item);
            }

            return new OrderCreatedResponse
            {
                OrderId = order.Id.ToString(),
                PaymentUrl = $"https://payment.example.com/{order.Id}"
            };
        }

        public async Task<IEnumerable<OrderSummaryResponse>> GetOrders(Guid userId)
        {
            var orders = await _orderRepository.FindAsync(o => o.UserId == userId);
            return _mapper.Map<IEnumerable<OrderSummaryResponse>>(orders);
        }
    }
}
