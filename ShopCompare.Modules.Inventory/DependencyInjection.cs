using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopCompare.Modules.Inventory.Application;
using ShopCompare.Modules.Inventory.Application.Stock.GetStock;
using ShopCompare.Modules.Inventory.Application.Stock.ReserveStock;
using ShopCompare.Modules.Inventory.Infrastructure.Persistence;
using ShopCompare.Modules.Inventory.PublicApi;

namespace ShopCompare.Modules.Inventory;

public static class DependencyInjection
{
    public static IServiceCollection AddInventoryModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<InventoryDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("shopcompare-db"));
        });

        services.AddScoped<IInventoryModule, InventoryModule>();

        services.AddScoped<GetStockHandler>();
        services.AddScoped<ReserveStockHandler>();

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}