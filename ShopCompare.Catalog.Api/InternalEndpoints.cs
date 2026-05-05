using Microsoft.EntityFrameworkCore;
using ShopCompare.Catalog.Api.Infrastructure.Persistence;

namespace ShopCompare.Catalog.Api;

public static class InternalCatalogEndpoints
{
    public static IEndpointRouteBuilder MapInternalCatalogEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/internal/catalog")
            .WithTags("Internal Catalog");

        group.MapGet("/products/{id:guid}", async (
            Guid id,
            CatalogDbContext dbContext,
            CancellationToken cancellationToken) =>
        {
            var product = await dbContext.Products
                .AsNoTracking()
                .Where(x => x.Id == id && x.IsActive)
                .Select(x => new CatalogProductDto(
                    x.Id,
                    x.Name,
                    x.Price,
                    x.IsActive))
                .FirstOrDefaultAsync(cancellationToken);

            return product is null
                ? Results.NotFound()
                : Results.Ok(product);
        });

        group.MapGet("/products", async (
            CatalogDbContext dbContext,
            CancellationToken cancellationToken) =>
        {
            var products = await dbContext.Products
                .AsNoTracking()
                .Where(x => x.IsActive)
                .Select(x => new CatalogProductDto(
                    x.Id,
                    x.Name,
                    x.Price,
                    x.IsActive))
                .ToListAsync(cancellationToken);

            return Results.Ok(products);
        });

        group.MapGet("/products/{id:guid}/exists", async (
            Guid id,
            CatalogDbContext dbContext,
            CancellationToken cancellationToken) =>
        {
            var exists = await dbContext.Products
                .AsNoTracking()
                .AnyAsync(x => x.Id == id && x.IsActive, cancellationToken);

            return Results.Ok(new ProductExistsResponse(exists));
        });

        return app;
    }
}

public sealed record CatalogProductDto(
    Guid Id,
    string Name,
    decimal Price,
    bool IsActive);

public sealed record ProductExistsResponse(bool Exists);