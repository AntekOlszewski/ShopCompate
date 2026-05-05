namespace ShopCompare.Orders.Api.Clients.Cart;

public interface ICartClient
{
    Task<CartDto?> GetCartAsync(Guid userId, CancellationToken cancellationToken = default);

    Task ClearCartAsync(Guid userId, CancellationToken cancellationToken = default);
}
