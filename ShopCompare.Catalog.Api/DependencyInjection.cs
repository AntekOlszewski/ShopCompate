using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ShopCompare.Catalog.Api.Application.Products.GetProductById;
using ShopCompare.Catalog.Api.Application.Products.GetProducts;
using ShopCompare.Catalog.Api.Application.Products.SeedProducts;
using ShopCompare.Catalog.Api.Infrastructure.Persistence;

namespace ShopCompare.Catalog.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddCatalogApi(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<CatalogDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("catalog-db"));
        });

        services.AddScoped<GetProductsHandler>();
        services.AddScoped<GetProductByIdHandler>();
        services.AddScoped<SeedProductsHandler>();

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}