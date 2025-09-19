using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.DbContexts;

public class ShopDbContext : DbContext
{
    public ShopDbContext(DbContextOptions<ShopDbContext> options)
        : base(options)
    {
    }
    
#region DbSets
    
    public DbSet<Product> Products { get; set; }
    
#endregion
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuration by Fluent API
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
        });
    }
}