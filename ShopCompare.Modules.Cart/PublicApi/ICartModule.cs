namespace ShopCompare.Modules.Cart.PublicApi;

public interface ICartModule
{
    Task<CartDto?> GetCartAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

    Task ClearCartAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}