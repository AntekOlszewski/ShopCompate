namespace ShopCompare.Payments.Api.Application.Payments.ProcessPayment;

public sealed record ProcessPaymentRequest(
    Guid OrderId,
    decimal Amount);