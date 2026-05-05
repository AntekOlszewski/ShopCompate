namespace ShopCompare.Catalog.Api.Domain;

public sealed class Category
{
    private readonly List<Product> _products = new();

    private Category()
    {
    }

    public Category(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public IReadOnlyCollection<Product> Products => _products;
}