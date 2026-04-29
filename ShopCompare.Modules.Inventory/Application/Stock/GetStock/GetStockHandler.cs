using Microsoft.EntityFrameworkCore;
using ShopCompare.Modules.Inventory.Infrastructure.Persistence;

namespace ShopCompare.Modules.Inventory.Application.Stock.GetStock;

public sealed class GetStockHandler(InventoryDbContext dbContext)
{
    public async Task<StockResponse?> HandleAsync(
        Guid productId,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.StockItems
            .AsNoTracking()
            .Where(x => x.ProductId == productId)
            .Select(x => new StockResponse(
                x.ProductId,
                x.AvailableQuantity,
                x.ReservedQuantity,
                x.AvailableQuantity - x.ReservedQuantity))
            .FirstOrDefaultAsync(cancellationToken);
    }
}