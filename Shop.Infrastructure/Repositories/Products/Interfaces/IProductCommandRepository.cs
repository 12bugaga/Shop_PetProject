using Shop.Domain.Entities;

namespace Shop.Infrastructure.Repositories.Products.Interfaces;

public interface IProductCommandRepository
{
    Task AddAsync(Product product, CancellationToken cancellationToken = default);
}