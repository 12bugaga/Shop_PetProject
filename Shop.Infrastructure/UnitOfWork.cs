using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Shop.Infrastructure.DbContexts;

namespace Shop.Infrastructure;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    IDbTransaction BeginTransaction(CancellationToken cancellationToken = default, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
}

public class UnitOfWork : IUnitOfWork
{
    private readonly ShopAPIDbContext _context;

    public UnitOfWork(ShopAPIDbContext context)
    {
        _context = context;
    }
    
    public IDbTransaction? Transaction { get; private set; }
    public IDbTransaction BeginTransaction(CancellationToken cancellationToken = default, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        _context.Database.BeginTransaction(isolationLevel);
        Transaction = _context.Database.CurrentTransaction.GetDbTransaction();

        return Transaction;
    }
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}