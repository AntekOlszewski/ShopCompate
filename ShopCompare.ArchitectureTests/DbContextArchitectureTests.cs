using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ShopCompare.ArchitectureTests;

public sealed class DbContextArchitectureTests
{
    public static IEnumerable<object[]> Modules =>
        ModuleAssemblies.All.Select(module => new object[] { module });

    [Theory]
    [MemberData(nameof(Modules))]
    public void Each_Module_Should_Have_Exactly_One_DbContext(ModuleDefinition module)
    {
        var dbContexts = module.Assembly
            .GetTypes()
            .Where(type =>
                !type.IsAbstract &&
                typeof(DbContext).IsAssignableFrom(type))
            .ToList();

        dbContexts.Should().HaveCount(
            1,
            $"{module.Name} module should have exactly one dedicated DbContext");
    }

    [Theory]
    [MemberData(nameof(Modules))]
    public void Module_DbContext_Should_Be_Defined_In_Infrastructure_Namespace(
        ModuleDefinition module)
    {
        var dbContext = module.Assembly
            .GetTypes()
            .SingleOrDefault(type =>
                !type.IsAbstract &&
                typeof(DbContext).IsAssignableFrom(type));

        dbContext.Should().NotBeNull();

        dbContext!.Namespace.Should().StartWith(
            $"{module.Namespace}.Infrastructure");
    }
}