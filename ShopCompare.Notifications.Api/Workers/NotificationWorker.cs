using ShopCompare.Notifications.Api.Domain;
using ShopCompare.Notifications.Api.Infrastructure.Persistence;
using ShopCompare.Notifications.Api.Queue;

namespace ShopCompare.Notifications.Api.Workers;

public sealed class NotificationWorker(
    INotificationQueue queue,
    IServiceScopeFactory scopeFactory,
    ILogger<NotificationWorker> logger)
    : BackgroundService
{
    private readonly Random _random = new();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var message in queue.DequeueAsync(stoppingToken))
        {
            try
            {
                await ProcessAsync(message, stoppingToken);
            }
            catch (Exception exception)
            {
                logger.LogError(
                    exception,
                    "Failed to process notification for order {OrderId}",
                    message.OrderId);
            }
        }
    }

    private async Task ProcessAsync(
        OrderConfirmationNotificationMessage message,
        CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<NotificationsDbContext>();

        var payload = $"Order confirmation for order {message.OrderId}.";

        var notification = new Notification(
            Guid.NewGuid(),
            message.UserId,
            NotificationType.OrderConfirmation,
            payload);

        dbContext.Notifications.Add(notification);

        await Task.Delay(_random.Next(100, 301), cancellationToken);

        notification.MarkAsSent();

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}