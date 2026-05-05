using ShopCompare.Catalog.Api.Domain;
using ShopCompare.Inventory.Api.Domain;
using ShopCompare.Modules.Notifications.Domain;
using ShopCompare.Modules.Orders.Domain;
using ShopCompare.Modules.Payments.Domain;

namespace ShopCompare.ArchitectureTests;

public static class ModuleAssemblies
{
    public static IReadOnlyCollection<ModuleDefinition> All { get; } =
    [
        new("Catalog", "ShopCompare.Catalog.Api", typeof(Product).Assembly),
        new("Inventory", "ShopCompare.Inventory.Api", typeof(StockItem).Assembly),
        new("Cart", "ShopCompare.Cart.Api", typeof(Cart.Api.Domain.Cart).Assembly),
        new("Orders", "ShopCompare.Modules.Orders", typeof(Order).Assembly),
        new("Payments", "ShopCompare.Modules.Payments", typeof(Payment).Assembly),
        new("Notifications", "ShopCompare.Modules.Notifications", typeof(Notification).Assembly),
    ];
}