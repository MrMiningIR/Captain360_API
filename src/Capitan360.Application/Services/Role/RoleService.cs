using Capitan360.Application.Common;
using Capitan360.Application.Services.Role.Commands;
using Capitan360.Application.Services.Role.Commands.CreateRole;
using Capitan360.Application.Services.Role.Commands.UpdateQuery;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Repositories.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.Role;

public class RoleService(RoleManager<Domain.Entities.AuthorizationEntity.Role> roleManager,
    ILogger<RoleService> logger, IIdentityRepository identityRepository, IUnitOfWork unitOfWork) : IRoleService
{
    public async Task<ApiResponse<string>> CreateRole(CreateRoleCommand command)
    {
        logger.LogInformation("CreateRole is Called with {@command}", command);

        var exists = await roleManager.RoleExistsAsync(command.RoleName);
        if (exists)
            return ApiResponse<string>.Error(400, "نقشی با این نام در سیستم وجود دارد");

        var newRole = new Domain.Entities.AuthorizationEntity.Role
        {
            PersianName = command.PersianName,
            Name = command.RoleName,
            NormalizedName = command.RoleName.ToUpper()
        };

        var result = await roleManager.CreateAsync(newRole);
        if (result is { Succeeded: false })
            return ApiResponse<string>.Error(400, string.Join(", ", result.Errors.Select(x => x.Description)));

        return ApiResponse<string>.Ok(newRole.Id);
    }

    public async Task<ApiResponse<string>> DeleteRole(DeleteRoleCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateRole is Called with {@command}", command);
        var role = await roleManager.FindByIdAsync(command.RoleId);
        if (role is null)
            return ApiResponse<string>.Error(400, "این نقش در سیستم وجود ندارد");

        identityRepository.DeleteRole(role);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<string>.Ok(command.RoleId);
    }

    public async Task<ApiResponse<string>> UpdateRole(UpdateRoleCommand command)
    {
        logger.LogInformation("CreateRole is Called with {@command}", command);
        var role = await roleManager.FindByIdAsync(command.RoleId);
        if (role is null)
            return ApiResponse<string>.Error(400, "این نقش در سیستم وجود ندارد");

        role.PersianName = command.RolePersianName;
        role.Name = command.RoleName;
        role.NormalizedName = command.RoleName.ToUpper();

        var result = await roleManager.UpdateAsync(role);
        if (result is { Succeeded: false })
            return ApiResponse<string>.Error(400, string.Join(", ", result.Errors.Select(x => x.Description)));

        return ApiResponse<string>.Ok(command.RoleId);
    }


}