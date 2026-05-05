namespace ShopCompare.Orders.Api.Clients.Inventory;

public sealed record ReserveStockRequest(
    Guid? OrderId,
    IReadOnlyCollection<ReserveStockRequestItem> Items);

public sealed record ReserveStockRequestItem(
    Guid ProductId,
    int Quantity);