using FluentAssertions;
using ShopCompare.Orders.Api.Domain;
using ShopCompare.SharedKernel.Exceptions;
using Xunit;

namespace ShopCompare.UnitTests;

public sealed class OrderTests
{
    [Fact]
    public void MarkAsPaid_When_Order_Is_Already_Paid_Should_Throw()
    {
        var orderId = Guid.NewGuid();

        var order = new Order(
            orderId,
            Guid.NewGuid(),
            new[]
            {
                new OrderItem(
                    Guid.NewGuid(),
                    orderId,
                    Guid.NewGuid(),
                    "Product 1",
                    100m,
                    2)
            });

        order.MarkAsPaid(Guid.NewGuid());

        var act = () => order.MarkAsPaid(Guid.NewGuid());

        act.Should().Throw<DomainException>();
    }
}