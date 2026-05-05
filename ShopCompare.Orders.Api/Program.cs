using Microsoft.EntityFrameworkCore;
using ShopCompare.Orders.Api;
using ShopCompare.Orders.Api.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOrdersApi(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();
await dbContext.Database.MigrateAsync();

app.MapOrderEndpoints();

app.Run();