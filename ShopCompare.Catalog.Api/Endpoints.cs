using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ShopCompare.Catalog.Api.Application.Products.GetProductById;
using ShopCompare.Catalog.Api.Application.Products.GetProducts;
using ShopCompare.Catalog.Api.Application.Products.SeedProducts;

namespace ShopCompare.Catalog.Api;

public static class CatalogEndpoints
{
    public static IEndpointRouteBuilder MapCatalogEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/products")
            .WithTags("Catalog");

        group.MapGet("/", async (
            int? page,
            int? pageSize,
            string? search,
            GetProductsHandler handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetProductsQuery(
                page ?? 1,
                pageSize ?? 50,
                search);

            var products = await handler.HandleAsync(query, cancellationToken);

            return Results.Ok(products);
        });

        group.MapGet("/{id:guid}", async (
            Guid id,
            GetProductByIdHandler handler,
            CancellationToken cancellationToken) =>
        {
            var product = await handler.HandleAsync(id, cancellationToken);

            return product is null
                ? Results.NotFound()
                : Results.Ok(product);
        });

        group.MapPost("/seed", async (
            SeedProductsRequest request,
            IValidator<SeedProductsRequest> validator,
            SeedProductsHandler handler,
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

            var createdCount = await handler.HandleAsync(request, cancellationToken);

            return Results.Ok(new
            {
                productsCount = createdCount
            });
        });

        return app;
    }
}