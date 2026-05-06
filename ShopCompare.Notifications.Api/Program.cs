using Microsoft.EntityFrameworkCore;
using ShopCompare.Notifications.Api;
using ShopCompare.Notifications.Api.Infrastructure.Persistence;
using ShopCompare.SharedKernel.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddNotificationsApi(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseGlobalExceptionHandling();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<NotificationsDbContext>();
await dbContext.Database.MigrateAsync();

app.MapNotificationEndpoints();
app.MapInternalNotificationEndpoints();

app.Run();