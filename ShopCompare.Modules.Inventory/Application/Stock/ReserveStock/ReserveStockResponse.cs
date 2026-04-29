namespace ShopCompare.Modules.Inventory.Application.Stock.ReserveStock;

public sealed record ReserveStockResponse(
    bool Success,
    string? Error);