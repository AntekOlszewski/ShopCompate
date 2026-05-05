namespace ShopCompare.Cart.Api.Clients.Catalog;

public sealed record CatalogProductDto(
    Guid Id,
    string Name,
    decimal Price,
    bool IsActive);