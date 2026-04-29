namespace ShopCompare.Modules.Orders.Application.Orders.GetOrder;

public sealed record OrderDetailsResponse(
    Guid Id,
    Guid UserId,
    string Status,
    decimal TotalAmount,
    Guid? PaymentId,
    DateTimeOffset CreatedAtUtc,
    DateTimeOffset? PaidAtUtc,
    string? FailureReason,
    IReadOnlyCollection<OrderItemResponse> Items);