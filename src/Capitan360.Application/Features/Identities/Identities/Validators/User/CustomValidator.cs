using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Application.Features.Identities.Identities.Validators.User;

public class CustomUserValidator : UserValidator<Domain.Entities.Identities.User>
{
    private readonly IMultiTenantContextAccessor<TenantInfo> _tenantAccessor;

    public CustomUserValidator(IdentityErrorDescriber errors, IMultiTenantContextAccessor<TenantInfo> tenantAccessor) 
        : base(errors)
    {
        _tenantAccessor = tenantAccessor;
    }

    public override async Task<IdentityResult> ValidateAsync(UserManager<Domain.Entities.Identities.User> manager, Domain.Entities.Identities.User user)
    {
        // Get current company ID from tenant context
        var tenantInfo = _tenantAccessor.MultiTenantContext?.TenantInfo;
        int? companyId = null;
        if (tenantInfo != null && !string.IsNullOrEmpty(tenantInfo.Id))
        {
            if (int.TryParse(tenantInfo.Id, out var parsedCompanyId))
                companyId = parsedCompanyId;
        }

        // If company ID is available, check for duplicate username within the same company
        if (companyId.HasValue && !string.IsNullOrEmpty(user.NormalizedUserName))
        {
            var existingUser = await manager.Users
                .FirstOrDefaultAsync(u => 
                    u.NormalizedUserName == user.NormalizedUserName && 
                    u.CompanyId == companyId.Value && 
                    u.Id != user.Id);

            if (existingUser != null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "DuplicateUserName",
                    Description = "این نام کاربری قبلاً در این شرکت استفاده شده است."
                });
            }
        }

        // Run base validation but filter out DuplicateUserName errors (we handle it above)
        var baseResult = await base.ValidateAsync(manager, user);

        if (!baseResult.Succeeded)
        {
            var filteredErrors = baseResult.Errors.Where(e => e.Code != "DuplicateUserName").ToList();
            return filteredErrors.Any() ? IdentityResult.Failed(filteredErrors.ToArray()) : IdentityResult.Success;
        }

        return IdentityResult.Success;
    }
}