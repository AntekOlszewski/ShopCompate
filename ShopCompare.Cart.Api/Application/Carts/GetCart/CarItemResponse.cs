namespace ShopCompare.Cart.Api.Application.Carts.GetCart;

public sealed record CartItemResponse(
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity,
    decimal TotalPrice);