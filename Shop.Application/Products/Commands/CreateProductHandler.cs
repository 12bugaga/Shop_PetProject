using MediatR;
using Shop.Domain.Entities;

namespace Shop.Application.Products.Commands;

public record CreateProductCommand(string Name, decimal Price) : IRequest<Guid>; 

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;

    public CreateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Guid> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product(command.Name, command.Price);
        await _productRepository.Add(product);
        return product.Id;
    }
}