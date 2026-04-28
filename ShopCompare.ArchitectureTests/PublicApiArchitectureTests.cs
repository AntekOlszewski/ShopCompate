using NetArchTest.Rules;
using Xunit;

namespace ShopCompare.ArchitectureTests;

public sealed class PublicApiArchitectureTests
{
    public static IEnumerable<object[]> Modules =>
        ModuleAssemblies.All.Select(module => new object[] { module });

    [Theory]
    [MemberData(nameof(Modules))]
    public void PublicApi_Should_Not_Depend_On_Domain(ModuleDefinition module)
    {
        var result = Types
            .InAssembly(module.Assembly)
            .That()
            .ResideInNamespace($"{module.Namespace}.PublicApi")
            .ShouldNot()
            .HaveDependencyOn($"{module.Namespace}.Domain")
            .GetResult();

        result.ShouldBeSuccessful();
    }

    [Theory]
    [MemberData(nameof(Modules))]
    public void PublicApi_Should_Not_Depend_On_Application(ModuleDefinition module)
    {
        var result = Types
            .InAssembly(module.Assembly)
            .That()
            .ResideInNamespace($"{module.Namespace}.PublicApi")
            .ShouldNot()
            .HaveDependencyOn($"{module.Namespace}.Application")
            .GetResult();

        result.ShouldBeSuccessful();
    }

    [Theory]
    [MemberData(nameof(Modules))]
    public void PublicApi_Should_Not_Depend_On_Infrastructure(ModuleDefinition module)
    {
        var result = Types
            .InAssembly(module.Assembly)
            .That()
            .ResideInNamespace($"{module.Namespace}.PublicApi")
            .ShouldNot()
            .HaveDependencyOn($"{module.Namespace}.Infrastructure")
            .GetResult();

        result.ShouldBeSuccessful();
    }

    [Theory]
    [MemberData(nameof(Modules))]
    public void PublicApi_Should_Not_Depend_On_Endpoints(ModuleDefinition module)
    {
        var result = Types
            .InAssembly(module.Assembly)
            .That()
            .ResideInNamespace($"{module.Namespace}.PublicApi")
            .ShouldNot()
            .HaveDependencyOn($"{module.Namespace}.Endpoints")
            .GetResult();

        result.ShouldBeSuccessful();
    }
}