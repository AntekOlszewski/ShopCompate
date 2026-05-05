namespace ShopCompare.Notifications.Api.Application.Notifications.QueueOrderConfirmation;

public sealed record QueueOrderConfirmationRequest(
    Guid UserId,
    Guid OrderId);