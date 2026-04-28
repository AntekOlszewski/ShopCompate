using NetArchTest.Rules;
using Xunit;

namespace ShopCompare.ArchitectureTests;

public sealed class ModuleBoundaryTests
{
    public static IEnumerable<object[]> ModulePairs =>
        ModuleAssemblies.All
            .SelectMany(sourceModule =>
                ModuleAssemblies.All
                    .Where(targetModule => targetModule != sourceModule)
                    .Select(targetModule => new object[]
                    {
                        sourceModule,
                        targetModule
                    }));

    [Theory]
    [MemberData(nameof(ModulePairs))]
    public void Modules_Should_Not_Depend_On_Other_Modules_Internal_Layers(
        ModuleDefinition sourceModule,
        ModuleDefinition targetModule)
    {
        var forbiddenNamespaces = new[]
        {
            $"{targetModule.Namespace}.Domain",
            $"{targetModule.Namespace}.Application",
            $"{targetModule.Namespace}.Infrastructure",
            $"{targetModule.Namespace}.Endpoints"
        };

        foreach (var forbiddenNamespace in forbiddenNamespaces)
        {
            var result = Types
                .InAssembly(sourceModule.Assembly)
                .ShouldNot()
                .HaveDependencyOn(forbiddenNamespace)
                .GetResult();

            result.ShouldBeSuccessful();
        }
    }
}