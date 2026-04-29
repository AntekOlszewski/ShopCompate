using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopCompare.Modules.Orders.Application.Orders.CreateOrder;
using ShopCompare.Modules.Orders.Application.Orders.GetOrder;
using ShopCompare.Modules.Orders.Application.Orders.GetUserOrders;
using ShopCompare.Modules.Orders.Infrastructure.Persistence;

namespace ShopCompare.Modules.Orders;

public static class DependencyInjection
{
    public static IServiceCollection AddOrdersModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<OrdersDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("shopcompare-db"));
        });

        services.AddScoped<CreateOrderHandler>();
        services.AddScoped<GetOrderHandler>();
        services.AddScoped<GetUserOrdersHandler>();

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}