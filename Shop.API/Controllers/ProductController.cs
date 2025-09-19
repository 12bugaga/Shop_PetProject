using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Products.Commands;
using Shop.Application.Products.Queries;

namespace Shop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string categoryName, CancellationToken cancellationToken)
    {
        var products = await _mediator.Send(new GetProductsHandler.GetProductsQuery(categoryName));
        return Ok(products);
    }
}