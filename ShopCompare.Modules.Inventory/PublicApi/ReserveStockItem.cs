namespace ShopCompare.Modules.Inventory.PublicApi;

public sealed record ReserveStockItem(
    Guid ProductId,
    int Quantity);