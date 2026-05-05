namespace ShopCompare.Orders.Api.Clients.Notifications;

internal sealed class NotificationsClient(
    HttpClient httpClient,
    ILogger<NotificationsClient> logger)
    : INotificationsClient
{
    public async Task QueueOrderConfirmationAsync(
        QueueOrderConfirmationRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync(
            "/internal/notifications/order-confirmation",
            request,
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogWarning(
                "Failed to queue notification for order {OrderId}. Status code: {StatusCode}",
                request.OrderId,
                response.StatusCode);
        }
    }
}