using AutoMapper;
using sobujayonApp.Core.DTO;
using sobujayonApp.Core.Entities;

namespace sobujayonApp.Core.Mappers
{
    public class GeneralMappingProfile : Profile
    {
        public GeneralMappingProfile()
        {
            // Category
            CreateMap<Category, CategoryResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
            CreateMap<CreateCategoryRequest, Category>()
                .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Name.ToLower().Replace(" ", "-")));

            // Product
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null));
            CreateMap<Product, ProductDetailsResponse>()
                 .IncludeBase<Product, ProductResponse>();
            
            CreateMap<CreateProductRequest, Product>()
                .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Name.ToLower().Replace(" ", "-")))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => int.Parse(src.Category_Id))); // Assuming ID passed as string

            // Cart (Manual mapping might be easier for complex logic, but here is basic)
            CreateMap<Cart, CartResponse>()
                 .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
            CreateMap<CartItem, CartItemResponse>()
                 .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId.ToString())); // or return Product object? request says list of {plantId, quantity}

            // Order
            CreateMap<Order, OrderSummaryResponse>()
                 .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id.ToString()));
            
            // Blog
            CreateMap<Blog, BlogResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
            CreateMap<Blog, BlogDetailsResponse>()
                .IncludeBase<Blog, BlogResponse>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Tags) ? new List<string>() : src.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()));
            
            CreateMap<CreateBlogRequest, Blog>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => string.Join(",", src.Tags)));

            // Common
            CreateMap<NavItem, NavItemResponse>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
            CreateMap<DeliveryArea, DeliveryAreaResponse>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
             
             // Review
             CreateMap<Review, ReviewResponse>();
        }
    }
}
