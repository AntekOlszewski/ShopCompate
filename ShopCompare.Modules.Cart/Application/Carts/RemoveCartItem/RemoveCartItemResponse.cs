namespace ShopCompare.Modules.Cart.Application.Carts.RemoveCartItem;

public sealed record RemoveCartItemResponse(
    bool Success,
    string? Error);