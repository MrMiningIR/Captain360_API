using Microsoft.AspNetCore.Identity;

namespace Capitan360.Application.Services.Identity.Validators.User;

public class CustomUserValidator : UserValidator<Domain.Entities.UserEntity.User>
{
    public CustomUserValidator(IdentityErrorDescriber errors = null) : base(errors)
    {
    }

    public override async Task<IdentityResult> ValidateAsync(UserManager<Domain.Entities.UserEntity.User> manager, Domain.Entities.UserEntity.User user)
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