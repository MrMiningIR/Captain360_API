using Capitan360.Domain.Entities.Identities;
using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Capitan360.Infrastructure.Repositories.Identities;

public class CompanyAwareUserManager : UserManager<User>
{
    private readonly IMultiTenantContextAccessor<TenantInfo> _tenantAccessor;

    public CompanyAwareUserManager(
        IUserStore<User> store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<User> passwordHasher,
        IEnumerable<IUserValidator<User>> userValidators,
        IEnumerable<IPasswordValidator<User>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<UserManager<User>> logger,
        IMultiTenantContextAccessor<TenantInfo> tenantAccessor)
        : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
    {
        _tenantAccessor = tenantAccessor;
    }

    public override Task<User?> FindByNameAsync(string userName)
    {
        var normalizedName = NormalizeName(userName);

        // Use CompanyAwareUserStore which handles tenant-aware lookup
        return Store is CompanyAwareUserStore companyStore
            ? companyStore.FindByNameAsync(normalizedName, CancellationToken.None)
            : base.FindByNameAsync(userName);
    }

    public override Task<User?> FindByEmailAsync(string email)
    {
        var normalizedEmail = NormalizeEmail(email);

        // Use CompanyAwareUserStore which handles tenant-aware lookup
        return Store is CompanyAwareUserStore companyStore
            ? companyStore.FindByEmailAsync(normalizedEmail, CancellationToken.None)
            : base.FindByEmailAsync(email);
    }
}

