using ECommerce.Persistance.Context;
using E_Commerce.Persistance.DbInitializer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Persistance.DependencyInjection;

public static class PersistanceServiceExtensions
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options 
            => options.UseSqlServer(configuration.GetConnectionString("SQLConnection"))
            );

        services.AddScoped<IDbInitializer, DbInitializer>();
        //services.AddScoped<IUnitOfWork, IUnitOfWork>();

        return services;
    }
}
