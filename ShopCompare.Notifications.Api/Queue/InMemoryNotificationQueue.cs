using System.Threading.Channels;

namespace ShopCompare.Notifications.Api.Queue;

internal sealed class InMemoryNotificationQueue : INotificationQueue
{
    private readonly Channel<OrderConfirmationNotificationMessage> _channel =
        Channel.CreateUnbounded<OrderConfirmationNotificationMessage>();

    public async ValueTask EnqueueAsync(
        OrderConfirmationNotificationMessage message,
        CancellationToken cancellationToken = default)
    {
        await _channel.Writer.WriteAsync(message, cancellationToken);
    }

    public IAsyncEnumerable<OrderConfirmationNotificationMessage> DequeueAsync(
        CancellationToken cancellationToken = default)
    {
        return _channel.Reader.ReadAllAsync(cancellationToken);
    }
}