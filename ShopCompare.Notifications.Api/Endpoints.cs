using Microsoft.EntityFrameworkCore;
using ShopCompare.Notifications.Api.Infrastructure.Persistence;

namespace ShopCompare.Notifications.Api;

public static class NotificationEndpoints
{
    public static IEndpointRouteBuilder MapNotificationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/notifications")
            .WithTags("Notifications");

        group.MapGet("/", async (
            NotificationsDbContext dbContext,
            CancellationToken cancellationToken) =>
        {
            var notifications = await dbContext.Notifications
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAtUtc)
                .Take(100)
                .Select(x => new
                {
                    x.Id,
                    x.UserId,
                    Type = x.Type.ToString(),
                    Status = x.Status.ToString(),
                    x.CreatedAtUtc,
                    x.SentAtUtc,
                    x.FailureReason
                })
                .ToListAsync(cancellationToken);

            return Results.Ok(notifications);
        });

        return app;
    }
}