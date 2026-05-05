namespace ShopCompare.Cart.Api.Application.Carts.RemoveCartItem;

public sealed record RemoveCartItemResponse(
    bool Success,
    string? Error);