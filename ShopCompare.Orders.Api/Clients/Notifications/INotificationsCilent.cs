namespace ShopCompare.Orders.Api.Clients.Notifications;

public interface INotificationsClient
{
    Task QueueOrderConfirmationAsync(
        QueueOrderConfirmationRequest request,
        CancellationToken cancellationToken = default);
}