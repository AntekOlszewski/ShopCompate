using ShopCompare.SharedKernel.Exceptions;

namespace ShopCompare.Modules.Inventory.Domain;

public sealed class StockReservation
{
    private StockReservation()
    {
    }

    public StockReservation(
        Guid id,
        Guid productId,
        int quantity,
        Guid? orderId = null)
    {
        if (id == Guid.Empty)
            throw new DomainException("Reservation id cannot be empty.");

        if (productId == Guid.Empty)
            throw new DomainException("Product id cannot be empty.");

        if (quantity <= 0)
            throw new DomainException("Reservation quantity must be greater than zero.");

        Id = id;
        ProductId = productId;
        Quantity = quantity;
        OrderId = orderId;
        CreatedAtUtc = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; private set; }

    public Guid ProductId { get; private set; }

    public int Quantity { get; private set; }

    public Guid? OrderId { get; private set; }

    public DateTimeOffset CreatedAtUtc { get; private set; }
}