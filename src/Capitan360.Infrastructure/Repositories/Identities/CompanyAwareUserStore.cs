using Capitan360.Domain.Entities.Identities;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Capitan360.Infrastructure.Persistence;
using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Capitan360.Infrastructure.Repositories.Identities;

public class CompanyAwareUserStore : UserStore<User, Role, ApplicationDbContext, string>
{
    private readonly IMultiTenantContextAccessor<TenantInfo> _tenantAccessor;
    private readonly ApplicationDbContext _dbContext;
    private readonly ICompanyUriRepository? _companyUriRepository;

    public CompanyAwareUserStore(
        ApplicationDbContext context,
        IdentityErrorDescriber describer,
        IMultiTenantContextAccessor<TenantInfo> tenantAccessor,
        IServiceProvider serviceProvider)
        : base(context, describer)
    {
        _tenantAccessor = tenantAccessor;
        _dbContext = context;
        // Try to get CompanyUriRepository from service provider (may not be available in all contexts)
        _companyUriRepository = serviceProvider.GetService<ICompanyUriRepository>();
    }

    private async Task<int?> GetCurrentCompanyIdAsync(CancellationToken cancellationToken = default)
    {
        var tenantInfo = _tenantAccessor.MultiTenantContext?.TenantInfo;
        if (tenantInfo == null || string.IsNullOrEmpty(tenantInfo.Identifier))
            return null;

        // First try to parse tenantInfo.Id as CompanyId
        if (!string.IsNullOrEmpty(tenantInfo.Id) && int.TryParse(tenantInfo.Id, out var companyId))
            return companyId;

        // If not, try to get CompanyId from URI
        if (_companyUriRepository != null && !string.IsNullOrEmpty(tenantInfo.Identifier))
        {
            var companyUri = await _companyUriRepository.GetUriByTenant(tenantInfo.Identifier, cancellationToken);
            if (companyUri != null)
                return companyUri.CompanyId;
        }

        return null;
    }

    public override async Task<User?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default)
    {
        var companyId = await GetCurrentCompanyIdAsync(cancellationToken);
        if (companyId == null)
            return await base.FindByNameAsync(normalizedUserName, cancellationToken);

        return await Users.SingleOrDefaultAsync(
            u => u.NormalizedUserName == normalizedUserName && u.CompanyId == companyId.Value,
            cancellationToken);
    }

    public override async Task<User?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default)
    {
        var companyId = await GetCurrentCompanyIdAsync(cancellationToken);
        if (companyId == null)
            return await base.FindByEmailAsync(normalizedEmail, cancellationToken);

        return await Users.SingleOrDefaultAsync(
            u => u.NormalizedEmail == normalizedEmail && u.CompanyId == companyId.Value,
            cancellationToken);
    }

    public override IQueryable<User> Users => base.Users;
}

