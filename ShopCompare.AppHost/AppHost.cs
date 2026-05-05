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

builder.AddProject<Projects.ShopCompare_Api>("api")
    .WithReference(postgres)
    .WaitFor(postgres);

builder.Build().Run();