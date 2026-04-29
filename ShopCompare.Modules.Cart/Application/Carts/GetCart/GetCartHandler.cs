using Microsoft.EntityFrameworkCore;
using ShopCompare.Modules.Cart.Infrastructure.Persistence;

namespace ShopCompare.Modules.Cart.Application.Carts.GetCart;

public sealed class GetCartHandler(CartDbContext dbContext)
{
    public async Task<CartResponse?> HandleAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var cart = await dbContext.Carts
            .AsNoTracking()
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        if (cart is null)
            return null;

        var items = cart.Items
            .Select(x => new CartItemResponse(
                x.ProductId,
                x.ProductName,
                x.UnitPrice,
                x.Quantity,
                x.TotalPrice))
            .ToList();

        return new CartResponse(
            cart.UserId,
            items,
            cart.TotalAmount);
    }
}