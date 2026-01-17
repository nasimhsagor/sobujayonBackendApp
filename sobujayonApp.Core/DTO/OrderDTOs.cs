using System;
using System.Collections.Generic;

namespace sobujayonApp.Core.DTO
{
    public class OrderSummaryResponse
    {
        public string OrderId { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
    }

    public class OrderCreatedResponse
    {
        public string OrderId { get; set; }
        public string PaymentUrl { get; set; }
    }

    public class CreateOrderRequest
    {
        public string AddressId { get; set; }
        public string PaymentMethod { get; set; } // ONLINE, COD
    }
}
