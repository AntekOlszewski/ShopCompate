using ShopCompare.Modules.Catalog;

namespace ShopCompare.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationModules(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCatalogModule(configuration);

        return services;
    }
}