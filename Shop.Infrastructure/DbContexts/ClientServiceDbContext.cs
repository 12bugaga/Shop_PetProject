using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;

namespace Shop.ClientService.Infrastructure;

public class ClientServiceDbContext : DbContext
{
    public ClientServiceDbContext(DbContextOptions<ClientServiceDbContext> options) : base(options)
    {
    }
    
    public DbSet<Client> Clients { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}