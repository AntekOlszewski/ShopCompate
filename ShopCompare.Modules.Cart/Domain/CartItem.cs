using ShopCompare.SharedKernel.Exceptions;

namespace ShopCompare.Modules.Cart.Domain;

public sealed class CartItem
{
    private CartItem()
    {
    }

    public CartItem(
        Guid id,
        Guid cartId,
        Guid productId,
        string productName,
        decimal unitPrice,
        int quantity)
    {
        if (id == Guid.Empty)
            throw new DomainException("Cart item id cannot be empty.");

        if (cartId == Guid.Empty)
            throw new DomainException("Cart id cannot be empty.");

        if (productId == Guid.Empty)
            throw new DomainException("Product id cannot be empty.");

        if (string.IsNullOrWhiteSpace(productName))
            throw new DomainException("Product name cannot be empty.");

        if (unitPrice < 0)
            throw new DomainException("Unit price cannot be negative.");

        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than zero.");

        Id = id;
        CartId = cartId;
        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public Guid Id { get; private set; }

    public Guid CartId { get; private set; }

    public Guid ProductId { get; private set; }

    public string ProductName { get; private set; } = string.Empty;

    public decimal UnitPrice { get; private set; }

    public int Quantity { get; private set; }

    public decimal TotalPrice => UnitPrice * Quantity;

    public void IncreaseQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than zero.");

        Quantity += quantity;
    }
}