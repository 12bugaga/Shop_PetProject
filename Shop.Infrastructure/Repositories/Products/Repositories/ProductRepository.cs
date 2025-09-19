using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Infrastructure.DbContexts;
using Shop.Infrastructure.Repositories.Products.Interfaces;

namespace Shop.Infrastructure.Repositories.Products.Repositories;

public class ProductRepository : IProductQueryRepository, IProductCommandRepository
{
    private readonly ShopAPIDbContext _apiDbContext;

    public ProductRepository(ShopAPIDbContext apiDbContext)
    {
        _apiDbContext = apiDbContext;
    }
    
    #region IProductCommandRepository
    
    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _apiDbContext.Products.AddAsync(product, cancellationToken);
    }
    #endregion

    #region IProductQueryRepository

    public async Task<List<Product>> GetCategoryProductsAsync(string categoryName, CancellationToken cancellationToken = default)
    {
        return await _apiDbContext.Products.Where(a => a.CategoryName == categoryName).ToListAsync();
    }

    #endregion
}