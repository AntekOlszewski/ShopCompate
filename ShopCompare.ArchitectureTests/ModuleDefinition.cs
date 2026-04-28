using System.Reflection;

namespace ShopCompare.ArchitectureTests;

public sealed record ModuleDefinition(
    string Name,
    string Namespace,
    Assembly Assembly);