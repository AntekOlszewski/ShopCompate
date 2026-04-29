using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ShopCompare.Modules.Orders.Application.Orders.CreateOrder;
using ShopCompare.Modules.Orders.Application.Orders.GetOrder;
using ShopCompare.Modules.Orders.Application.Orders.GetUserOrders;

namespace ShopCompare.Modules.Orders;

public static class OrderEndpoints
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/orders")
            .WithTags("Orders");

        group.MapPost("/", async (
            CreateOrderRequest request,
            IValidator<CreateOrderRequest> validator,
            CreateOrderHandler handler,
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
                ? Results.Created($"/api/orders/{result.OrderId}", result)
                : Results.BadRequest(result);
        });

        group.MapGet("/{orderId:guid}", async (
            Guid orderId,
            GetOrderHandler handler,
            CancellationToken cancellationToken) =>
        {
            var order = await handler.HandleAsync(orderId, cancellationToken);

            return order is null
                ? Results.NotFound()
                : Results.Ok(order);
        });

        group.MapGet("/users/{userId:guid}", async (
            Guid userId,
            GetUserOrdersHandler handler,
            CancellationToken cancellationToken) =>
        {
            var orders = await handler.HandleAsync(userId, cancellationToken);

            return Results.Ok(orders);
        });

        return app;
    }
}