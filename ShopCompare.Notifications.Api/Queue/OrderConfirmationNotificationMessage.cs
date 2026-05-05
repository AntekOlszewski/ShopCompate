namespace ShopCompare.Notifications.Api.Queue;

public sealed record OrderConfirmationNotificationMessage(
    Guid UserId,
    Guid OrderId);