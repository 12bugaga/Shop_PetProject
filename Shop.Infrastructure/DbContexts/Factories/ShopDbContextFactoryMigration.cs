using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Shop.Infrastructure.DbContexts.Factories;

public class ShopDbContextFactoryMigration : IDesignTimeDbContextFactory<ShopAPIDbContext>
{
    public ShopAPIDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ShopAPIDbContext>();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseNpgsql(connectionString, b => b.MigrationsAssembly("Shop.Infrastructure"));

        return new ShopAPIDbContext(optionsBuilder.Options);
    }
}