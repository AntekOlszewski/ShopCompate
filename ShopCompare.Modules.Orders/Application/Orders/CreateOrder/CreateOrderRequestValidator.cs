using FluentValidation;

namespace ShopCompare.Modules.Orders.Application.Orders.CreateOrder;

public sealed class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}