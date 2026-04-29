namespace ShopCompare.Modules.Notifications.PublicApi;

public interface INotificationModule
{
    Task<SendNotificationResult> SendOrderConfirmationAsync(
        Guid userId,
        Guid orderId,
        CancellationToken cancellationToken = default);
}