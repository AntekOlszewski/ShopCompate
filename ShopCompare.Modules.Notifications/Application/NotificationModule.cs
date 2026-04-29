using ShopCompare.Modules.Notifications.Application.Notifications.SendOrderConfirmation;
using ShopCompare.Modules.Notifications.PublicApi;

namespace ShopCompare.Modules.Notifications.Application;

internal sealed class NotificationModule : INotificationModule
{
    private readonly SendOrderConfirmationHandler _handler;

    public NotificationModule(SendOrderConfirmationHandler handler)
    {
        _handler = handler;
    }

    public async Task<SendNotificationResult> SendOrderConfirmationAsync(
        Guid userId,
        Guid orderId,
        CancellationToken cancellationToken = default)
    {
        var request = new SendOrderConfirmationRequest(userId, orderId);

        var response = await _handler.HandleAsync(request, cancellationToken);

        return new SendNotificationResult(
            response.Success,
            response.NotificationId,
            response.Error);
    }
}