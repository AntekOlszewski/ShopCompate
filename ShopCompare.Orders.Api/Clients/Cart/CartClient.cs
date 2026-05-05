using System.Net;

namespace ShopCompare.Orders.Api.Clients.Cart;

internal sealed class CartClient(HttpClient httpClient) : ICartClient
{
    public async Task<CartDto?> GetCartAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync(
            $"/internal/cart/{userId}",
            cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<CartDto>(
            cancellationToken);
    }

    public async Task ClearCartAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync(
            $"/internal/cart/{userId}",
            cancellationToken);

        response.EnsureSuccessStatusCode();
    }
}