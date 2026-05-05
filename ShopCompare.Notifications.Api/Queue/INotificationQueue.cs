namespace ShopCompare.Notifications.Api.Queue;

public interface INotificationQueue
{
    ValueTask EnqueueAsync(
        OrderConfirmationNotificationMessage message,
        CancellationToken cancellationToken = default);

    IAsyncEnumerable<OrderConfirmationNotificationMessage> DequeueAsync(
        CancellationToken cancellationToken = default);
}