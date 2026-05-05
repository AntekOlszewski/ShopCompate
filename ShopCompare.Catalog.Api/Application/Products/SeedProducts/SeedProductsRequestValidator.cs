using FluentValidation;

namespace ShopCompare.Catalog.Api.Application.Products.SeedProducts;

public sealed class SeedProductsRequestValidator : AbstractValidator<SeedProductsRequest>
{
    public SeedProductsRequestValidator()
    {
        RuleFor(x => x.ProductsCount)
            .InclusiveBetween(1, 100_000);

        RuleFor(x => x.CategoriesCount)
            .InclusiveBetween(1, 1000);
    }
}