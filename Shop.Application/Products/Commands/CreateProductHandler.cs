using MediatR;
using Shop.Domain.Entities;
using Shop.Infrastructure.Repositories.Products.Interfaces;

namespace Shop.Application.Products.Commands;

public record CreateProductCommand(string Name, decimal Price, string CategoryName) : IRequest<Guid>; 

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductCommandRepository _productRepository;

    public CreateProductHandler(IProductCommandRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Guid> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product(command.CategoryName, command.Name, command.Price);
        await _productRepository.AddAsync(product);
        return product.Id;
    }
}