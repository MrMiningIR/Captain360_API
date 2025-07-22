

using Capitan360.Domain.Repositories.AddressRepo;
using Capitan360.Domain.Repositories.CompanyRepo;

namespace Capitan360.Domain.Abstractions;

public interface IUnitOfWork  : IDisposable 
{
    
   
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
    Task BeginTransactionAsync(CancellationToken cancellationToken = new());
    Task CommitTransactionAsync(CancellationToken cancellationToken = new());
    Task RollbackTransactionAsync(CancellationToken cancellationToken = new());
    bool HasActiveTransaction { get; }

}