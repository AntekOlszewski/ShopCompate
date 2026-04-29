namespace ShopCompare.Modules.Cart.Application.Carts.GetCart;

public sealed record CartItemResponse(
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity,
    decimal TotalPrice);