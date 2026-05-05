namespace ShopCompare.Orders.Api.Clients.Inventory;

public interface IInventoryClient
{
    Task<ReserveStockResponse> ReserveStockAsync(
        ReserveStockRequest request,
        CancellationToken cancellationToken = default);
}