using Shop.Domain.Entities;

namespace Shop.Infrastructure.Repositories.Products.Interfaces;

public interface IProductQueryRepository
{
    Task<List<Product>> GetCategoryProductsAsync(string categoryName, CancellationToken cancellationToken = default);
}