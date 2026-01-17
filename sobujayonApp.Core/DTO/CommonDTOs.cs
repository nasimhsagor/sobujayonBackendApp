using System.Collections.Generic;

namespace sobujayonApp.Core.DTO
{
    public class NavItemResponse
    {
        public string Id { get; set; }
        public string NameEn { get; set; } // mapped from NameEn
        public string NameBn { get; set; } // mapped from NameBn
        public string Slug { get; set; }
    }

    public class DeliveryAreaResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal DeliveryFee { get; set; }
    }

    public class ReviewResponse
    {
        public int Rating { get; set; }
        public string? Comment { get; set; }
        // Maybe user name?
    }
}
