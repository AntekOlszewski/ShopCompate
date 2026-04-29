namespace ShopCompare.Modules.Notifications.PublicApi;

public sealed record SendNotificationResult(
    bool Success,
    Guid? NotificationId,
    string? Error);