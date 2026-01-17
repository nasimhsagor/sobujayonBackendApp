using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using sobujayonApp.Core.DTO;

namespace sobujayonApp.Core.ServiceContracts
{
    public interface IOrderService
    {
        Task<OrderCreatedResponse> CreateOrder(Guid userId, CreateOrderRequest request);
        Task<IEnumerable<OrderSummaryResponse>> GetOrders(Guid userId);
    }
}
