var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ShopCompare_Api>("api");

builder.Build().Run();