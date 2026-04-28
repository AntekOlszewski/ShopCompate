namespace ShopCompare.Modules.Catalog.Application.Products.SeedProducts;

public sealed record SeedProductsRequest(
    int ProductsCount = 1000,
    int CategoriesCount = 10);