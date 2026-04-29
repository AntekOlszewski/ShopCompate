namespace ShopCompare.Modules.Orders.Application.Orders.GetOrder;

public sealed record OrderItemResponse(
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity,
    decimal TotalPrice);