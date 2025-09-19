using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Infrastructure.DbContexts;
using Shop.Infrastructure.Repositories.Products.Interfaces;

namespace Shop.Infrastructure.Repositories.Products.Repositories;

public class ProductRepository : IProductQueryRepository, IProductCommandRepository
{
    private readonly ShopDbContext _dbContext;

    public ProductRepository(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    #region IProductCommandRepository
    
    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _dbContext.Products.AddAsync(product, cancellationToken);
    }
    #endregion

    #region IProductQueryRepository

    public async Task<List<Product>> GetCategoryProductsAsync(string categoryName, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products.Where(a => a.CategoryName == categoryName).ToListAsync();
    }

    #endregion
}