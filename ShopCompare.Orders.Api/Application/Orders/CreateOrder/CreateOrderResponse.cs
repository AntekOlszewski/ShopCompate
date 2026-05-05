namespace ShopCompare.Orders.Api.Application.Orders.CreateOrder;

public sealed record CreateOrderResponse(
    bool Success,
    Guid? OrderId,
    string? Error);