using Microsoft.EntityFrameworkCore;
using ShopCompare.Api;
using ShopCompare.Api.Middleware;
using ShopCompare.Modules.Cart;
using ShopCompare.Modules.Cart.Infrastructure.Persistence;
using ShopCompare.Catalog.Api;
using ShopCompare.Catalog.Api.Infrastructure.Persistence;
using ShopCompare.Modules.Inventory;
using ShopCompare.Modules.Inventory.Infrastructure.Persistence;
using ShopCompare.Modules.Notifications;
using ShopCompare.Modules.Notifications.Infrastructure.Persistence;
using ShopCompare.Modules.Orders;
using ShopCompare.Modules.Orders.Infrastructure.Persistence;
using ShopCompare.Modules.Payments;
using ShopCompare.Modules.Payments.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationModules(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();

using (var scope = app.Services.CreateScope())
{
    var catalogDb = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    catalogDb.Database.Migrate();
    
    var inventoryDb = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
    inventoryDb.Database.Migrate();
    
    var cartDb = scope.ServiceProvider.GetRequiredService<CartDbContext>();
    cartDb.Database.Migrate();
    
    var paymentsDb = scope.ServiceProvider.GetRequiredService<PaymentsDbContext>();
    paymentsDb.Database.Migrate();
    
    var notificationsDb = scope.ServiceProvider.GetRequiredService<NotificationsDbContext>();
    notificationsDb.Database.Migrate();
    
    var ordersDb = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();
    ordersDb.Database.Migrate();
}

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCatalogEndpoints();
app.MapInventoryEndpoints();
app.MapCartEndpoints();
app.MapPaymentEndpoints();
app.MapNotificationEndpoints();
app.MapOrderEndpoints();

app.Run();