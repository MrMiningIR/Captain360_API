using Microsoft.AspNetCore.Identity;

namespace Capitan360.Application.Features.Identities.Identities.Validators.User;

public class CustomUserValidator : UserValidator<Domain.Entities.Identities.User>
{
    public CustomUserValidator(IdentityErrorDescriber errors = null) : base(errors)
    {
    }

    public override async Task<IdentityResult> ValidateAsync(UserManager<Domain.Entities.Identities.User> manager, Domain.Entities.Identities.User user)
    {

        var baseResult = await base.ValidateAsync(manager, user);


        if (!baseResult.Succeeded)
        {
            var filteredErrors = baseResult.Errors.Where(e => e.Code != "DuplicateUserName").ToList();
            return filteredErrors.Any() ? IdentityResult.Failed(filteredErrors.ToArray()) : IdentityResult.Success;
        }

        return IdentityResult.Success;
    }
}