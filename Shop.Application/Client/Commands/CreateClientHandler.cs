using MediatR;
using Shop.Infrastructure.Repositories.Client.Interfaces;

namespace Shop.Application.Client.Commands;

public record CreateClientCommand(string Name, string Email, string Password) : IRequest<string>; 

public class CreateClientHandler : IRequestHandler<CreateClientCommand, string>
{
    private readonly IClientCommandRepository _clientRepository;

    public CreateClientHandler(IClientCommandRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<string> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var client = new Domain.Entities.Client(request.Name, request.Password, request.Email);
        await _clientRepository.CreateClientAsync(client);
        return client.Id.ToString();
    }
}