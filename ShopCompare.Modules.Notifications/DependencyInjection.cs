using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopCompare.Modules.Notifications.Application;
using ShopCompare.Modules.Notifications.Application.Notifications.SendOrderConfirmation;
using ShopCompare.Modules.Notifications.Infrastructure.Persistence;
using ShopCompare.Modules.Notifications.PublicApi;

namespace ShopCompare.Modules.Notifications;

public static class DependencyInjection
{
    public static IServiceCollection AddNotificationsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<NotificationsDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("shopcompare-db"));
        });

        services.AddScoped<INotificationModule, NotificationModule>();

        services.AddScoped<SendOrderConfirmationHandler>();

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}