using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Shop.ClientService.Infrastructure;

namespace Shop.Infrastructure.DbContexts.Factories;

public class ClientServiceDbContextMigration : IDesignTimeDbContextFactory<ClientServiceDbContext>
{
    public ClientServiceDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ClientServiceDbContext>();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseNpgsql(connectionString, b => b.MigrationsAssembly("Shop.Infrastructure"));

        return new ClientServiceDbContext(optionsBuilder.Options);
    }
}