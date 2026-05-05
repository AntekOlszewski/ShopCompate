namespace ShopCompare.Orders.Api.Clients.Notifications;

public sealed record QueueOrderConfirmationRequest(
    Guid UserId,
    Guid OrderId);