using Microsoft.EntityFrameworkCore;
using ShopCompare.Inventory.Api.Clients.Catalog;
using ShopCompare.Inventory.Api.Domain;
using ShopCompare.Inventory.Api.Infrastructure.Persistence;

namespace ShopCompare.Inventory.Api.Application.Stock.ReserveStock;

public sealed class ReserveStockHandler(
    InventoryDbContext dbContext,
    ICatalogClient catalogClient)
{
    public async Task<ReserveStockResponse> HandleAsync(
        ReserveStockRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request.Items.Count == 0)
        {
            return new ReserveStockResponse(
                false,
                "Reservation must contain at least one item.");
        }

        foreach (var item in request.Items)
        {
            var productExists = await catalogClient.ProductExistsAsync(
                item.ProductId,
                cancellationToken);

            if (!productExists)
            {
                return new ReserveStockResponse(
                    false,
                    $"Product '{item.ProductId}' does not exist.");
            }
        }

        var productIds = request.Items
            .Select(x => x.ProductId)
            .Distinct()
            .ToList();

        var stockItems = await dbContext.StockItems
            .Where(x => productIds.Contains(x.ProductId))
            .ToListAsync(cancellationToken);

        foreach (var item in request.Items)
        {
            var stockItem = stockItems.FirstOrDefault(x => x.ProductId == item.ProductId);

            if (stockItem is null)
            {
                return new ReserveStockResponse(
                    false,
                    $"Stock item for product '{item.ProductId}' was not found.");
            }

            if (!stockItem.CanReserve(item.Quantity))
            {
                return new ReserveStockResponse(
                    false,
                    $"Not enough stock for product '{item.ProductId}'.");
            }
        }

        foreach (var item in request.Items)
        {
            var stockItem = stockItems.Single(x => x.ProductId == item.ProductId);

            stockItem.Reserve(item.Quantity);

            var reservation = new StockReservation(
                Guid.NewGuid(),
                item.ProductId,
                item.Quantity,
                request.OrderId);

            dbContext.StockReservations.Add(reservation);
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return new ReserveStockResponse(true, null);
    }
}