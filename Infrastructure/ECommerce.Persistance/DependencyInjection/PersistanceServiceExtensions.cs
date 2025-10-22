using E_Commerce.Persistance.DbInitializer;
using ECommerce.Persistance.Context;
using ECommerce.Persistance.Repositories;
using ECommerce.Persistance.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace ECommerce.Persistance.DependencyInjection;

public static class PersistanceServiceExtensions
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options 
            => options.UseSqlServer(configuration.GetConnectionString("SQLConnection"))
            );
        services.AddSingleton<IConnectionMultiplexer>(cfg => 
        {
            return ConnectionMultiplexer
            .Connect(configuration.GetConnectionString("RedisConnection")!);
        });
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<ICashService, CashService>();

        services.AddScoped<IDbInitializer, DbInitializer>();
        //services.AddScoped<IUnitOfWork, IUnitOfWork>();

        return services;
    }
}
