using System.Collections.Generic;

namespace sobujayonApp.Core.DTO
{
    public class CartResponse
    {
        public List<CartItemResponse> Items { get; set; } = new();
        public decimal Total { get; set; }
    }

    public class CartItemResponse
    {
        public string ProductId { get; set; } // int as string
        public int Quantity { get; set; }
        // Maybe some product details included often? Spec example just shows list.
        // Wait, Spec for Cart GET: items: [{plantId: "plant-1", quantity: 2}].
        // I'll assume just IDs for now or maybe full object if UI needs it.
        // The spec example shows: 
        // items: [ {plantId: "plant-1", quantity: 2} ]
    }

    public class AddCartItemRequest
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
