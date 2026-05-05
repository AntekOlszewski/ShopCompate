using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ShopCompare.Cart.Api.Application.Carts.AddCartItem;
using ShopCompare.Cart.Api.Application.Carts.GetCart;
using ShopCompare.Cart.Api.Application.Carts.RemoveCartItem;
using ShopCompare.Cart.Api.Clients.Catalog;
using ShopCompare.Cart.Api.Infrastructure.Persistence;

namespace ShopCompare.Cart.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddCartApi(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<CartDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("cart-db"));
        });
        
        services.AddHttpClient<ICatalogClient, CatalogClient>(client =>
        {
            client.BaseAddress = new Uri("https+http://catalog-api");
            client.Timeout = TimeSpan.FromSeconds(5);
        });

        services.AddScoped<AddCartItemHandler>();
        services.AddScoped<GetCartHandler>();
        services.AddScoped<RemoveCartItemHandler>();

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}