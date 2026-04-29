using ShopCompare.SharedKernel.Exceptions;

namespace ShopCompare.Modules.Inventory.Domain;

public sealed class StockItem
{
    private StockItem()
    {
    }

    public StockItem(Guid productId, int availableQuantity)
    {
        if (productId == Guid.Empty)
            throw new DomainException("Product id cannot be empty.");

        if (availableQuantity < 0)
            throw new DomainException("Available quantity cannot be negative.");

        ProductId = productId;
        AvailableQuantity = availableQuantity;
        ReservedQuantity = 0;
    }

    public Guid ProductId { get; private set; }

    public int AvailableQuantity { get; private set; }

    public int ReservedQuantity { get; private set; }

    public int RemainingQuantity => AvailableQuantity - ReservedQuantity;

    public bool CanReserve(int quantity)
    {
        return quantity > 0 && RemainingQuantity >= quantity;
    }

    public void Reserve(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Reservation quantity must be greater than zero.");

        if (!CanReserve(quantity))
            throw new DomainException("Not enough stock available.");

        ReservedQuantity += quantity;
    }
}