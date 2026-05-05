namespace ShopCompare.Inventory.Api.Application.Stock.ReserveStock;

public sealed record ReserveStockResponse(
    bool Success,
    string? Error);