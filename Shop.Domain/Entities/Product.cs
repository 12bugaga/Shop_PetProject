using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Entities;

[Table("Products", Schema = "product")]
public class Product : BaseEntity
{
    [Required]
    [Column(TypeName = "character varying(200)")]
    public string Name { get; private set; }
    
    [Required]
    [Column(TypeName = "numeric(18,2)")]
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