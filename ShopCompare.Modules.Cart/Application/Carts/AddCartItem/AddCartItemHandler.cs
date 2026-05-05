using Microsoft.EntityFrameworkCore;
using ShopCompare.Modules.Cart.Infrastructure.Persistence;
using ShopCompare.Catalog.Api.PublicApi;

namespace ShopCompare.Modules.Cart.Application.Carts.AddCartItem;

public sealed class AddCartItemHandler(
    CartDbContext dbContext,
    ICatalogModule catalogModule)
{
    public async Task<AddCartItemResponse> HandleAsync(
        Guid userId,
        AddCartItemRequest request,
        CancellationToken cancellationToken = default)
    {
        if (userId == Guid.Empty)
        {
            return new AddCartItemResponse(
                false,
                "User id cannot be empty.");
        }

        var product = await catalogModule.GetProductAsync(
            request.ProductId,
            cancellationToken);

        if (product is null)
        {
            return new AddCartItemResponse(
                false,
                $"Product '{request.ProductId}' was not found.");
        }

        if (!product.IsActive)
        {
            return new AddCartItemResponse(
                false,
                $"Product '{request.ProductId}' is inactive.");
        }

        var cart = await dbContext.Carts
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        if (cart is null)
        {
            cart = new Domain.Cart(Guid.NewGuid(), userId);
            dbContext.Carts.Add(cart);
        }

        cart.AddItem(
            product.Id,
            product.Name,
            product.Price,
            request.Quantity);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new AddCartItemResponse(true, null);
    }
}