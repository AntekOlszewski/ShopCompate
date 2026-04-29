using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace ShopCompare.IntegrationTests;

public sealed class ShopCompareApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var testConfiguration = new Dictionary<string, string?>
            {
                ["ConnectionStrings:shopcompare-db"] =
                    "Host=localhost;Port=5432;Database=shopcompare-db;Username=postgres;Password=postgres"
            };

            configurationBuilder.AddInMemoryCollection(testConfiguration);
        });
    }
}