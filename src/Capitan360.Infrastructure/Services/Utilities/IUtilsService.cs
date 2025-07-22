namespace Capitan360.Infrastructure.Services.Utilities;

public interface IUtilsService
{
    Task<bool> CheckTableExistsAsync(string tableName, CancellationToken ct);
}