using Microsoft.EntityFrameworkCore;
using ShopCompare.Cart.Api;
using ShopCompare.Cart.Api.Infrastructure.Persistence;
using ShopCompare.SharedKernel.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCartApi(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseGlobalExceptionHandling();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<CartDbContext>();
await dbContext.Database.MigrateAsync();

app.MapCartEndpoints();
app.MapInternalCartEndpoints();

app.Run();