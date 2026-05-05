using Microsoft.EntityFrameworkCore;
using ShopCompare.Inventory.Api;
using ShopCompare.Inventory.Api.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInventoryApi(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
await dbContext.Database.MigrateAsync();

app.MapInventoryEndpoints();
app.MapInternalInventoryEndpoints();

app.Run();