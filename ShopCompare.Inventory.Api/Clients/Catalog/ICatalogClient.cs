namespace ShopCompare.Inventory.Api.Clients.Catalog;

public interface ICatalogClient
{
    Task<IReadOnlyCollection<CatalogProductDto>> GetProductsAsync(
        CancellationToken cancellationToken = default);

    Task<bool> ProductExistsAsync(
        Guid productId,
        CancellationToken cancellationToken = default);
}