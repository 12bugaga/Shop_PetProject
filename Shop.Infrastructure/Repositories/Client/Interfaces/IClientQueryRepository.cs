namespace Shop.Infrastructure.Repositories.Client.Interfaces;

public interface IClientQueryRepository
{
    Task<Domain.Entities.Client> GetClientByIdAsync(Guid clientId);
}