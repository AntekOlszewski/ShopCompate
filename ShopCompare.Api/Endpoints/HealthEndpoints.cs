namespace ShopCompare.Api.Endpoints;

public static class HealthEndpoints
{
    public static IEndpointRouteBuilder MapHealthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/health", () => Results.Ok(new
            {
                status = "ok"
            }))
            .WithTags("Health");

        return app;
    }
}