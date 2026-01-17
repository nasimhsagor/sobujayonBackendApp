using System;

namespace sobujayonApp.Core.Entities
{
    public class NavItem
    {
        public int Id { get; set; }
        public string NameEn { get; set; } = null!;
        public string NameBn { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public int Order { get; set; }
    }
}
