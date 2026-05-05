namespace ShopCompare.Inventory.Api.Application.Stock.GetStock;

public sealed record StockResponse(
    Guid ProductId,
    int AvailableQuantity,
    int ReservedQuantity,
    int RemainingQuantity);