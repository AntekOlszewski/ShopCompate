using Microsoft.EntityFrameworkCore;
using ShopCompare.Orders.Api.Infrastructure.Persistence;

namespace ShopCompare.Orders.Api.Application.Orders.GetOrder;

public sealed class GetOrderHandler(OrdersDbContext dbContext)
{
    public async Task<OrderDetailsResponse?> HandleAsync(
        Guid orderId,
        CancellationToken cancellationToken = default)
    {
        var order = await dbContext.Orders
            .AsNoTracking()
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == orderId, cancellationToken);

        if (order is null)
            return null;

        var items = order.Items
            .Select(x => new OrderItemResponse(
                x.ProductId,
                x.ProductName,
                x.UnitPrice,
                x.Quantity,
                x.TotalPrice))
            .ToList();

        return new OrderDetailsResponse(
            order.Id,
            order.UserId,
            order.Status.ToString(),
            order.TotalAmount,
            order.PaymentId,
            order.CreatedAtUtc,
            order.PaidAtUtc,
            order.FailureReason,
            items);
    }
}