using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.DbContexts;

public class ShopAPIDbContext : DbContext
{
    public ShopAPIDbContext(DbContextOptions<ShopAPIDbContext> options)
        : base(options)
    {
    }
    
#region DbSets
    
    public DbSet<Product> Products { get; set; }
    
#endregion
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}