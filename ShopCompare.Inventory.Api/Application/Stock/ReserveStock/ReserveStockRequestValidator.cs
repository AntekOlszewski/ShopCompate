using FluentValidation;

namespace ShopCompare.Inventory.Api.Application.Stock.ReserveStock;

public sealed class ReserveStockRequestValidator : AbstractValidator<ReserveStockRequest>
{
    public ReserveStockRequestValidator()
    {
        RuleFor(x => x.Items)
            .NotEmpty();

        RuleForEach(x => x.Items)
            .ChildRules(item =>
            {
                item.RuleFor(x => x.ProductId).NotEmpty();
                item.RuleFor(x => x.Quantity).GreaterThan(0);
            });
    }
}