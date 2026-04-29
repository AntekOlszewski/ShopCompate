using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace ShopCompare.IntegrationTests;

public sealed class CatalogFlowTests(ShopCompareApiFactory factory) : IClassFixture<ShopCompareApiFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task SeedProducts_Should_Create_Products()
    {
        var response = await _client.PostAsJsonAsync("/api/products/seed", new
        {
            productsCount = 10,
            categoriesCount = 2
        });

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var productsResponse = await _client.GetAsync("/api/products");

        productsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}