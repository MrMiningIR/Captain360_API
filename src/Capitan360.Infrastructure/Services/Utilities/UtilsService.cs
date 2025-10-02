using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Services.Utilities;

public class UtilsService(ApplicationDbContext dbContext) : IUtilsService
{
    public async Task<bool> CheckTableExistsAsync(string tableName, CancellationToken cancellationToken)
    {
        try
        {
            var connection = dbContext.Database.GetDbConnection();
            await connection.OpenAsync(cancellationToken);
            await using var command = connection.CreateCommand();

            command.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @tableName";
            var parameter = command.CreateParameter();
            parameter.ParameterName = "@tableName";
            parameter.Value = tableName;
            command.Parameters.Add(parameter);

            var result = await command.ExecuteScalarAsync(cancellationToken);
            return Convert.ToInt32(result) > 0;
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Error checking table existence: {ex.Message}");
            return false;
        }
        finally
        {
            if (dbContext.Database.GetDbConnection().State == System.Data.ConnectionState.Open)
            {
                await dbContext.Database.CloseConnectionAsync();
            }
        }
    }
}