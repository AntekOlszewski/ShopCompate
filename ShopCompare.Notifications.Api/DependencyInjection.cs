using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ShopCompare.Notifications.Api.Infrastructure.Persistence;
using ShopCompare.Notifications.Api.Queue;
using ShopCompare.Notifications.Api.Workers;

namespace ShopCompare.Notifications.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddNotificationsApi(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<NotificationsDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("notifications-db"));
        });

        services.AddSingleton<INotificationQueue, InMemoryNotificationQueue>();
        services.AddHostedService<NotificationWorker>();

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}