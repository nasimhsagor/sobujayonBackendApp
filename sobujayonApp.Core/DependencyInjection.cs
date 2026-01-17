using sobujayonApp.Core.ServiceContracts;
using sobujayonApp.Core.Services;
using sobujayonApp.Core.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace sobujayonApp.Core;

public static class DependencyInjection
{
  /// <summary>
  /// Extension method to add core services to the dependency injection container
  /// </summary>
  /// <param name="services"></param>
  /// <returns></returns>
  public static IServiceCollection AddCore(this IServiceCollection services)
  {
    //TO DO: Add services to the IoC container
    //Core services often include validation, caching and other business components.

    services.AddTransient<IUsersService, UsersService>();
    services.AddTransient<IProductsService, ProductsService>();
    services.AddTransient<IJwtService, JwtService>();
    services.AddTransient<ICategoriesService, CategoriesService>();
    services.AddTransient<ICartService, CartService>();
    services.AddTransient<IOrderService, OrderService>();
    services.AddTransient<IBlogService, BlogService>();
    services.AddTransient<IWishlistService, WishlistService>();
    services.AddTransient<ICommonService, CommonService>();
    services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

    return services;
  }
}
