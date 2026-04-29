using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ShopCompare.Modules.Inventory.Application.Stock.GetStock;
using ShopCompare.Modules.Inventory.Application.Stock.ReserveStock;
using ShopCompare.Modules.Inventory.Application.Stock.SeedStock;

namespace ShopCompare.Modules.Inventory;

public static class InventoryEndpoints
{
    public static IEndpointRouteBuilder MapInventoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/inventory")
            .WithTags("Inventory");

        group.MapGet("/{productId:guid}", async (
            Guid productId,
            GetStockHandler handler,
            CancellationToken cancellationToken) =>
        {
            var stock = await handler.HandleAsync(productId, cancellationToken);

            return stock is null
                ? Results.NotFound()
                : Results.Ok(stock);
        });

        group.MapPost("/reservations", async (
            ReserveStockRequest request,
            IValidator<ReserveStockRequest> validator,
            ReserveStockHandler handler,
            CancellationToken cancellationToken) =>
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(
                    validationResult.Errors
                        .GroupBy(x => x.PropertyName)
                        .ToDictionary(
                            x => x.Key,
                            x => x.Select(e => e.ErrorMessage).ToArray()));
            }

            var result = await handler.HandleAsync(request, cancellationToken);

            return result.Success
                ? Results.Ok(result)
                : Results.BadRequest(result);
        });
        
        group.MapPost("/seed", async (
            SeedStockHandler handler,
            CancellationToken cancellationToken) =>
        {
            var created = await handler.HandleAsync(cancellationToken);

            return Results.Ok(new
            {
                created
            });
        });

        return app;
    }
}