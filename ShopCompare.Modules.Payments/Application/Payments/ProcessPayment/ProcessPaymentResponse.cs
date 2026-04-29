namespace ShopCompare.Modules.Payments.Application.Payments.ProcessPayment;

public sealed record ProcessPaymentResponse(
    bool Success,
    Guid? PaymentId,
    string? Error);