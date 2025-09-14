
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.AddressEntity;
using Capitan360.Domain.Entities.AuthorizationEntity;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Entities.ContentEntity;
using Capitan360.Domain.Entities.DomesticWaybillEntity;
using Capitan360.Domain.Entities.PackageEntity;
using Capitan360.Domain.Entities.UserEntity;
using Capitan360.Domain.Exceptions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Capitan360.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) //Todo : Must Change to Internal
    : IdentityDbContext<User, Role, string>(options), IUnitOfWork
{
    private IDbContextTransaction? _currentTransaction;


    public DbSet<Company> Companies { get; set; }
    public DbSet<CompanyType> CompanyTypes { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<GroupPermission> GroupPermissions { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserPermission> UserPermissions { get; set; }
    public DbSet<RoleGroup> RoleGroups { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<TokenBlacklist> TokenBlacklists { get; set; }
    public DbSet<UserCompany> UserCompanies { get; set; }
    public DbSet<CompanyUri> CompanyUris { get; set; }

    public DbSet<Address> Addresses { get; set; }
    public DbSet<Area> Areas { get; set; }
    // public DbSet<CompanyAddress> CompanyAddresses { get; set; }

    public DbSet<CompanyCommissions> CompanyCommissions { get; set; }
    public DbSet<CompanyBank> CompanyBanks { get; set; }
    public DbSet<CompanyPreferences> CompanyPreferences { get; set; }
    public DbSet<CompanySmsPatterns> CompanySmsPatterns { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public DbSet<ContentType> ContentTypes { get; set; }
    public DbSet<PackageType> PackageTypes { get; set; }
    public DbSet<CompanyContentType> CompanyContentTypes { get; set; }
    public DbSet<CompanyPackageType> CompanyPackageTypes { get; set; }

    public DbSet<CompanyDomesticPaths> CompanyDomesticPaths { get; set; }

    public DbSet<CompanyDomesticPathStructPrices> CompanyDomesticPathStructPrices { get; set; }
    public DbSet<CompanyDomesticPathStructPriceMunicipalAreas> CompanyDomesticPathStructPriceMunicipalAreas { get; set; }

    public DbSet<CompanyDomesticPathCharge> CompanyDomesticPathCharges { get; set; }
    public DbSet<CompanyDomesticPathChargeContentType> CompanyDomesticPathChargeContentTypes { get; set; }



    public DbSet<CompanyInsurance> CompanyInsurances { get; set; }
    public DbSet<CompanyInsuranceCharge> CompanyInsuranceCharges { get; set; }
    public DbSet<CompanyInsuranceChargePayment> CompanyInsurancesChargePayments { get; set; }
    public DbSet<CompanyInsuranceChargePaymentContentType> CompanyInsurancesChargePaymentContentTypes { get; set; }

    public DbSet<UserPermissionVersionControl> UserPermissionVersionControls { get; set; }


    public DbSet<DomesticWaybill> DomesticWaybills { get; set; }
    public DbSet<DomesticWaybillPackageType> DomesticWaybillPackageTypes { get; set; }
    public DbSet<DomesticWaybillPeriod> DomesticWaybillPeriods { get; set; }
    public DbSet<ManifestForm> ManifestForms { get; set; }
    public DbSet<ManifestFormPeriod> ManifestFormPeriods { get; set; }





    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);

        // Add All Configurations
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Add Shadow Properties for all Entities
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var entityBuilder = builder.Entity(entityType.ClrType);

            // if we use Sql Server otherwise we need to change this


            entityBuilder.Property<DateTime>("CreatedDate").HasDefaultValueSql("GETUTCDATE()");
            entityBuilder.Property<DateTime?>("UpdatedDate").IsRequired(false);
            entityBuilder.Property<bool>("Deleted").HasDefaultValue(false);
            entityBuilder.Property<string?>("CreatedBy").HasMaxLength(100);

            entityBuilder.Property<string?>("UpdatedBy").HasMaxLength(100);

            // اعمال Query Filter بعد از تعریف Shadow Property
            // اعمال Query Filter با نوع پویا
            var parameter = Expression.Parameter(entityType.ClrType, "e");
            var deletedProperty = Expression.Call(
                typeof(EF),
                nameof(EF.Property),
                new[] { typeof(bool) },
                parameter,
                Expression.Constant("Deleted")
            );
            var notDeleted = Expression.Not(deletedProperty);
            var lambda = Expression.Lambda(notDeleted, parameter);

            entityBuilder.HasQueryFilter(lambda);

        }

    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        // Adding ShadowProperties , or Logging(we have a middleware so here we don't use logger)

        try
        {


            foreach (var entry in ChangeTracker.Entries()
                         .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedDate").CurrentValue = DateTime.UtcNow;
                    entry.Property("CreatedBy").CurrentValue ??= "System"; //TOD
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("UpdatedDate").CurrentValue = DateTime.UtcNow;
                    entry.Property("UpdatedBy").CurrentValue ??= "System"; //TODO
                }
            }

            // اگه UserId رو از یه کانتکست (مثل IHttpContextAccessor) می‌خوای بگیری، باید اون رو به ApplicationDbContext تزریق کنی.

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        catch (Exception exception)
        {


            throw exception switch
            {
                DbUpdateConcurrencyException => new ConcurrencyException("Concurrency exception occurred.", exception),
                DbUpdateException => new DbUpdateException("Database update exception occurred.", exception),
                SqlException => new Exception("An error occurred while saving changes.", exception),
                _ => new Exception("An error occurred while saving changes.", exception)
            };
        }


    }

    // Stating New Transaction , and Throwing Error if there is already a transaction!
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = new())
    {
        if (_currentTransaction != null)
        {
            throw new InvalidOperationException("A transaction is already in progress.");
        }

        _currentTransaction = await base.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        if (_currentTransaction == null)
        {
            throw new InvalidOperationException("No active transaction to commit.");
        }

        try
        {
            await base.SaveChangesAsync(cancellationToken);
            await _currentTransaction.CommitAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            await RollbackTransactionAsync(cancellationToken);

            throw exception switch
            {
                DbUpdateConcurrencyException => new ConcurrencyException("Concurrency exception occurred.", exception),
                DbUpdateException => new DbUpdateException("Database update exception occurred.", exception),
                SqlException => new Exception("An error occurred while saving changes.", exception),
                _ => new Exception("An error occurred while saving changes.", exception)
            };
        }
        finally
        {
            _currentTransaction.Dispose();
            _currentTransaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = new())
    {
        if (_currentTransaction == null)
        {
            return;
        }

        try
        {
            await _currentTransaction.RollbackAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            throw new Exception("An error occurred while rolling back changes.", exception);
        }
        finally
        {
            _currentTransaction.Dispose();
            _currentTransaction = null;
        }
    }

    public bool HasActiveTransaction => _currentTransaction != null;

    public override void Dispose()
    {
        if (_currentTransaction != null)
        {
            _currentTransaction.Dispose();
            _currentTransaction = null;
        }
        base.Dispose();
    }
}
