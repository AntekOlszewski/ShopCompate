namespace ShopCompare.Inventory.Api.Clients.Catalog;

internal sealed class CatalogClient(HttpClient httpClient) : ICatalogClient
{
    public async Task<IReadOnlyCollection<CatalogProductDto>> GetProductsAsync(
        CancellationToken cancellationToken = default)
    {
        var products = await httpClient.GetFromJsonAsync<List<CatalogProductDto>>(
            "/internal/catalog/products",
            cancellationToken);

        return products ?? [];
    }

    public async Task<bool> ProductExistsAsync(
        Guid productId,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync(
            $"/internal/catalog/products/{productId}/exists",
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        var result = await response.Content.ReadFromJsonAsync<ProductExistsResponse>(
            cancellationToken);

        return result?.Exists == true;
    }
}