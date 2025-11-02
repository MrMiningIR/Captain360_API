using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Identities.Old.Commands.RemoveUserPermission;
using Capitan360.Application.Features.Identities.Old.Commands.UpDeInlUserPermissionById;
using Capitan360.Application.Features.Identities.Old.Queries.GetUserPermissions;
using Capitan360.Application.Features.Identities.Users.UserPermissions.Commands.AssignPermissions;
using Capitan360.Application.Features.Identities.Users.UserPermissions.Dtos;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Identities;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Features.Identities.Old.Services;

public class UserPermissionService(ILogger<UserPermissionService> logger,
  IMapper mapper, IUserPermissionRepository userPermissionRepository, IUserRepository userRepository,
  IUnitOfWork unitOfWork) : IUserPermissionService
{
    public async Task<ApiResponse<int>> AssignPermissionToUser(AssignPermissionsToUserCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("AssignPermissionToUser Called with {@AssignPermission}", command);

        var mappedUserPermission = mapper.Map<Domain.Entities.Identities.UserPermission>(command);

        if (mappedUserPermission == null)
            return ApiResponse<int>.Error(400, "مشکل در عملیات تبدیل");
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var user = await userRepository.GetUserByIdAsync(command.UserId, false, true, cancellationToken);
        if (user is null)
        {
            return ApiResponse<int>.Error(400, "کاربر معتبر نیست");
        }
        var result = await userPermissionRepository.AssignPermissionToUser(mappedUserPermission, cancellationToken);




        user.PermissionVersion = Guid.NewGuid().ToString();

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        logger.LogInformation("Permission successfully Added: {Id}", result);

        return ApiResponse<int>.Ok(result);
    }

    public async Task<ApiResponse<List<int>>> AssignPermissionsToUser(AssignPermissionsToUserCommand commands, CancellationToken cancellationToken)
    {
        logger.LogInformation("AssignPermissionsToUser Called with {@AssignPermissions}", commands);

        var mappedUserPermissions = mapper.Map<List<Domain.Entities.Identities.UserPermission>>(commands.PermissionList);

        if (mappedUserPermissions == null)
            return ApiResponse<List<int>>.Error(400, "مشکل در عملیات تبدیل");

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var user = await userRepository.GetUserByIdAsync(commands.UserId, false, true, cancellationToken);
        if (user is null)
        {
            return ApiResponse<List<int>>.Error(400, "کاربر معتبر نیست");
        }
        var result = await userPermissionRepository.AssignPermissionsToUser(mappedUserPermissions, cancellationToken);

        user.PermissionVersion = Guid.NewGuid().ToString();
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        logger.LogInformation("Permissions successfully Added: {Ids}", string.Join(",", result));

        return ApiResponse<List<int>>.Ok(result);
    }

    public async Task<ApiResponse<int>> RemovePermissionFromUser(RemoveUserPermissionCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("RemovePermissionFromUser Called with {@RemovePermission}", command);

        var userPermission =
            await userPermissionRepository.GetUserPermissionByPermissionIdAndUserId(command.UserId,
                command.PermissionId, cancellationToken);

        if (userPermission is null)
            return ApiResponse<int>.Error(400, "این دسترسی وجود ندارد");

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var user = await userRepository.GetUserByIdAsync(command.UserId, false, true, cancellationToken);
        if (user is null)
        {
            return ApiResponse<int>.Error(400, "کاربر معتبر نیست");
        }

        var result = await userPermissionRepository.RemovePermissionFromUser(userPermission, cancellationToken);

        user.PermissionVersion = Guid.NewGuid().ToString();
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        logger.LogInformation("Permissions successfully Removed: {Id}", result);

        return ApiResponse<int>.Ok(result);
    }

    public async Task<ApiResponse<List<int>>> RemovePermissionsFromUser(RemoveUserPermissionCommands commands, CancellationToken cancellationToken)
    {
        logger.LogInformation("RemovePermissionsFromUser Called with {@RemovePermissions}", commands);

        var mappedUserPermissions = mapper.Map<List<Domain.Entities.Identities.UserPermission>>(commands.PermissionList);

        if (mappedUserPermissions == null)
            return ApiResponse<List<int>>.Error(400, "مشکل در عملیات تبدیل");
        await unitOfWork.BeginTransactionAsync(cancellationToken);
        var user = await userRepository.GetUserByIdAsync(commands.PermissionList.First().UserId, false, true, cancellationToken);
        if (user is null)
        {
            return ApiResponse<List<int>>.Error(400, "کاربر معتبر نیست");
        }

        var result = await userPermissionRepository.RemovePermissionsFromUser(mappedUserPermissions, cancellationToken);
        user.PermissionVersion = Guid.NewGuid().ToString();
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);
        logger.LogInformation("Permissions successfully Removed: {Ids}", string.Join(",", result));

        return ApiResponse<List<int>>.Ok(result);
    }



    public async Task<ApiResponse<List<string>>> UserPermissionOperation(UpDeInlUserPermissionByIdsCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UserPermissionOperation Called with {@UpDeInlUser}", command);
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var user = await userRepository.GetUserByIdAsync(command.UserId!, false, true, cancellationToken);
        if (user is null)
        {
            return ApiResponse<List<string>>.Error(400, "کاربر معتبر نیست");



        }

        await userPermissionRepository.RemoveAllPermissionsFromUser(command.UserId!, cancellationToken);


        if (command.PermissionIds.Any())
        {

            await userPermissionRepository.AssignPermissionsToUser(command.PermissionIds, command.UserId!, cancellationToken);
        }


        //var (currentPermissionsResult, _) = await userPermissionRepository.GetAllUserPermissions(command.UserId!, 100, 1, cancellationToken);
        //if (!command.PermissionIds.Any())
        //{
        //    if (currentPermissionsResult.Any())
        //    {
        //        await userPermissionRepository.RemovePermissionsFromUser(currentPermissionsResult.ToList(), cancellationToken);
        //    }
        //}
        //else
        //{
        //    if (currentPermissionsResult.Any())
        //    {
        //        List<string> mustBeAddedPermissionIds = command.PermissionIds.Except(currentPermissionsResult.Select(x => x.PermissionId.ToString())
        //            .ToList()).ToList();

        //        List<string> mustBeRemovedPermissionIds = currentPermissionsResult.Select(x => x.PermissionId.ToString()).ToList()
        //            .Except(command.PermissionIds).ToList();

        //        if (mustBeAddedPermissionIds.Any())
        //        {

        //            await userPermissionRepository.AssignPermissionsToUser(mustBeAddedPermissionIds, command.UserId!, cancellationToken);
        //        }

        //        if (mustBeRemovedPermissionIds.Any())
        //        {

        //            await userPermissionRepository.RemovePermissionsFromUser(mustBeRemovedPermissionIds, command.UserId!, cancellationToken);
        //        }
        //    }
        //    else
        //    {
        //        await userPermissionRepository.AssignPermissionsToUser(command.PermissionIds, command.UserId!, cancellationToken);
        //    }
        //}



        user.PermissionVersion = Guid.NewGuid().ToString();
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

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