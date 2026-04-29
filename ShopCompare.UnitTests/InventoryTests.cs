using FluentAssertions;
using ShopCompare.Modules.Inventory.Domain;
using ShopCompare.SharedKernel.Exceptions;
using Xunit;

namespace ShopCompare.UnitTests;

public sealed class InventoryTests
{
    [Fact]
    public void Reserve_Should_Increase_Reserved_Quantity()
    {
        var stockItem = new StockItem(Guid.NewGuid(), 10);

        stockItem.Reserve(4);

        stockItem.ReservedQuantity.Should().Be(4);
        stockItem.RemainingQuantity.Should().Be(6);
    }

    [Fact]
    public void Reserve_When_Not_Enough_Stock_Should_Throw()
    {
        var stockItem = new StockItem(Guid.NewGuid(), 10);

        var act = () => stockItem.Reserve(11);

        act.Should().Throw<DomainException>();
    }
}