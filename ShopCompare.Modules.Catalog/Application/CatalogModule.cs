using Microsoft.EntityFrameworkCore;
using ShopCompare.Modules.Catalog.Infrastructure.Persistence;
using ShopCompare.Modules.Catalog.PublicApi;

namespace ShopCompare.Modules.Catalog.Application;

internal sealed class CatalogModule(CatalogDbContext dbContext) : ICatalogModule
{
    public async Task<CatalogProductDto?> GetProductAsync(
        Guid productId,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Products
            .AsNoTracking()
            .Where(x => x.Id == productId && x.IsActive)
            .Select(x => new CatalogProductDto(
                x.Id,
                x.Name,
                x.Price,
                x.IsActive))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> ProductExistsAsync(
        Guid productId,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Products
            .AsNoTracking()
            .AnyAsync(x => x.Id == productId && x.IsActive, cancellationToken);
    }
    
    public async Task<IReadOnlyCollection<CatalogProductDto>> GetProductsAsync(
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Products
            .AsNoTracking()
            .Select(x => new CatalogProductDto(
                x.Id,
                x.Name,
                x.Price,
                x.IsActive))
            .ToListAsync(cancellationToken);
    }
}