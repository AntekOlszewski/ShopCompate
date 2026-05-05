using ShopCompare.SharedKernel.Exceptions;

namespace ShopCompare.Payments.Api.Domain;

public sealed class Payment
{
    private Payment()
    {
    }

    public Payment(
        Guid id,
        Guid orderId,
        decimal amount)
    {
        if (id == Guid.Empty)
            throw new DomainException("Payment id cannot be empty.");

        if (orderId == Guid.Empty)
            throw new DomainException("Order id cannot be empty.");

        if (amount <= 0)
            throw new DomainException("Payment amount must be greater than zero.");

        Id = id;
        OrderId = orderId;
        Amount = amount;
        Status = PaymentStatus.Pending;
        CreatedAtUtc = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; private set; }

    public Guid OrderId { get; private set; }

    public decimal Amount { get; private set; }

    public PaymentStatus Status { get; private set; }

    public DateTimeOffset CreatedAtUtc { get; private set; }

    public DateTimeOffset? CompletedAtUtc { get; private set; }

    public string? FailureReason { get; private set; }

    public void MarkAsSucceeded()
    {
        if (Status != PaymentStatus.Pending)
            throw new DomainException("Only pending payment can be completed.");

        Status = PaymentStatus.Succeeded;
        CompletedAtUtc = DateTimeOffset.UtcNow;
    }

    public void MarkAsFailed(string reason)
    {
        if (Status != PaymentStatus.Pending)
            throw new DomainException("Only pending payment can be failed.");

        if (string.IsNullOrWhiteSpace(reason))
            throw new DomainException("Failure reason cannot be empty.");

        Status = PaymentStatus.Failed;
        FailureReason = reason;
        CompletedAtUtc = DateTimeOffset.UtcNow;
    }
}