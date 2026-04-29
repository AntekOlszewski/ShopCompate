namespace ShopCompare.Modules.Cart.Application.Carts.AddCartItem;

public sealed record AddCartItemRequest(
    Guid ProductId,
    int Quantity);