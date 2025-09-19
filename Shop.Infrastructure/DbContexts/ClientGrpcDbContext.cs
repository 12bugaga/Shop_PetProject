using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;

namespace Shop.ClientService.Infrastructure;

public class ClientGrpcDbContext : DbContext
{
    public ClientGrpcDbContext(DbContextOptions<ClientGrpcDbContext> options) : base(options)
    {
    }
    
    public DbSet<Client> Clients { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}