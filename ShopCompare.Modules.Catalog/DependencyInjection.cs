using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopCompare.Modules.Catalog.Application;
using ShopCompare.Modules.Catalog.Application.Products.GetProductById;
using ShopCompare.Modules.Catalog.Application.Products.GetProducts;
using ShopCompare.Modules.Catalog.Application.Products.SeedProducts;
using ShopCompare.Modules.Catalog.Infrastructure.Persistence;
using ShopCompare.Modules.Catalog.PublicApi;

namespace ShopCompare.Modules.Catalog;

public static class DependencyInjection
{
    public static IServiceCollection AddCatalogModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<CatalogDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("shopcompare-db"));
        });

        services.AddScoped<ICatalogModule, CatalogModule>();

        services.AddScoped<GetProductsHandler>();
        services.AddScoped<GetProductByIdHandler>();
        services.AddScoped<SeedProductsHandler>();

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}