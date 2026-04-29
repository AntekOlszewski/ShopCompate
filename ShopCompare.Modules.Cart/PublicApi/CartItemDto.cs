namespace ShopCompare.Modules.Cart.PublicApi;

public sealed record CartItemDto(
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity);