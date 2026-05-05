namespace ShopCompare.Orders.Api.Clients.Payments;

public sealed record ProcessPaymentRequest(
    Guid OrderId,
    decimal Amount);