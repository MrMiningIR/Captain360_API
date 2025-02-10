

namespace Capitan360.Domain.Abstractions;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken  cancellationToken=default);

}