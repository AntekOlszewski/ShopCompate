using Microsoft.EntityFrameworkCore;
using ShopCompare.Inventory.Api.Clients.Catalog;
using ShopCompare.Inventory.Api.Domain;
using ShopCompare.Inventory.Api.Infrastructure.Persistence;

namespace ShopCompare.Inventory.Api.Application.Stock.SeedStock;

public sealed class SeedStockHandler(
    InventoryDbContext dbContext,
    ICatalogClient catalogClient)
{
    public async Task<int> HandleAsync(CancellationToken cancellationToken = default)
    {
        var products = await catalogClient.GetProductsAsync(cancellationToken);

        if (products.Count == 0)
            return 0;

        var existingProductIds = await dbContext.StockItems
            .Select(x => x.ProductId)
            .ToListAsync(cancellationToken);

        var toCreate = products
            .Where(p => !existingProductIds.Contains(p.Id))
            .ToList();

        foreach (var product in toCreate)
        {
            var stockItem = new StockItem(
                product.Id,
                availableQuantity: 100);

            dbContext.StockItems.Add(stockItem);
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return toCreate.Count;
    }
}