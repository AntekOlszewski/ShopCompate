using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ShopCompare.Inventory.Api.Application.Stock.GetStock;
using ShopCompare.Inventory.Api.Application.Stock.ReserveStock;

namespace ShopCompare.Inventory.Api;

public static class InternalInventoryEndpoints
{
    public static IEndpointRouteBuilder MapInternalInventoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/internal/inventory")
            .WithTags("Internal Inventory");

        group.MapGet("/{productId:guid}", async (
            Guid productId,
            GetStockHandler handler,
            CancellationToken cancellationToken) =>
        {
            var result = await handler.HandleAsync(productId, cancellationToken);

            return result is null
                ? Results.NotFound()
                : Results.Ok(result);
        });

        group.MapPost("/reservations", async (
            ReserveStockRequest request,
            ReserveStockHandler handler,
            CancellationToken cancellationToken) =>
        {
            var result = await handler.HandleAsync(request, cancellationToken);

            return result.Success ? Results.Ok(result) : Results.BadRequest(result);
        });

        return app;
    }
}