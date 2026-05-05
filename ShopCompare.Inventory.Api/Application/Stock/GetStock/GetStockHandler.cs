using Microsoft.EntityFrameworkCore;
using ShopCompare.Inventory.Api.Infrastructure.Persistence;

namespace ShopCompare.Inventory.Api.Application.Stock.GetStock;

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