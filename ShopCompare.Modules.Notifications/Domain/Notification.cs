using ShopCompare.SharedKernel.Exceptions;

namespace ShopCompare.Modules.Notifications.Domain;

public sealed class Notification
{
    private Notification()
    {
    }

    public Notification(
        Guid id,
        Guid userId,
        NotificationType type,
        string payload)
    {
        if (id == Guid.Empty)
            throw new DomainException("Notification id cannot be empty.");

        if (userId == Guid.Empty)
            throw new DomainException("User id cannot be empty.");

        if (string.IsNullOrWhiteSpace(payload))
            throw new DomainException("Notification payload cannot be empty.");

        Id = id;
        UserId = userId;
        Type = type;
        Payload = payload;
        Status = NotificationStatus.Pending;
        CreatedAtUtc = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public NotificationType Type { get; private set; }

    public string Payload { get; private set; } = string.Empty;

    public NotificationStatus Status { get; private set; }

    public DateTimeOffset CreatedAtUtc { get; private set; }

    public DateTimeOffset? SentAtUtc { get; private set; }

    public string? FailureReason { get; private set; }

    public void MarkAsSent()
    {
        if (Status != NotificationStatus.Pending)
            throw new DomainException("Only pending notification can be sent.");

        Status = NotificationStatus.Sent;
        SentAtUtc = DateTimeOffset.UtcNow;
    }

    public void MarkAsFailed(string reason)
    {
        if (Status != NotificationStatus.Pending)
            throw new DomainException("Only pending notification can be failed.");

        if (string.IsNullOrWhiteSpace(reason))
            throw new DomainException("Failure reason cannot be empty.");

        Status = NotificationStatus.Failed;
        FailureReason = reason;
    }
}