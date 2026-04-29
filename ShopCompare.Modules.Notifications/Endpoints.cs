using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ShopCompare.Modules.Notifications.Application.Notifications.SendOrderConfirmation;

namespace ShopCompare.Modules.Notifications;

public static class NotificationEndpoints
{
    public static IEndpointRouteBuilder MapNotificationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/notifications")
            .WithTags("Notifications");

        group.MapPost("/order-confirmation", async (
            SendOrderConfirmationRequest request,
            IValidator<SendOrderConfirmationRequest> validator,
            SendOrderConfirmationHandler handler,
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

        return app;
    }
}