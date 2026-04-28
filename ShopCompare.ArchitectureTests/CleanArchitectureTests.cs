using NetArchTest.Rules;
using Xunit;

namespace ShopCompare.ArchitectureTests;

public sealed class CleanArchitectureTests
{
    public static IEnumerable<object[]> Modules =>
        ModuleAssemblies.All.Select(module => new object[] { module });

    [Theory]
    [MemberData(nameof(Modules))]
    public void Domain_Should_Not_Depend_On_Application(ModuleDefinition module)
    {
        var result = Types
            .InAssembly(module.Assembly)
            .That()
            .ResideInNamespace($"{module.Namespace}.Domain")
            .ShouldNot()
            .HaveDependencyOn($"{module.Namespace}.Application")
            .GetResult();

        result.ShouldBeSuccessful();
    }

    [Theory]
    [MemberData(nameof(Modules))]
    public void Domain_Should_Not_Depend_On_Infrastructure(ModuleDefinition module)
    {
        var result = Types
            .InAssembly(module.Assembly)
            .That()
            .ResideInNamespace($"{module.Namespace}.Domain")
            .ShouldNot()
            .HaveDependencyOn($"{module.Namespace}.Infrastructure")
            .GetResult();

        result.ShouldBeSuccessful();
    }

    [Theory]
    [MemberData(nameof(Modules))]
    public void Domain_Should_Not_Depend_On_Endpoints(ModuleDefinition module)
    {
        var result = Types
            .InAssembly(module.Assembly)
            .That()
            .ResideInNamespace($"{module.Namespace}.Domain")
            .ShouldNot()
            .HaveDependencyOn($"{module.Namespace}.Endpoints")
            .GetResult();

        result.ShouldBeSuccessful();
    }

    [Theory]
    [MemberData(nameof(Modules))]
    public void Domain_Should_Not_Depend_On_PublicApi(ModuleDefinition module)
    {
        var result = Types
            .InAssembly(module.Assembly)
            .That()
            .ResideInNamespace($"{module.Namespace}.Domain")
            .ShouldNot()
            .HaveDependencyOn($"{module.Namespace}.PublicApi")
            .GetResult();

        result.ShouldBeSuccessful();
    }

    // [Theory]
    // [MemberData(nameof(Modules))]
    // public void Application_Should_Not_Depend_On_Infrastructure(ModuleDefinition module)
    // {
    //     var result = Types
    //         .InAssembly(module.Assembly)
    //         .That()
    //         .ResideInNamespace($"{module.Namespace}.Application")
    //         .ShouldNot()
    //         .HaveDependencyOn($"{module.Namespace}.Infrastructure")
    //         .GetResult();
    //
    //     result.ShouldBeSuccessful();
    // }

    [Theory]
    [MemberData(nameof(Modules))]
    public void Application_Should_Not_Depend_On_Endpoints(ModuleDefinition module)
    {
        var result = Types
            .InAssembly(module.Assembly)
            .That()
            .ResideInNamespace($"{module.Namespace}.Application")
            .ShouldNot()
            .HaveDependencyOn($"{module.Namespace}.Endpoints")
            .GetResult();

        result.ShouldBeSuccessful();
    }

    [Theory]
    [MemberData(nameof(Modules))]
    public void Infrastructure_Should_Not_Depend_On_Endpoints(ModuleDefinition module)
    {
        var result = Types
            .InAssembly(module.Assembly)
            .That()
            .ResideInNamespace($"{module.Namespace}.Infrastructure")
            .ShouldNot()
            .HaveDependencyOn($"{module.Namespace}.Endpoints")
            .GetResult();

        result.ShouldBeSuccessful();
    }
}