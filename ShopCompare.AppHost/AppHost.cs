var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");

var catalogDb = postgres.AddDatabase("catalog-db");

var catalog = builder.AddProject<Projects.ShopCompare_Catalog_Api>("catalog-api")
    .WithReference(catalogDb)
    .WaitFor(catalogDb);

var inventoryDb = postgres.AddDatabase("inventory-db");

var inventory = builder.AddProject<Projects.ShopCompare_Inventory_Api>("inventory-api")
    .WithReference(inventoryDb)
    .WithReference(catalog)
    .WaitFor(inventoryDb)
    .WaitFor(catalog);

var cartDb = postgres.AddDatabase("cart-db");

var cart = builder.AddProject<Projects.ShopCompare_Cart_Api>("cart-api")
    .WithReference(cartDb)
    .WithReference(catalog)
    .WaitFor(cartDb)
    .WaitFor(catalog);

var paymentsDb = postgres.AddDatabase("payments-db");

var payments = builder.AddProject<Projects.ShopCompare_Payments_Api>("payments-api")
    .WithReference(paymentsDb)
    .WaitFor(paymentsDb);

var notificationsDb = postgres.AddDatabase("notifications-db");

var notifications = builder.AddProject<Projects.ShopCompare_Notifications_Api>("notifications-api")
    .WithReference(notificationsDb)
    .WaitFor(notificationsDb);

var ordersDb = postgres.AddDatabase("orders-db");

var orders = builder.AddProject<Projects.ShopCompare_Orders_Api>("orders-api")
    .WithReference(ordersDb)
    .WithReference(cart)
    .WithReference(inventory)
    .WithReference(payments)
    .WithReference(notifications)
    .WaitFor(ordersDb)
    .WaitFor(cart)
    .WaitFor(inventory)
    .WaitFor(payments)
    .WaitFor(notifications);

var gateway = builder.AddYarp("gateway")
    .WithConfiguration(yarp =>
    {
        yarp.AddRoute("/api/products/{**catch-all}", catalog);
        yarp.AddRoute("/api/inventory/{**catch-all}", inventory);
        yarp.AddRoute("/api/carts/{**catch-all}", cart);
        yarp.AddRoute("/api/orders/{**catch-all}", orders);
        yarp.AddRoute("/api/payments/{**catch-all}", payments);
        yarp.AddRoute("/api/notifications/{**catch-all}", notifications);
    });

builder.Build().Run();