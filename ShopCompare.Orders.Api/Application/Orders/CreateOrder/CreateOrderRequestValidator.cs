using FluentValidation;

namespace ShopCompare.Orders.Api.Application.Orders.CreateOrder;

public sealed class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}