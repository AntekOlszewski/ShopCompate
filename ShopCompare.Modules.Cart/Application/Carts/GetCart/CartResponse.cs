namespace ShopCompare.Modules.Cart.Application.Carts.GetCart;

public sealed record CartResponse(
    Guid UserId,
    IReadOnlyCollection<CartItemResponse> Items,
    decimal TotalAmount);