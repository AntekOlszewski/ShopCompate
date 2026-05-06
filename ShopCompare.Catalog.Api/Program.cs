using Microsoft.EntityFrameworkCore;
using ShopCompare.Catalog.Api;
using ShopCompare.Catalog.Api.Infrastructure.Persistence;
using ShopCompare.SharedKernel.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCatalogApi(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseGlobalExceptionHandling();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
await dbContext.Database.MigrateAsync();

app.MapCatalogEndpoints();
app.MapInternalCatalogEndpoints();

app.Run();