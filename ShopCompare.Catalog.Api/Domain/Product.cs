namespace ShopCompare.Catalog.Api.Domain;

public sealed class Product
{
    private Product()
    {
    }

    public Product(
        Guid id,
        string name,
        string description,
        decimal price,
        Guid categoryId,
        bool isActive = true)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        CategoryId = categoryId;
        IsActive = isActive;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public decimal Price { get; private set; }

    public Guid CategoryId { get; private set; }

    public Category Category { get; private set; } = null!;

    public bool IsActive { get; private set; }

    public void Deactivate()
    {
        IsActive = false;
    }
}