var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume("shopcompare");

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

builder.AddProject<Projects.ShopCompare_Api>("api")
    .WithReference(postgres)
    .WaitFor(postgres);

builder.Build().Run();