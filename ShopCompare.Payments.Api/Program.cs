using Microsoft.EntityFrameworkCore;
using ShopCompare.Payments.Api;
using ShopCompare.Payments.Api.Infrastructure.Persistence;
using ShopCompare.SharedKernel.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPaymentsApi(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseGlobalExceptionHandling();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsDbContext>();
await dbContext.Database.MigrateAsync();

app.MapPaymentEndpoints();
app.MapInternalPaymentEndpoints();

app.Run();