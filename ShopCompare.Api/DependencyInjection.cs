using ShopCompare.Modules.Catalog;
using ShopCompare.Modules.Inventory;

namespace ShopCompare.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationModules(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCatalogModule(configuration);
        services.AddInventoryModule(configuration);

        return services;
    }
}