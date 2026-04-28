using Microsoft.EntityFrameworkCore;
using ShopCompare.Modules.Catalog.Infrastructure.Persistence;

namespace ShopCompare.Modules.Catalog.Application.Products.GetProducts;

public sealed class GetProductsHandler(CatalogDbContext dbContext)
{
    public async Task<IReadOnlyCollection<ProductListItemResponse>> HandleAsync(
        GetProductsQuery query,
        CancellationToken cancellationToken = default)
    {
        var page = query.Page < 1 ? 1 : query.Page;
        var pageSize = query.PageSize is < 1 or > 200 ? 50 : query.PageSize;

        var productsQuery = dbContext.Products
            .AsNoTracking()
            .Include(x => x.Category)
            .Where(x => x.IsActive);

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            productsQuery = productsQuery.Where(x =>
                x.Name.Contains(query.Search) ||
                x.Description.Contains(query.Search));
        }

        return await productsQuery
            .OrderBy(x => x.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new ProductListItemResponse(
                x.Id,
                x.Name,
                x.Price,
                x.Category.Name))
            .ToListAsync(cancellationToken);
    }
}