namespace ShopCompare.Catalog.Api.Application.Products.GetProducts;

public sealed record ProductListItemResponse(
    Guid Id,
    string Name,
    decimal Price,
    string CategoryName);