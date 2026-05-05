using FluentAssertions;
using Xunit;

namespace ShopCompare.UnitTests;

public sealed class CartTests
{
    [Fact]
    public void AddItem_Should_Add_New_Item()
    {
        var cart = new Cart.Api.Domain.Cart(Guid.NewGuid(), Guid.NewGuid());

        cart.AddItem(Guid.NewGuid(), "Product 1", 100m, 2);

        cart.Items.Should().HaveCount(1);
        cart.TotalAmount.Should().Be(200m);
    }

    [Fact]
    public void AddItem_When_Product_Already_Exists_Should_Increase_Quantity()
    {
        var cart = new Cart.Api.Domain.Cart(Guid.NewGuid(), Guid.NewGuid());
        var productId = Guid.NewGuid();

        cart.AddItem(productId, "Product 1", 100m, 2);
        cart.AddItem(productId, "Product 1", 100m, 3);

        cart.Items.Should().HaveCount(1);
        cart.Items.Single().Quantity.Should().Be(5);
        cart.TotalAmount.Should().Be(500m);
    }
}