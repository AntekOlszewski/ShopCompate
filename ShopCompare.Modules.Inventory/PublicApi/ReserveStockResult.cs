namespace ShopCompare.Modules.Inventory.PublicApi;

public sealed record ReserveStockResult(
    bool Success,
    string? Error);