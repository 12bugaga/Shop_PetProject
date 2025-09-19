namespace Shop.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public string CategoryName { get; private set; }
    public Product() {}

    public Product(string categoryName, string name, decimal price)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        CategoryName = categoryName;
    }
}