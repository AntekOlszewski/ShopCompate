using Microsoft.EntityFrameworkCore;
using ShopCompare.Api;
using ShopCompare.Api.Middleware;
using ShopCompare.Modules.Orders;
using ShopCompare.Modules.Orders.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationModules(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();

using (var scope = app.Services.CreateScope())
{
    var ordersDb = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();
    ordersDb.Database.Migrate();
}

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapOrderEndpoints();

app.Run();