using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Shop.Infrastructure;

namespace Shop.Application.Behaviors;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if(request is not ICommand)
            return await next();
        
        using var transaction = _unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            var response = await next();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            transaction.Commit();

            return response;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}