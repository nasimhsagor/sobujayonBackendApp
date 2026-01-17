using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using sobujayonApp.Core.DTO;

namespace sobujayonApp.Core.ServiceContracts
{
    public interface IWishlistService
    {
        Task AddToWishlist(Guid userId, string productId);
        Task<IEnumerable<ProductResponse>> GetWishlist(Guid userId);
    }
}
