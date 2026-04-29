using Microsoft.EntityFrameworkCore;
using ShopCompare.Modules.Orders.Infrastructure.Persistence;

namespace ShopCompare.Modules.Orders.Application.Orders.GetUserOrders;

public sealed class GetUserOrdersHandler(OrdersDbContext dbContext)
{
    public async Task<IReadOnlyCollection<UserOrderResponse>> HandleAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Orders
            .AsNoTracking()
            .Include(x => x.Items)
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAtUtc)
            .Select(x => new UserOrderResponse(
                x.Id,
                x.Status.ToString(),
                x.Items.Sum(i => i.UnitPrice * i.Quantity),
                x.CreatedAtUtc))
            .ToListAsync(cancellationToken);
    }
}