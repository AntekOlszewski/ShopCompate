using FluentValidation;

namespace ShopCompare.Modules.Payments.Application.Payments.ProcessPayment;

public sealed class ProcessPaymentRequestValidator : AbstractValidator<ProcessPaymentRequest>
{
    public ProcessPaymentRequestValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty();

        RuleFor(x => x.Amount)
            .GreaterThan(0);
    }
}