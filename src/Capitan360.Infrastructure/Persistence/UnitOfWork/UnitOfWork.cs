namespace Capitan360.Infrastructure.Persistence.UnitOfWork;

//public class UnitOfWork 
//{
//    private readonly ApplicationDbContext _dbContext;
//    private readonly ILogger<UnitOfWork> _logger;
//    private IDbContextTransaction? _currentTransaction;
//    private bool _disposed;



//    public UnitOfWork(ApplicationDbContext dbContext,  ILogger<UnitOfWork> logger)
//    {


//        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
//        _logger = logger;

//    }
//    // ICompanyRepository Companies { get; }
//    // IAreaRepository Areas { get; }
//    // must in both I and U



//    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
//    {
//        // Adding ShadowProperties , or Logging

//        try
//        {

//            var result = await _dbContext.SaveChangesAsync(cancellationToken);

//            return result;
//        }

//        catch (Exception exception)
//        {


//            throw exception switch
//            {
//                DbUpdateConcurrencyException => new ConcurrencyException("Concurrency exception occurred.", exception),
//                DbUpdateException => new DbUpdateException("Database update exception occurred.", exception),
//                SqlException => new Exception("An error occurred while saving changes.", exception),
//                _ => new Exception("An error occurred while saving changes.", exception)
//            };
//        }


//    }

//    // Stating New Transaction , and Throwing Error if there is already a transaction!
//    public async Task BeginTransactionAsync(CancellationToken cancellationToken = new())
//    {
//        if (_currentTransaction != null)
//        {
//            throw new InvalidOperationException("A transaction is already in progress.");
//        }

//        _currentTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
//    }

//    public async Task CommitTransactionAsync(CancellationToken cancellationToken = new())
//    {
//        if (_currentTransaction == null)
//        {
//            throw new InvalidOperationException("No active transaction to commit.");
//        }

//        try
//        {
//            await _dbContext.SaveChangesAsync(cancellationToken);
//            await _currentTransaction.CommitAsync(cancellationToken);
//        }
//        catch (Exception exception)
//        {
//            await RollbackTransactionAsync(cancellationToken);

//            throw exception switch
//            {
//                DbUpdateConcurrencyException => new ConcurrencyException("Concurrency exception occurred.", exception),
//                DbUpdateException => new DbUpdateException("Database update exception occurred.", exception),
//                SqlException => new Exception("An error occurred while saving changes.", exception),
//                _ => new Exception("An error occurred while saving changes.", exception)
//            };
//        }
//        finally
//        {
//            _currentTransaction.Dispose();
//            _currentTransaction = null;
//        }
//    }

//    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = new())
//    {
//        if (_currentTransaction == null)
//        {
//            return;
//        }

//        try
//        {
//            await _currentTransaction.RollbackAsync(cancellationToken);
//        }
//        catch (Exception exception)
//        {
//            throw new Exception("An error occurred while rolling back changes.", exception);
//        }
//        finally
//        {
//            _currentTransaction.Dispose();
//            _currentTransaction = null;
//        }
//    }

//    public bool HasActiveTransaction => _currentTransaction != null;

//    public void Dispose()
//    {
//        Dispose(true);
//        GC.SuppressFinalize(this);
//    }

//    protected virtual void Dispose(bool disposing)
//    {
//        if (_disposed)
//        {
//            return;
//        }

//        if (disposing)
//        {
//            if (_currentTransaction != null)
//            {
//                _currentTransaction.Dispose();
//                _currentTransaction = null;
//            }

//            _dbContext.Dispose();
//        }

//        _disposed = true;
//    }

//    ~UnitOfWork()
//    {
//        Dispose(false);
//    }
//}