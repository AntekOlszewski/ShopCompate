using ShopCompare.Modules.Notifications;
using ShopCompare.Modules.Orders;

namespace ShopCompare.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationModules(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddNotificationsModule(configuration);
        services.AddOrdersModule(configuration);

        return services;
    }
}