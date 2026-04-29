namespace ShopCompare.Modules.Inventory.PublicApi;

public interface IInventoryModule
{
    Task<bool> CheckAvailabilityAsync(
        Guid productId,
        int quantity,
        CancellationToken cancellationToken = default);

    Task<ReserveStockResult> ReserveStockAsync(
        IReadOnlyCollection<ReserveStockItem> items,
        Guid? orderId = null,
        CancellationToken cancellationToken = default);
}