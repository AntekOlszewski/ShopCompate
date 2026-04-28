using Microsoft.EntityFrameworkCore;
using ShopCompare.Modules.Catalog.Infrastructure.Persistence;

namespace ShopCompare.Modules.Catalog.Application.Products.GetProductById;

public sealed class GetProductByIdHandler(CatalogDbContext dbContext)
{
    public async Task<ProductDetailsResponse?> HandleAsync(
        Guid productId,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Products
            .AsNoTracking()
            .Include(x => x.Category)
            .Where(x => x.Id == productId)
            .Select(x => new ProductDetailsResponse(
                x.Id,
                x.Name,
                x.Description,
                x.Price,
                x.Category.Name,
                x.IsActive))
            .FirstOrDefaultAsync(cancellationToken);
    }
}