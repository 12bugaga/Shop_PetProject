using Microsoft.EntityFrameworkCore;
using Shop.ClientService.Infrastructure;
using Shop.Infrastructure.Repositories.Client.Interfaces;

namespace Shop.Infrastructure.Repositories.Client.Repositories;

public class ClientRepository : IClientCommandRepository, IClientQueryRepository
{
    private readonly ClientGrpcDbContext _context;

    public ClientRepository(ClientGrpcDbContext context)
    {
        _context = context;
    }

    public async Task CreateClientAsync(Domain.Entities.Client client)
    {
        await _context.Clients.AddAsync(client);
    }

    public async Task<Domain.Entities.Client> GetClientByIdAsync(Guid clientId)
    {
        return await _context.Clients.SingleAsync(c => c.Id == clientId);
    }
}