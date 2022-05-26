using System.Threading;
using System.Threading.Tasks;

namespace Sharpee.Framework.DomainModel.Data;

public sealed class VoidUnitOfWork : IUnitOfWork
{
    public void BeginTransaction()
    { }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }

    public void Commit()
    { }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }

    public void Rollback()
    { }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }
}
