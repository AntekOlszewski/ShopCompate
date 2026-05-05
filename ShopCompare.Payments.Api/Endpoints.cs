using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ShopCompare.Payments.Api.Application.Payments.ProcessPayment;

namespace ShopCompare.Payments.Api;

public static class PaymentEndpoints
{
    public static IEndpointRouteBuilder MapPaymentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/payments")
            .WithTags("Payments");

        group.MapPost("/", async (
            ProcessPaymentRequest request,
            IValidator<ProcessPaymentRequest> validator,
            ProcessPaymentHandler handler,
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