namespace ShopCompare.Modules.Catalog.PublicApi;

public interface ICatalogModule
{
    Task<CatalogProductDto?> GetProductAsync(
        Guid productId,
        CancellationToken cancellationToken = default);

    Task<bool> ProductExistsAsync(
        Guid productId,
        CancellationToken cancellationToken = default);
}