namespace ShopCompare.Modules.Inventory.Application.Stock.ReserveStock;

public sealed record ReserveStockRequest(
    Guid? OrderId,
    IReadOnlyCollection<ReserveStockRequestItem> Items);

public sealed record ReserveStockRequestItem(
    Guid ProductId,
    int Quantity);