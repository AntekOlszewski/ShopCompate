namespace ShopCompare.Catalog.Api.Application.Products.GetProductById;

public sealed record ProductDetailsResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string CategoryName,
    bool IsActive);