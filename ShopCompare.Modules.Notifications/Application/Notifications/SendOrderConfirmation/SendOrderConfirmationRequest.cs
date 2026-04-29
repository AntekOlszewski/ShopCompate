namespace ShopCompare.Modules.Notifications.Application.Notifications.SendOrderConfirmation;

public sealed record SendOrderConfirmationRequest(
    Guid UserId,
    Guid OrderId);