namespace ShopCompare.Modules.Catalog.Application.Products.GetProducts;

public sealed record GetProductsQuery(
    int Page = 1,
    int PageSize = 50,
    string? Search = null);