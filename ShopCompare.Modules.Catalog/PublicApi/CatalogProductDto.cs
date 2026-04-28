namespace ShopCompare.Modules.Catalog.PublicApi;

public sealed record CatalogProductDto(
    Guid Id,
    string Name,
    decimal Price,
    bool IsActive);