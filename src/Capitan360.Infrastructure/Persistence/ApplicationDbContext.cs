
using Capitan360.Domain.Entities.AuthorizationEntity;
using Capitan360.Domain.Entities.UserEntity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Abstractions;
using System.Data;
using Capitan360.Domain.Entities.AddressEntity;
using Capitan360.Domain.Exceptions;

namespace Capitan360.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) //Todo : Must Change to Internal
    : IdentityDbContext<User, Role, string>(options), IUnitOfWork
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<GroupPermission> GroupPermissions { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RoleGroup> RoleGroups { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<TokenBlacklist> TokenBlacklists { get; set; }
    public DbSet<UserCompany> UserCompanies { get; set; }
    public DbSet<CompanyUri> CompanyUris { get; set; }

    public DbSet<Address> Addresses { get; set; }
    public DbSet<Area> Areas { get; set; }
    public DbSet<CompanyAddress> CompanyAddresses { get; set; }

    public DbSet<CompanyCommissions> CompanyCommissions { get; set; }
    public DbSet<CompanyPreferences> CompanyPreferences { get; set; }
    public DbSet<CompanySmsPatterns> CompanySmsPatterns { get; set; }

    public DbSet<RefreshToken?> RefreshTokens { get; set; }



    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);

        // Add All Configurations
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Add Shadow Properties for all Entities
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var entityBuilder = builder.Entity(entityType.ClrType);

            entityBuilder.Property<DateTime>("CreatedDate").HasDefaultValueSql("GETUTCDATE()");
            entityBuilder.Property<DateTime?>("UpdatedDate").IsRequired(false);
            entityBuilder.Property<bool>("Deleted").HasDefaultValue(false);
        }
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        try
        {

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency exception occurred.", ex);

        }
    }


}
