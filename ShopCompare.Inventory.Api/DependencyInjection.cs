using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ShopCompare.Inventory.Api.Application.Stock.GetStock;
using ShopCompare.Inventory.Api.Application.Stock.ReserveStock;
using ShopCompare.Inventory.Api.Application.Stock.SeedStock;
using ShopCompare.Inventory.Api.Clients.Catalog;
using ShopCompare.Inventory.Api.Infrastructure.Persistence;

namespace ShopCompare.Inventory.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddInventoryApi(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<InventoryDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("inventory-db"));
        });
        
        services.AddHttpClient<ICatalogClient, CatalogClient>(client =>
        {
            client.BaseAddress = new Uri("https+http://catalog-api");
            client.Timeout = TimeSpan.FromSeconds(5);
        });

        services.AddScoped<GetStockHandler>();
        services.AddScoped<ReserveStockHandler>();
        services.AddScoped<SeedStockHandler>();

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}