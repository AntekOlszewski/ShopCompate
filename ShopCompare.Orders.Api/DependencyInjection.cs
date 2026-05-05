using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopCompare.Orders.Api.Application.Orders.CreateOrder;
using ShopCompare.Orders.Api.Application.Orders.GetOrder;
using ShopCompare.Orders.Api.Application.Orders.GetUserOrders;
using ShopCompare.Orders.Api.Clients.Cart;
using ShopCompare.Orders.Api.Clients.Inventory;
using ShopCompare.Orders.Api.Clients.Notifications;
using ShopCompare.Orders.Api.Clients.Payments;
using ShopCompare.Orders.Api.Infrastructure.Persistence;

namespace ShopCompare.Orders.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddOrdersApi(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<OrdersDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("orders-db"));
        });
        
        services.AddHttpClient<ICartClient, CartClient>(client =>
        {
            client.BaseAddress = new Uri("https+http://cart-api");
            client.Timeout = TimeSpan.FromSeconds(5);
        });

        services.AddHttpClient<IInventoryClient, InventoryClient>(client =>
        {
            client.BaseAddress = new Uri("https+http://inventory-api");
            client.Timeout = TimeSpan.FromSeconds(5);
        });

        services.AddHttpClient<IPaymentsClient, PaymentsClient>(client =>
        {
            client.BaseAddress = new Uri("https+http://payments-api");
            client.Timeout = TimeSpan.FromSeconds(10);
        });

        services.AddHttpClient<INotificationsClient, NotificationsClient>(client =>
        {
            client.BaseAddress = new Uri("https+http://notifications-api");
            client.Timeout = TimeSpan.FromSeconds(2);
        });

        services.AddScoped<CreateOrderHandler>();
        services.AddScoped<GetOrderHandler>();
        services.AddScoped<GetUserOrdersHandler>();

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}