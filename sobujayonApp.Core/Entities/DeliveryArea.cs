using System;

namespace sobujayonApp.Core.Entities
{
    public class DeliveryArea
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal DeliveryFee { get; set; }
    }
}
