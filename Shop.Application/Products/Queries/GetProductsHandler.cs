using MediatR;
using Shop.Infrastructure.Repositories.Products.Interfaces;

namespace Shop.Application.Products.Queries;

public class GetProductsHandler
{
    public record GetProductsQuery(string CategoryName) : IRequest<IEnumerable<ProductDto>>;

    private readonly IProductQueryRepository _productRepository;
    
    public GetProductsHandler(IProductQueryRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var result = (await _productRepository.GetCategoryProductsAsync(request.CategoryName)).Select(a => new ProductDto(a.Id, a.Name, a.Price, a.CategoryName));
        return result;
    }
    public record ProductDto(Guid Id, string Name, decimal Price, string CategoryName);
}