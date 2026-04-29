namespace ShopCompare.Modules.Payments.PublicApi;

public sealed record ProcessPaymentResult(
    bool Success,
    Guid? PaymentId,
    string? Error);