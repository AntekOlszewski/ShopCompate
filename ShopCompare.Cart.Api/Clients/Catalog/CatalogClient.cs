using System.Net;

namespace ShopCompare.Cart.Api.Clients.Catalog;

internal sealed class CatalogClient(HttpClient httpClient) : ICatalogClient
{
    public async Task<CatalogProductDto?> GetProductAsync(
        Guid productId,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync(
            $"/internal/catalog/products/{productId}",
            cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<CatalogProductDto>(
            cancellationToken);
    }
}