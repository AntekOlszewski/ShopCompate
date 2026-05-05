namespace ShopCompare.Orders.Api.Clients.Inventory;

internal sealed class InventoryClient(HttpClient httpClient) : IInventoryClient
{
    public async Task<ReserveStockResponse> ReserveStockAsync(
        ReserveStockRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync(
            "/internal/inventory/reservations",
            request,
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<ReserveStockResponse>(
                cancellationToken);

            return error ?? new ReserveStockResponse(false, "Inventory reservation failed.");
        }

        var result = await response.Content.ReadFromJsonAsync<ReserveStockResponse>(
            cancellationToken);

        return result ?? new ReserveStockResponse(false, "Inventory returned empty response.");
    }
}