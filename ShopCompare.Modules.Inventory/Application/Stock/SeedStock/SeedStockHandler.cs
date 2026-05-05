using Microsoft.EntityFrameworkCore;
using ShopCompare.Catalog.Api.PublicApi;
using ShopCompare.Modules.Inventory.Domain;
using ShopCompare.Modules.Inventory.Infrastructure.Persistence;

namespace ShopCompare.Modules.Inventory.Application.Stock.SeedStock;

public sealed class SeedStockHandler(
    InventoryDbContext dbContext,
    ICatalogModule catalogModule)
{
    public async Task<int> HandleAsync(CancellationToken cancellationToken = default)
    {
        var products = await catalogModule.GetProductsAsync(cancellationToken);

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
                availableQuantity: 100); // stały seed

            dbContext.StockItems.Add(stockItem);
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return toCreate.Count;
    }
}