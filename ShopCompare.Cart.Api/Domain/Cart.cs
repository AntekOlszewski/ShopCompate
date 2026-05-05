using ShopCompare.SharedKernel.Exceptions;

namespace ShopCompare.Cart.Api.Domain;

public sealed class Cart
{
    private readonly List<CartItem> _items = new();

    private Cart()
    {
    }

    public Cart(Guid id, Guid userId)
    {
        if (id == Guid.Empty)
            throw new DomainException("Cart id cannot be empty.");

        if (userId == Guid.Empty)
            throw new DomainException("User id cannot be empty.");

        Id = id;
        UserId = userId;
    }

    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public IReadOnlyCollection<CartItem> Items => _items;

    public decimal TotalAmount => _items.Sum(x => x.TotalPrice);

    public void AddItem(
        Guid productId,
        string productName,
        decimal unitPrice,
        int quantity)
    {
        if (productId == Guid.Empty)
            throw new DomainException("Product id cannot be empty.");

        if (string.IsNullOrWhiteSpace(productName))
            throw new DomainException("Product name cannot be empty.");

        if (unitPrice < 0)
            throw new DomainException("Unit price cannot be negative.");

        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than zero.");

        var existingItem = _items.FirstOrDefault(x => x.ProductId == productId);

        if (existingItem is not null)
        {
            existingItem.IncreaseQuantity(quantity);
            return;
        }

        _items.Add(new CartItem(
            Guid.NewGuid(),
            Id,
            productId,
            productName,
            unitPrice,
            quantity));
    }

    public bool RemoveItem(Guid productId)
    {
        var item = _items.FirstOrDefault(x => x.ProductId == productId);

        if (item is null)
            return false;

        _items.Remove(item);

        return true;
    }

    public void Clear()
    {
        _items.Clear();
    }
}