namespace Shop.Infrastructure.Repositories.Client.Interfaces;

public interface IClientCommandRepository
{
    Task CreateClientAsync(Domain.Entities.Client client);
}