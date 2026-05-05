namespace ShopCompare.Orders.Api.Clients.Cart;

public sealed record CartDto(
    Guid UserId,
    IReadOnlyCollection<CartItemDto> Items,
    decimal TotalAmount);
    
public sealed record CartItemDto(
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity);