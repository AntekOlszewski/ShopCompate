using ShopCompare.Catalog.Api.PublicApi;
using ShopCompare.Modules.Inventory.PublicApi;

namespace ShopCompare.Modules.Inventory.Application.Stock.ReserveStock;

public sealed class ReserveStockHandler(IInventoryModule inventoryModule, ICatalogModule catalogModule)
{
    public async Task<ReserveStockResponse> HandleAsync(
        ReserveStockRequest request,
        CancellationToken cancellationToken = default)
    {
        foreach (var item in request.Items)
        {
            var exists = await catalogModule.ProductExistsAsync(
                item.ProductId,
                cancellationToken);

            if (!exists)
            {
                return new ReserveStockResponse(
                    false,
                    $"Product '{item.ProductId}' does not exist.");
            }
        }
        
        var items = request.Items
            .Select(x => new ReserveStockItem(x.ProductId, x.Quantity))
            .ToList();

        var result = await inventoryModule.ReserveStockAsync(
            items,
            request.OrderId,
            cancellationToken);

        return new ReserveStockResponse(result.Success, result.Error);
    }
}