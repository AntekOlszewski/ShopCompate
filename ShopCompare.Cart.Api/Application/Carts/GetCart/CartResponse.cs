namespace ShopCompare.Cart.Api.Application.Carts.GetCart;

public sealed record CartResponse(
    Guid UserId,
    IReadOnlyCollection<CartItemResponse> Items,
    decimal TotalAmount);