namespace ShopCompare.Cart.Api.Application.Carts.AddCartItem;

public sealed record AddCartItemRequest(
    Guid ProductId,
    int Quantity);