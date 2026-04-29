namespace ShopCompare.Modules.Inventory.Application.Stock.GetStock;

public sealed record StockResponse(
    Guid ProductId,
    int AvailableQuantity,
    int ReservedQuantity,
    int RemainingQuantity);