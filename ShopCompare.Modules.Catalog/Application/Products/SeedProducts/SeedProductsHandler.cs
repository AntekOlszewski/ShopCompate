using Microsoft.EntityFrameworkCore;
using ShopCompare.Modules.Catalog.Domain;
using ShopCompare.Modules.Catalog.Infrastructure.Persistence;

namespace ShopCompare.Modules.Catalog.Application.Products.SeedProducts;

public sealed class SeedProductsHandler(CatalogDbContext dbContext)
{
    public async Task<int> HandleAsync(
        SeedProductsRequest request,
        CancellationToken cancellationToken = default)
    {
        var existingProductsCount = await dbContext.Products
            .CountAsync(cancellationToken);

        if (existingProductsCount > 0)
        {
            return existingProductsCount;
        }

        var categories = Enumerable.Range(1, request.CategoriesCount)
            .Select(index => new Category(
                Guid.NewGuid(),
                $"Category {index}"))
            .ToList();

        var random = new Random(12345);

        var products = Enumerable.Range(1, request.ProductsCount)
            .Select(index =>
            {
                var category = categories[random.Next(categories.Count)];

                return new Product(
                    Guid.NewGuid(),
                    $"Product {index}",
                    $"Description for product {index}",
                    Math.Round((decimal)(random.NextDouble() * 1000 + 10), 2),
                    category.Id);
            })
            .ToList();

        await dbContext.Categories.AddRangeAsync(categories, cancellationToken);
        await dbContext.Products.AddRangeAsync(products, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return products.Count;
    }
}