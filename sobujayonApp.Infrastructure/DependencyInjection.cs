using sobujayonApp.Core.RepositoryContracts;
using sobujayonApp.Infrastructure.DbContext;
using sobujayonApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace sobujayonApp.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Extension method to add infrastructure services to the dependency injection container
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add DbContext with SQL Server
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"));
        });

        // Add repository
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IProductsRepository, ProductsRepository>();

        return services;
    }
}