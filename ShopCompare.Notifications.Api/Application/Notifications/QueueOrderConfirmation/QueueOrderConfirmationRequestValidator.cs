using FluentValidation;

namespace ShopCompare.Notifications.Api.Application.Notifications.QueueOrderConfirmation;

public sealed class QueueOrderConfirmationRequestValidator
    : AbstractValidator<QueueOrderConfirmationRequest>
{
    public QueueOrderConfirmationRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.OrderId).NotEmpty();
    }
}