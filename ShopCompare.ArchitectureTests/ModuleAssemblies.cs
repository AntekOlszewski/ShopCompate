using ShopCompare.Catalog.Api.Domain;
using ShopCompare.Inventory.Api.Domain;
using ShopCompare.Notifications.Api.Domain;
using ShopCompare.Orders.Api.Domain;
using ShopCompare.Payments.Api.Domain;

namespace ShopCompare.ArchitectureTests;

public static class ModuleAssemblies
{
    public static IReadOnlyCollection<ModuleDefinition> All { get; } =
    [
        new("Catalog", "ShopCompare.Catalog.Api", typeof(Product).Assembly),
        new("Inventory", "ShopCompare.Inventory.Api", typeof(StockItem).Assembly),
        new("Cart", "ShopCompare.Cart.Api", typeof(Cart.Api.Domain.Cart).Assembly),
        new("Orders", "ShopCompare.Orders.Api", typeof(Order).Assembly),
        new("Payments", "ShopCompare.Payments.Api", typeof(Payment).Assembly),
        new("Notifications", "ShopCompare.Notifications.Api", typeof(Notification).Assembly),
    ];
}