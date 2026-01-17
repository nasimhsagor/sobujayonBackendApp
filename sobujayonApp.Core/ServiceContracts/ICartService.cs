using System;
using System.Threading.Tasks;
using sobujayonApp.Core.DTO;

namespace sobujayonApp.Core.ServiceContracts
{
    public interface ICartService
    {
        Task<CartResponse> GetCart(Guid userId);
        Task AddToCart(Guid userId, AddCartItemRequest request);
    }
}
