using System.Threading;
using System.Threading.Tasks;

namespace Sharpee.Framework.DomainModel.Data;

/// <summary>
/// Unit of work in order to encapsulate a transaction.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Starts a new transaction.
    /// </summary>
    void BeginTransaction();

    /// <summary>
    /// Starts a new transaction
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Commits changes into database.
    /// </summary>
    void Commit();

    /// <summary>
    /// Commits changes into database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Rollbacks transaction.
    /// </summary>
    void Rollback();

    /// <summary>
    /// Rollbacks transaction.
    /// </summary>
    Task RollbackAsync(CancellationToken cancellationToken = default);
}