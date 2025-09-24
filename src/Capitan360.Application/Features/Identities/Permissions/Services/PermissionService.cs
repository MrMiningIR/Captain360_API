using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Permission.Dtos;
using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Identities;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Capitan360.Application.Features.Permission.Services;

public class PermissionService(IPermissionRepository permissionRepository,
    PermissionCollectorService permissionCollector, IUnitOfWork unitOfWork, ILogger<PermissionService> logger, IMapper mapper) : IPermissionService
{
    //public async Task<List<string>?> GetUserPermissions(string userId, CancellationToken cancellationToken)
    //{
    //    if (string.IsNullOrEmpty(userId))
    //        throw new UnExpectedException("User Id is Empty");

    //    var permissions = await permissionRepository.GetUserPermissionsAsync(userId, cancellationToken);

    //    return permissions ?? [];
    //}

    public async Task<ApiResponse<List<ParentPermissionTransfer>>> GetParentPermissions(CancellationToken ct)
    {
        logger.LogInformation("GetParentPermissions is Called");

        var result = await permissionRepository.GetParentPermissions(ct);

        return ApiResponse<List<ParentPermissionTransfer>>.Ok(result);
    }

    public async Task<ApiResponse<List<IPermissionService.PermissionDto>>> GetPermissionsByParentName(IPermissionService.GetPermissionsByParentNameQuery parentQuery, CancellationToken ct)
    {
        logger.LogInformation("GetPermissionsByParent is Called with {@GetPermissionsByParent}", parentQuery);
        var result = await permissionRepository.GetPermissionsByParentName(parentQuery.Parent, ct);

        var mappedResult = mapper.Map<List<IPermissionService.PermissionDto>>(result);

        return ApiResponse<List<IPermissionService.PermissionDto>>.Ok(mappedResult);
    }

    public async Task<ApiResponse<List<IPermissionService.PermissionDto>>> GetPermissionsByParentCode(IPermissionService.GetPermissionsByParentCodeQuery parentQuery, CancellationToken ct)
    {
        logger.LogInformation("GetPermissionsByParentCode is Called with {@GetPermissionsByParent}", parentQuery);
        var result = await permissionRepository.GetPermissionsByParentCode(parentQuery.ParentCode, ct);

        var mappedResult = mapper.Map<List<IPermissionService.PermissionDto>>(result);

        return ApiResponse<List<IPermissionService.PermissionDto>>.Ok(mappedResult);
    }

    public async Task<ApiResponse<PagedResult<IPermissionService.PermissionDto>>> GetAllMatchingPermissions(
        IPermissionService.GetAllMatchingPermissionsQuery getAllMatchingPermissionsQuery, CancellationToken ct)
    {
        logger.LogInformation("GetAllCompanies is Called");
        if (getAllMatchingPermissionsQuery.PageSize <= 0 || getAllMatchingPermissionsQuery.PageNumber <= 0)
            return ApiResponse<PagedResult<IPermissionService.PermissionDto>>.Error(400, "اندازه صفحه یا شماره صفحه نامعتبر است");

        var (permissions, totalCount) = await permissionRepository.GetAllMatchingPermissions(getAllMatchingPermissionsQuery.SearchPhrase,
            getAllMatchingPermissionsQuery.PageSize, getAllMatchingPermissionsQuery.PageNumber, ct);

        var permissionDtos = mapper.Map<IReadOnlyList<IPermissionService.PermissionDto>>(permissions)
                          ?? Array.Empty<IPermissionService.PermissionDto>();
        logger.LogInformation("Retrieved {Count} Permissions", permissionDtos.Count);

        var data = new PagedResult<IPermissionService.PermissionDto>(permissionDtos, totalCount, getAllMatchingPermissionsQuery.PageSize, getAllMatchingPermissionsQuery.PageNumber);
        return ApiResponse<PagedResult<IPermissionService.PermissionDto>>.Ok(data, "Companies retrieved successfully");
    }

    public ApiResponse<List<PermissionCollectorDto>> GetSystemPermissions(Assembly? assembly)
    {
        if (assembly is null)
            return ApiResponse<List<PermissionCollectorDto>>.Error(400, "ناتوانی در شناسایی پرمیشن ها");

        var permissions = permissionCollector.GetActionsWithPermissionFilter(assembly);
        return ApiResponse<List<PermissionCollectorDto>>.Ok(permissions, "دسترسی ها با موفقیت بازیابی شدند");
    }

    public List<string> GetPermissionsForSystem(Assembly? assembly)
    {
        if (assembly is null)
            return [];
        var permissions = permissionCollector.GetActionsWithPermissionFilter(assembly);

        var resultList = new HashSet<string>();

        foreach (var permission in permissions)
        {
            resultList.Add(permission.PermissionName);
        }

        return resultList.ToList();
    }

    public async Task SavePermissionsInSystem(List<PermissionCollectorDto> permissionsData, CancellationToken ct)
    {
        string permissionName = "";
        string parentName = "";
        try
        {

            foreach (var permission in permissionsData)
            {
                var exist = await permissionRepository.ExistPermissionInPermissionSource(permission.PermissionCode,
                    permission.ParentCode, ct);

                if (!exist)
                {
                    permissionName = permission.PermissionName;
                    parentName = permission.Parent;
                    await permissionRepository.AddPermissionToPermissionSource(new Domain.Entities.Identities.Permission
                    {
                        Name = permission.PermissionName,
                        Parent = permission.Parent,
                        Active = true,
                        DisplayName = permission.DisplayName ?? "ثبت نشده",
                        ParentDisplayName = permission.SourceDisplayName ?? "ثبت نشده",
                        ParentCode = permission.ParentCode,
                        PermissionCode = permission.PermissionCode
                    }, ct);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"{permissionName}-{parentName}");
            throw;
        }
    }

    public async Task DeleteUnAvailablePermissions(List<PermissionCollectorDto> permissionsData, CancellationToken cancellationToken)
    {
        try
        {

            var existedPermissionList = await permissionRepository.GetAllPolicy(cancellationToken);

            foreach (var permission in existedPermissionList)
            {
                if (!permissionsData.Any(x => x.PermissionCode == permission.PermissionCode && x.ParentCode == permission.ParentCode))
                {
                    permissionRepository.DeletePermission(permission, cancellationToken);
                }
            }
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ApiResponse<List<Domain.Entities.Identities.Permission>>> GetDbPermissions(CancellationToken ct)
    {
        var permissions = await permissionRepository.GetAllPolicy(ct);
        if (!permissions.Any())
            return ApiResponse<List<Domain.Entities.Identities.Permission>>.Error(400, "در دیتابیس مجوزی وجود ندارد");

        return ApiResponse<List<Domain.Entities.Identities.Permission>>.Ok(permissions.ToList());
    }

    public async Task<ApiResponse<List<int>>> GetDbPermissionsId(CancellationToken ct)
    {
        var permissions = await permissionRepository.GetAllPermissionsId(ct);
        if (!permissions.Any())
            return ApiResponse<List<int>>.Error(400, "در دیتابیس مجوزی وجود ندارد");

        return ApiResponse<List<int>>.Ok(permissions);
    }
}