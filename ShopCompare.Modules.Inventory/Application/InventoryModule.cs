using Microsoft.EntityFrameworkCore;
using ShopCompare.Modules.Inventory.Domain;
using ShopCompare.Modules.Inventory.Infrastructure.Persistence;
using ShopCompare.Modules.Inventory.PublicApi;

namespace ShopCompare.Modules.Inventory.Application;

internal sealed class InventoryModule(InventoryDbContext dbContext) : IInventoryModule
{
    public async Task<bool> CheckAvailabilityAsync(
        Guid productId,
        int quantity,
        CancellationToken cancellationToken = default)
    {
        if (productId == Guid.Empty || quantity <= 0)
            return false;

        var stockItem = await dbContext.StockItems
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ProductId == productId, cancellationToken);

        return stockItem is not null && stockItem.CanReserve(quantity);
    }

    public async Task<ReserveStockResult> ReserveStockAsync(
        IReadOnlyCollection<ReserveStockItem> items,
        Guid? orderId = null,
        CancellationToken cancellationToken = default)
    {
        if (items.Count == 0)
            return new ReserveStockResult(false, "Reservation must contain at least one item.");

        var productIds = items.Select(x => x.ProductId).Distinct().ToList();

        var stockItems = await dbContext.StockItems
            .Where(x => productIds.Contains(x.ProductId))
            .ToListAsync(cancellationToken);

        foreach (var item in items)
        {
            var stockItem = stockItems.FirstOrDefault(x => x.ProductId == item.ProductId);

            if (stockItem is null)
            {
                return new ReserveStockResult(
                    false,
                    $"Stock item for product '{item.ProductId}' was not found.");
            }

            if (!stockItem.CanReserve(item.Quantity))
            {
                return new ReserveStockResult(
                    false,
                    $"Not enough stock for product '{item.ProductId}'.");
            }
        }

        foreach (var item in items)
        {
            var stockItem = stockItems.Single(x => x.ProductId == item.ProductId);

            stockItem.Reserve(item.Quantity);

            var reservation = new StockReservation(
                Guid.NewGuid(),
                item.ProductId,
                item.Quantity,
                orderId);

            dbContext.StockReservations.Add(reservation);
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return new ReserveStockResult(true, null);
    }
}