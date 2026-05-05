using FluentValidation;
using ShopCompare.Notifications.Api.Application.Notifications.QueueOrderConfirmation;
using ShopCompare.Notifications.Api.Queue;

namespace ShopCompare.Notifications.Api;

public static class InternalNotificationEndpoints
{
    public static IEndpointRouteBuilder MapInternalNotificationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/internal/notifications")
            .WithTags("Internal Notifications");

        group.MapPost("/order-confirmation", async (
            QueueOrderConfirmationRequest request,
            IValidator<QueueOrderConfirmationRequest> validator,
            INotificationQueue queue,
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

            await queue.EnqueueAsync(
                new OrderConfirmationNotificationMessage(
                    request.UserId,
                    request.OrderId),
                cancellationToken);

            return Results.Accepted();
        });

        return app;
    }
}