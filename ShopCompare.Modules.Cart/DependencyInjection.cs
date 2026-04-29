using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopCompare.Modules.Cart.Application;
using ShopCompare.Modules.Cart.Application.Carts.AddCartItem;
using ShopCompare.Modules.Cart.Application.Carts.GetCart;
using ShopCompare.Modules.Cart.Application.Carts.RemoveCartItem;
using ShopCompare.Modules.Cart.Infrastructure.Persistence;
using ShopCompare.Modules.Cart.PublicApi;

namespace ShopCompare.Modules.Cart;

public static class DependencyInjection
{
    public static IServiceCollection AddCartModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<CartDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("shopcompare-db"));
        });

        services.AddScoped<ICartModule, CartModule>();

        services.AddScoped<AddCartItemHandler>();
        services.AddScoped<GetCartHandler>();
        services.AddScoped<RemoveCartItemHandler>();

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}