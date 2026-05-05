using ShopCompare.Modules.Cart;
using ShopCompare.Catalog.Api;
using ShopCompare.Modules.Inventory;
using ShopCompare.Modules.Notifications;
using ShopCompare.Modules.Orders;
using ShopCompare.Modules.Payments;

namespace ShopCompare.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationModules(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCatalogModule(configuration);
        services.AddInventoryModule(configuration);
        services.AddCartModule(configuration);
        services.AddPaymentsModule(configuration);
        services.AddNotificationsModule(configuration);
        services.AddOrdersModule(configuration);

        return services;
    }
}