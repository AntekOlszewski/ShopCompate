var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .AddDatabase("shopcompare-db");

builder.AddProject<Projects.ShopCompare_Api>("api")
    .WithReference(postgres)
    .WaitFor(postgres);

builder.Build().Run();