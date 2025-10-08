using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Application.Features.Identities.Roles.Roles.Commands.Create;
using Capitan360.Application.Features.Identities.Roles.Roles.Commands.Delete;
using Capitan360.Application.Features.Identities.Roles.Roles.Commands.Update;
using Capitan360.Application.Features.Identities.Roles.Roles.Dtos;
using Capitan360.Application.Features.Identities.Roles.Roles.Queries.GetAll;
using Capitan360.Application.Features.Identities.Roles.Roles.Queries.GetById;
using Capitan360.Domain.Entities.Identities;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Identities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Features.Identities.Roles.Roles.Services;

public class RoleService(
    ILogger<RoleService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    IRoleRepository roleRepository
) : IRoleService
{
    public async Task<ApiResponse<IdentityResult>> CreateRoleAsync(CreateRoleCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateRole is Called with {@CreateRoleCommand}", command);

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<IdentityResult>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin())
            return ApiResponse<IdentityResult>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (await roleRepository.CheckExistRoleNameAsync(command.Name, null, cancellationToken))
            return ApiResponse<IdentityResult>.Error(StatusCodes.Status409Conflict, "نام نقش تکراری است");

        if (await roleRepository.CheckExistRolePersianNameAsync(command.PersianName, null, cancellationToken))
            return ApiResponse<IdentityResult>.Error(StatusCodes.Status409Conflict, "نام فارسی نقش تکراری است");

        var role = mapper.Map<Role>(command) ?? null;
        if (role == null)
            return ApiResponse<IdentityResult>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        var identityResult = await roleRepository.CreateRoleAsync(role, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Role created successfully with {@Role}", role);
        return ApiResponse<IdentityResult>.Created(identityResult, "نقش با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<string>> DeleteRoleAsync(DeleteRoleCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteRole is Called with {@Id}", command.Id);

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<string>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin())
            return ApiResponse<string>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var role = await roleRepository.GetRoleByIdAsync(command.Id, true, false, cancellationToken);
        if (role is null)
            return ApiResponse<string>.Error(StatusCodes.Status404NotFound, "نقش نامعتبر است");

        await roleRepository.DeleteRoleAsync(role.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Role Deleted successfully with {@Id}", command.Id);
        return ApiResponse<string>.Ok(command.Id, "نقش با موفقیت حذف شد");
    }

    public async Task<ApiResponse<RoleDto>> UpdateRoleAsync(UpdateRoleCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateRole is Called with {@UpdateRoleCommand}", command);

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<RoleDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin())
            return ApiResponse<RoleDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var role = await roleRepository.GetRoleByIdAsync(command.Id, false, true, cancellationToken);
        if (role is null)
            return ApiResponse<RoleDto>.Error(StatusCodes.Status404NotFound, "نقش نامعتبر است");

        if (await roleRepository.CheckExistRoleNameAsync(command.Name, command.Id, cancellationToken))
            return ApiResponse<RoleDto>.Error(StatusCodes.Status409Conflict, "نام نقش تکراری است");

        if (await roleRepository.CheckExistRolePersianNameAsync(command.PersianName, command.Id, cancellationToken))
            return ApiResponse<RoleDto>.Error(StatusCodes.Status409Conflict, "نام فارسی نقش تکراری است");

        var updatedRole = mapper.Map(command, role);
        if (updatedRole == null)
            return ApiResponse<RoleDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Role updated successfully with {@UpdateRoleCommand}", command);

        var updatedRoleDto = mapper.Map<RoleDto>(updatedRole);
        if (updatedRoleDto == null)
            return ApiResponse<RoleDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        return ApiResponse<RoleDto>.Ok(updatedRoleDto, "نقش با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<PagedResult<RoleDto>>> GetAllRolesAsync(GetAllRolesQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllRoles is Called");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PagedResult<RoleDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin())
            return ApiResponse<PagedResult<RoleDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var (roles, totalCount) = await roleRepository.GetAllRolesAsync(
            query.SearchPhrase,
            query.SortBy,
            true,
            query.PageNumber,
            query.PageSize,
            query.SortDirection,
        cancellationToken);

        var roleDtos = mapper.Map<IReadOnlyList<RoleDto>>(roles) ?? Array.Empty<RoleDto>();
        if (roleDtos == null)
            return ApiResponse<PagedResult<RoleDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {@Count} companytypes", roleDtos.Count);

        var data = new PagedResult<RoleDto>(roleDtos, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<RoleDto>>.Ok(data, "نقش‌ها با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<RoleDto>> GetRoleByIdAsync(GetRoleByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetRoleById is Called with {@Id}", query.Id);

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<RoleDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin())
            return ApiResponse<RoleDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var role = await roleRepository.GetRoleByIdAsync(query.Id, false, false, cancellationToken);
        if (role is null)
            return ApiResponse<RoleDto>.Error(StatusCodes.Status404NotFound, "نقش نامعتبر است");

        var result = mapper.Map<RoleDto>(role);
        if (result == null)
            return ApiResponse<RoleDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Role retrieved successfully with {@Id}", query.Id);
        return ApiResponse<RoleDto>.Ok(result, "نقش با موفقیت دریافت شد");
    }
}
