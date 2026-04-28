using FluentAssertions;
using NetArchTest.Rules;

namespace ShopCompare.ArchitectureTests;

public static class ArchitectureTestExtensions
{
    public static void ShouldBeSuccessful(this TestResult result)
    {
        result.IsSuccessful.Should().BeTrue(
            "failing types: {0}",
            result.FailingTypeNames is null
                ? string.Empty
                : string.Join(", ", result.FailingTypeNames));
    }
}