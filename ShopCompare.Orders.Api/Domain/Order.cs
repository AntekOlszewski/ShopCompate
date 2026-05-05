using ShopCompare.SharedKernel.Exceptions;

namespace ShopCompare.Orders.Api.Domain;

public sealed class Order
{
    private readonly List<OrderItem> _items = new();

    private Order()
    {
    }

    public Order(Guid id, Guid userId, IEnumerable<OrderItem> items)
    {
        if (id == Guid.Empty)
            throw new DomainException("Order id cannot be empty.");

        if (userId == Guid.Empty)
            throw new DomainException("User id cannot be empty.");

        var orderItems = items.ToList();

        if (orderItems.Count == 0)
            throw new DomainException("Order must contain at least one item.");

        Id = id;
        UserId = userId;
        Status = OrderStatus.PendingPayment;
        CreatedAtUtc = DateTimeOffset.UtcNow;

        _items.AddRange(orderItems);
    }

    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public OrderStatus Status { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items;

    public decimal TotalAmount => _items.Sum(x => x.TotalPrice);

    public Guid? PaymentId { get; private set; }

    public DateTimeOffset CreatedAtUtc { get; private set; }

    public DateTimeOffset? PaidAtUtc { get; private set; }

    public DateTimeOffset? FailedAtUtc { get; private set; }

    public string? FailureReason { get; private set; }

    public bool IsPaid => Status == OrderStatus.Paid;

    public void MarkAsPaid(Guid paymentId)
    {
        if (paymentId == Guid.Empty)
            throw new DomainException("Payment id cannot be empty.");

        if (Status == OrderStatus.Paid)
            throw new DomainException("Order is already paid.");

        if (Status == OrderStatus.Cancelled)
            throw new DomainException("Cancelled order cannot be paid.");

        PaymentId = paymentId;
        Status = OrderStatus.Paid;
        PaidAtUtc = DateTimeOffset.UtcNow;
    }

    public void MarkPaymentAsFailed(string reason)
    {
        if (Status == OrderStatus.Paid)
            throw new DomainException("Paid order cannot be marked as payment failed.");

        if (string.IsNullOrWhiteSpace(reason))
            throw new DomainException("Failure reason cannot be empty.");

        Status = OrderStatus.PaymentFailed;
        FailureReason = reason;
        FailedAtUtc = DateTimeOffset.UtcNow;
    }
}