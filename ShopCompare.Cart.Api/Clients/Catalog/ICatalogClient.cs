namespace ShopCompare.Cart.Api.Clients.Catalog;

public interface ICatalogClient
{
    Task<CatalogProductDto?> GetProductAsync(
        Guid productId,
        CancellationToken cancellationToken = default);
}