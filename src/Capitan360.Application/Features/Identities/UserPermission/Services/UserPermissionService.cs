using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.UserPermission.Commands.AssignUserPermission;
using Capitan360.Application.Features.UserPermission.Commands.RemoveUserPermission;
using Capitan360.Application.Features.UserPermission.Commands.UpDeInlUserPermissionById;
using Capitan360.Application.Features.UserPermission.Dtos;
using Capitan360.Application.Features.UserPermission.Queries.GetUserPermissions;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Repositories.Identities;
using Capitan360.Domain.Repositories.Permissions;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Features.UserPermission.Services;

public class UserPermissionService(ILogger<UserPermissionService> logger,
  IMapper mapper, IUserPermissionRepository userPermissionRepository,
  IUserPermissionVersionControlRepository userPermissionVersionControlRepository,
  IUnitOfWork unitOfWork) : IUserPermissionService
{
    public async Task<ApiResponse<int>> AssignPermissionToUser(AssignUserPermissionCommand command, CancellationToken ct)
    {
        logger.LogInformation("AssignPermissionToUser Called with {@AssignPermission}", command);

        var mappedUserPermission = mapper.Map<Domain.Entities.Identities.UserPermission>(command);

        if (mappedUserPermission == null)
            return ApiResponse<int>.Error(400, "مشکل در عملیات تبدیل");
        await unitOfWork.BeginTransactionAsync(ct);

        var result = await userPermissionRepository.AssignPermissionToUser(mappedUserPermission, ct);

        var pvc = await userPermissionVersionControlRepository.GetUserPermissionVersionObj(command.UserId, ct);
        if (pvc is null)
            return ApiResponse<int>.Error(400, "کاربر معتبر نیست");

        userPermissionVersionControlRepository.UpdateUserPermissionVersion(pvc);
        await unitOfWork.SaveChangesAsync(ct);
        await unitOfWork.CommitTransactionAsync(ct);

        logger.LogInformation("Permission successfully Added: {Id}", result);

        return ApiResponse<int>.Ok(result);
    }

    public async Task<ApiResponse<List<int>>> AssignPermissionsToUser(AssignUserPermissionCommands commands, CancellationToken ct)
    {
        logger.LogInformation("AssignPermissionsToUser Called with {@AssignPermissions}", commands);

        var mappedUserPermissions = mapper.Map<List<Domain.Entities.Identities.UserPermission>>(commands.PermissionList);

        if (mappedUserPermissions == null)
            return ApiResponse<List<int>>.Error(400, "مشکل در عملیات تبدیل");

        await unitOfWork.BeginTransactionAsync(ct);

        var result = await userPermissionRepository.AssignPermissionsToUser(mappedUserPermissions, ct);
        var pvc = await userPermissionVersionControlRepository.GetUserPermissionVersionObj(commands.PermissionList.First().UserId, ct);
        if (pvc is null)
            return ApiResponse<List<int>>.Error(400, "کاربر معتبر نیست");
        userPermissionVersionControlRepository.UpdateUserPermissionVersion(pvc);
        await unitOfWork.SaveChangesAsync(ct);
        await unitOfWork.CommitTransactionAsync(ct);

        logger.LogInformation("Permissions successfully Added: {Ids}", string.Join(",", result));

        return ApiResponse<List<int>>.Ok(result);
    }

    public async Task<ApiResponse<int>> RemovePermissionFromUser(RemoveUserPermissionCommand command, CancellationToken ct)
    {
        logger.LogInformation("RemovePermissionFromUser Called with {@RemovePermission}", command);

        var userPermission =
            await userPermissionRepository.GetUserPermissionByPermissionIdAndUserId(command.UserId,
                command.PermissionId, ct);

        if (userPermission is null)
            return ApiResponse<int>.Error(400, "این دسترسی وجود ندارد");

        await unitOfWork.BeginTransactionAsync(ct);

        var result = await userPermissionRepository.RemovePermissionFromUser(userPermission, ct);

        var pvc = await userPermissionVersionControlRepository.GetUserPermissionVersionObj(command.UserId, ct);
        if (pvc is null)
            return ApiResponse<int>.Error(400, "کاربر معتبر نیست");
        userPermissionVersionControlRepository.UpdateUserPermissionVersion(pvc);
        await unitOfWork.SaveChangesAsync(ct);
        await unitOfWork.CommitTransactionAsync(ct);

        logger.LogInformation("Permissions successfully Removed: {Id}", result);

        return ApiResponse<int>.Ok(result);
    }

    public async Task<ApiResponse<List<int>>> RemovePermissionsFromUser(RemoveUserPermissionCommands commands, CancellationToken ct)
    {
        logger.LogInformation("RemovePermissionsFromUser Called with {@RemovePermissions}", commands);

        var mappedUserPermissions = mapper.Map<List<Domain.Entities.Identities.UserPermission>>(commands.PermissionList);

        if (mappedUserPermissions == null)
            return ApiResponse<List<int>>.Error(400, "مشکل در عملیات تبدیل");
        await unitOfWork.BeginTransactionAsync(ct);

        var result = await userPermissionRepository.RemovePermissionsFromUser(mappedUserPermissions, ct);
        var pvc = await userPermissionVersionControlRepository.GetUserPermissionVersionObj(commands.PermissionList.First().UserId, ct);
        if (pvc is null)
            return ApiResponse<List<int>>.Error(400, "کاربر معتبر نیست");
        userPermissionVersionControlRepository.UpdateUserPermissionVersion(pvc);
        await unitOfWork.SaveChangesAsync(ct);
        await unitOfWork.CommitTransactionAsync(ct);
        logger.LogInformation("Permissions successfully Removed: {Ids}", string.Join(",", result));

        return ApiResponse<List<int>>.Ok(result);
    }



    public async Task<ApiResponse<List<string>>> UserPermissionOperation(UpDeInlUserPermissionByIdsCommand command, CancellationToken ct)
    {
        logger.LogInformation("UserPermissionOperation Called with {@UpDeInlUser}", command);
        await unitOfWork.BeginTransactionAsync(ct);

        var (currentPermissionsResult, _) = await userPermissionRepository.GetAllUserPermissions(command.UserId!, 100, 1, ct);
        if (!command.PermissionIds.Any())
        {
            if (currentPermissionsResult.Any())
            {
                await userPermissionRepository.RemovePermissionsFromUser(currentPermissionsResult.ToList(), ct);
            }
        }
        else
        {
            if (currentPermissionsResult.Any())
            {
                List<string> mustBeAddedPermissionIds = command.PermissionIds.Except(currentPermissionsResult.Select(x => x.PermissionId.ToString())
                    .ToList()).ToList();

                List<string> mustBeRemovedPermissionIds = currentPermissionsResult.Select(x => x.PermissionId.ToString()).ToList()
                    .Except(command.PermissionIds).ToList();

                if (mustBeAddedPermissionIds.Any())
                {

                    await userPermissionRepository.AssignPermissionsToUser(mustBeAddedPermissionIds, command.UserId!, ct);
                }

                if (mustBeRemovedPermissionIds.Any())
                {

                    await userPermissionRepository.RemovePermissionsFromUser(mustBeRemovedPermissionIds, command.UserId!, ct);
                }
            }
            else
            {
                await userPermissionRepository.AssignPermissionsToUser(command.PermissionIds, command.UserId!, ct);
            }
        }



        var pvc = await userPermissionVersionControlRepository.GetUserPermissionVersionObj(command.UserId, ct);
        if (pvc is null)
            return ApiResponse<List<string>>.Error(400, "کاربر معتبر نیست");
        userPermissionVersionControlRepository.UpdateUserPermissionVersion(pvc);
        await unitOfWork.SaveChangesAsync(ct);
        await unitOfWork.CommitTransactionAsync(ct);

        logger.LogInformation("Permissions successfully Operated!: {Ids}", string.Join(",", command.PermissionIds));

        return ApiResponse<List<string>>.Ok(command.PermissionIds);
    }

    public async Task<ApiResponse<PagedResult<UserPermissionDto>>> GetMatchingAllUserPermissions(GetUserPermissionsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetMatchingAllUserPermissions Called with {@GetAllUserPermissions}", query);

        var (items, totalCount) = await userPermissionRepository.GetAllUserPermissions(query.UserId, query.PageSize, query.PageNumber,
            cancellationToken);
        var mappedUserPermissionsDto = mapper.Map<IReadOnlyList<UserPermissionDto>>(items) ?? Array.Empty<UserPermissionDto>(); ;

        logger.LogInformation("Retrieved {Count} Permissions", mappedUserPermissionsDto.Count);

        var data = new PagedResult<UserPermissionDto>(mappedUserPermissionsDto, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<UserPermissionDto>>.Ok(data, "Permisisons retrieved successfully");
    }
}