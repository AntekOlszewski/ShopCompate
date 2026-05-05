namespace ShopCompare.Orders.Api.Application.Orders.GetUserOrders;

public sealed record UserOrderResponse(
    Guid Id,
    string Status,
    decimal TotalAmount,
    DateTimeOffset CreatedAtUtc);