using ShopCompare.Modules.Notifications.Domain;
using ShopCompare.Modules.Notifications.Infrastructure.Persistence;

namespace ShopCompare.Modules.Notifications.Application.Notifications.SendOrderConfirmation;

public sealed class SendOrderConfirmationHandler(NotificationsDbContext dbContext)
{
    private readonly Random _random = new();

    public async Task<SendOrderConfirmationResponse> HandleAsync(
        SendOrderConfirmationRequest request,
        CancellationToken cancellationToken = default)
    {
        var payload = $"Order confirmation for order {request.OrderId}.";

        var notification = new Notification(
            Guid.NewGuid(),
            request.UserId,
            NotificationType.OrderConfirmation,
            payload);

        dbContext.Notifications.Add(notification);

        var delay = _random.Next(100, 301);

        await Task.Delay(delay, cancellationToken);

        notification.MarkAsSent();

        await dbContext.SaveChangesAsync(cancellationToken);

        return new SendOrderConfirmationResponse(
            true,
            notification.Id,
            null);
    }
}