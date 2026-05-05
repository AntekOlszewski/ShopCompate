using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ShopCompare.Cart.Api.Application.Carts.AddCartItem;
using ShopCompare.Cart.Api.Application.Carts.GetCart;
using ShopCompare.Cart.Api.Application.Carts.RemoveCartItem;

namespace ShopCompare.Cart.Api;

public static class CartEndpoints
{
    public static IEndpointRouteBuilder MapCartEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/carts")
            .WithTags("Cart");

        group.MapGet("/{userId:guid}", async (
            Guid userId,
            GetCartHandler handler,
            CancellationToken cancellationToken) =>
        {
            var cart = await handler.HandleAsync(userId, cancellationToken);

            return cart is null
                ? Results.NotFound()
                : Results.Ok(cart);
        });

        group.MapPost("/{userId:guid}/items", async (
            Guid userId,
            AddCartItemRequest request,
            IValidator<AddCartItemRequest> validator,
            AddCartItemHandler handler,
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

            var result = await handler.HandleAsync(userId, request, cancellationToken);

            return result.Success
                ? Results.NoContent()
                : Results.BadRequest(result);
        });

        group.MapDelete("/{userId:guid}/items/{productId:guid}", async (
            Guid userId,
            Guid productId,
            RemoveCartItemHandler handler,
            CancellationToken cancellationToken) =>
        {
            var result = await handler.HandleAsync(userId, productId, cancellationToken);

            return result.Success
                ? Results.NoContent()
                : Results.BadRequest(result);
        });

        return app;
    }
}