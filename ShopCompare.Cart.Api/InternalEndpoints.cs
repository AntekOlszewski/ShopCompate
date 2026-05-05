using Microsoft.EntityFrameworkCore;
using ShopCompare.Cart.Api.Infrastructure.Persistence;

namespace ShopCompare.Cart.Api;

public static class InternalCartEndpoints
{
    public static IEndpointRouteBuilder MapInternalCartEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/internal/cart")
            .WithTags("Internal Cart");

        group.MapGet("/{userId:guid}", async (
            Guid userId,
            CartDbContext dbContext,
            CancellationToken cancellationToken) =>
        {
            var cart = await dbContext.Carts
                .AsNoTracking()
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

            if (cart is null)
            {
                return Results.NotFound();
            }

            var items = cart.Items
                .Select(x => new InternalCartItemDto(
                    x.ProductId,
                    x.ProductName,
                    x.UnitPrice,
                    x.Quantity))
                .ToList();

            return Results.Ok(new InternalCartDto(
                cart.UserId,
                items,
                cart.TotalAmount));
        });

        group.MapDelete("/{userId:guid}", async (
            Guid userId,
            CartDbContext dbContext,
            CancellationToken cancellationToken) =>
        {
            var cart = await dbContext.Carts
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

            if (cart is null)
            {
                return Results.NoContent();
            }

            cart.Clear();

            await dbContext.SaveChangesAsync(cancellationToken);

            return Results.NoContent();
        });

        return app;
    }
}

public sealed record InternalCartDto(
    Guid UserId,
    IReadOnlyCollection<InternalCartItemDto> Items,
    decimal TotalAmount);

public sealed record InternalCartItemDto(
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity);