using MediatR;
using Grpc.Core;
using Shop.Application.Client.Commands;
using Shop.ClientService.Protos;

namespace Shop.ClientService.Services;

public class ClientServiceGrpc : ClientService.ClientServiceBase
{
    private readonly IMediator _mediator;

    public ClientServiceGrpc(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<CreateClientResponse> CreateClient(CreateClientRequest request, ServerCallContext context)
    {
        var clientId = await _mediator.Send(new CreateClientCommand(request.Name, request.Password, request.Email));

        return new CreateClientResponse
        {
            Id = clientId,
            Status = "Created"
        };
    }
}