namespace ShopCompare.Modules.Payments.Application.Payments.ProcessPayment;

public sealed record ProcessPaymentRequest(
    Guid OrderId,
    decimal Amount);