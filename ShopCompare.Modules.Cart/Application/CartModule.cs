using Microsoft.EntityFrameworkCore;
using ShopCompare.Modules.Cart.Infrastructure.Persistence;
using ShopCompare.Modules.Cart.PublicApi;

namespace ShopCompare.Modules.Cart.Application;

internal sealed class CartModule(CartDbContext dbContext) : ICartModule
{
    public async Task<CartDto?> GetCartAsync(
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
            .Select(x => new CartItemDto(
                x.ProductId,
                x.ProductName,
                x.UnitPrice,
                x.Quantity))
            .ToList();

        return new CartDto(
            cart.UserId,
            items,
            cart.TotalAmount);
    }

    public async Task ClearCartAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var cart = await dbContext.Carts
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        if (cart is null)
            return;

        cart.Clear();

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}