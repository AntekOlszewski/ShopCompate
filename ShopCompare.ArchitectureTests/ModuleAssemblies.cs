using ShopCompare.Modules.Cart.Domain;
using ShopCompare.Modules.Catalog.Domain;
using ShopCompare.Modules.Inventory.Domain;
using ShopCompare.Modules.Notifications.Domain;
using ShopCompare.Modules.Payments.Domain;

namespace ShopCompare.ArchitectureTests;

public static class ModuleAssemblies
{
    public static IReadOnlyCollection<ModuleDefinition> All { get; } =
    [
        new("Catalog", "ShopCompare.Modules.Catalog", typeof(Product).Assembly),
        new("Inventory", "ShopCompare.Modules.Inventory", typeof(StockItem).Assembly),
        new("Cart", "ShopCompare.Modules.Cart", typeof(Cart).Assembly),
        // new("Orders", "ShopCompare.Modules.Orders", typeof(Order).Assembly),
        new("Payments", "ShopCompare.Modules.Payments", typeof(Payment).Assembly),
        new("Notifications", "ShopCompare.Modules.Notifications", typeof(Notification).Assembly),
    ];
}