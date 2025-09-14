namespace Capitan360.Domain.Interfaces;

public interface IUnitOfWork  : IDisposable 
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());

    Task BeginTransactionAsync(CancellationToken cancellationToken = new());
    
    Task CommitTransactionAsync(CancellationToken cancellationToken = new());
    
    Task RollbackTransactionAsync(CancellationToken cancellationToken = new());
    
    bool HasActiveTransaction { get; }
}