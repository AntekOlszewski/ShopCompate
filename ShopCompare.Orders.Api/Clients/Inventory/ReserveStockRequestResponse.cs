namespace ShopCompare.Orders.Api.Clients.Inventory;

public sealed record ReserveStockResponse(
    bool Success,
    string? Error);