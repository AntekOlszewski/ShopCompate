using FluentValidation;

namespace ShopCompare.Modules.Notifications.Application.Notifications.SendOrderConfirmation;

public sealed class SendOrderConfirmationRequestValidator
    : AbstractValidator<SendOrderConfirmationRequest>
{
    public SendOrderConfirmationRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.OrderId)
            .NotEmpty();
    }
}