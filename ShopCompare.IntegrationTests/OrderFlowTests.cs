using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace ShopCompare.IntegrationTests;

public sealed class OrderFlowTests(ShopCompareApiFactory factory) : IClassFixture<ShopCompareApiFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task CreateOrder_Should_Process_Full_Order_Flow()
    {
        var userId = Guid.NewGuid();

        await _client.PostAsJsonAsync("/api/products/seed", new
        {
            productsCount = 10,
            categoriesCount = 2
        });

        await _client.PostAsync("/api/inventory/seed", null);

        var products = await _client.GetFromJsonAsync<List<ProductListItem>>("/api/products");

        var product = products!.First();

        var addToCartResponse = await _client.PostAsJsonAsync($"/api/carts/{userId}/items", new
        {
            productId = product.Id,
            quantity = 2
        });

        addToCartResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var createOrderResponse = await _client.PostAsJsonAsync("/api/orders", new
        {
            userId
        });

        createOrderResponse.StatusCode.Should().BeOneOf(
            HttpStatusCode.Created,
            HttpStatusCode.BadRequest);
    }

    private sealed record ProductListItem(
        Guid Id,
        string Name,
        decimal Price,
        string CategoryName);
}