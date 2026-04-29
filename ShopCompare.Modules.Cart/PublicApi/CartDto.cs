namespace ShopCompare.Modules.Cart.PublicApi;

public sealed record CartDto(
    Guid UserId,
    IReadOnlyCollection<CartItemDto> Items,
    decimal TotalAmount);