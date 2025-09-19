using System.ComponentModel.DataAnnotations;

namespace Shop.Domain.Entities;

public class BaseEntity
{
    [Key]
    public virtual Guid Id { get; set; }
}