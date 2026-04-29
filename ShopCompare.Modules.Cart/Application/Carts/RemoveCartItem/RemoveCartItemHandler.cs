using Microsoft.EntityFrameworkCore;
using ShopCompare.Modules.Cart.Infrastructure.Persistence;

namespace ShopCompare.Modules.Cart.Application.Carts.RemoveCartItem;

public sealed class RemoveCartItemHandler(CartDbContext dbContext)
{
    public async Task<RemoveCartItemResponse> HandleAsync(
        Guid userId,
        Guid productId,
        CancellationToken cancellationToken = default)
    {
        if (userId == Guid.Empty)
        {
            return new RemoveCartItemResponse(
                false,
                "User id cannot be empty.");
        }

        if (productId == Guid.Empty)
        {
            return new RemoveCartItemResponse(
                false,
                "Product id cannot be empty.");
        }

        var cart = await dbContext.Carts
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        if (cart is null)
        {
            return new RemoveCartItemResponse(
                false,
                "Cart was not found.");
        }

        var removed = cart.RemoveItem(productId);

        if (!removed)
        {
            return new RemoveCartItemResponse(
                false,
                "Product was not found in cart.");
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return new RemoveCartItemResponse(true, null);
    }
}