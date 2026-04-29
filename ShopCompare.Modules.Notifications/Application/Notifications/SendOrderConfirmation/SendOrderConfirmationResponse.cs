namespace ShopCompare.Modules.Notifications.Application.Notifications.SendOrderConfirmation;

public sealed record SendOrderConfirmationResponse(
    bool Success,
    Guid? NotificationId,
    string? Error);