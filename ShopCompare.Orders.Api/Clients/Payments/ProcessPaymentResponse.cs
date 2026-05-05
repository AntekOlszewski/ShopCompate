namespace ShopCompare.Orders.Api.Clients.Payments;

public sealed record ProcessPaymentResponse(
    bool Success,
    Guid? PaymentId,
    string? Error);