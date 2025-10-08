using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Identities.Permissions.Dtos;
using Capitan360.Domain.Dtos.TransferObject;
using FluentValidation;
using System.ComponentModel;
using System.Reflection;

namespace Capitan360.Application.Features.Identities.Permissions.Services;

public interface IPermissionService
{
    //TODO
    //Task<List<string>?> GetUserPermissions(string userId, CancellationToken cancellationToken);

    //----------------------
    Task<ApiResponse<List<ParentPermissionTransfer>>> GetParentPermissions(CancellationToken cancellationToken);

    Task<ApiResponse<List<PermissionDto>>> GetPermissionsByParentName(GetPermissionsByParentNameQuery parentQuery, CancellationToken cancellationToken);
    Task<ApiResponse<List<PermissionDto>>> GetPermissionsByParentCode(GetPermissionsByParentCodeQuery parentQuery, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<PermissionDto>>>
        GetAllMatchingPermissions(GetAllMatchingPermissionsQuery getAllMatchingPermissionsQuery, CancellationToken cancellationToken);

    public record GetAllMatchingPermissionsQuery(

     string? SearchPhrase,
    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(50)] int PageSize = 50);

    public class GetAllMatchingPermissionsQueryValidator : AbstractValidator<GetAllMatchingPermissionsQuery>
    {
        private int[] _allowPageSizes = [5, 10, 15, 30, 50];

        public GetAllMatchingPermissionsQueryValidator()
        {
            RuleFor(r => r.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(r => r.PageSize)
                .Must(value => _allowPageSizes.Contains(value))
                .WithMessage($"Page size must be in [{string.Join(",", _allowPageSizes)}]");
        }
    }

    public record GetPermissionsByParentCodeQuery
    {
        public Guid ParentCode { get; set; }

    }; public record GetPermissionsByParentIdQuery
    {
        public int ParentId { get; set; }

    };
    public class GetPermissionsByParentIdQueryValidator : AbstractValidator<GetPermissionsByParentIdQuery>
    {
        public GetPermissionsByParentIdQueryValidator()
        {
            RuleFor(x => x.ParentId)
                .GreaterThan(0)
                .WithMessage("شناسه والد نمیتواند 0 باشد");
        }
    }
    public class GetPermissionsByParentCodeQueryValidator : AbstractValidator<GetPermissionsByParentCodeQuery>
    {
        public GetPermissionsByParentCodeQueryValidator()
        {
            RuleFor(x => x.ParentCode)
                .NotEmpty()
                .WithMessage("شناسه والد نمیتواند خالی باشد");
        }
    }
    public record GetPermissionsByParentNameQuery
    {
        public string Parent { get; set; }
    }

    public class GetPermissionsByParentNameQueryValidator : AbstractValidator<GetPermissionsByParentNameQuery>
    {
        public GetPermissionsByParentNameQueryValidator()
        {
            RuleFor(x => x.Parent)
                .NotEmpty()
                .WithMessage("شناسه کاربری نمیتواند خالی باشد");
        }
    }


    public class PermissionDto
    {
        public string Name { get; set; } = default!;
        public int Id { get; set; }
        public string DisplayName { get; set; } = default!;
        public string Parent { get; set; }
        public string ParentDisplayName { get; set; }
        public bool Active { get; set; }
        public Guid PermissionCode { get; set; }
        public Guid ParentCode { get; set; }
    }

    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<Domain.Entities.Identities.Permission, PermissionDto>();
        }
    }

    // System Permission Operation
    ApiResponse<List<PermissionDto>> GetSystemPermissions(Assembly? assembly);

    List<string> GetPermissionsForSystem(Assembly? assembly);

    Task SavePermissionsInSystem(List<PermissionDto> permissionsData, CancellationToken cancellationToken);

    Task DeleteUnAvailablePermissions(List<PermissionDto> permissionsData, CancellationToken cancellationToken);

    Task<ApiResponse<List<Domain.Entities.Identities.Permission>>> GetDbPermissions(CancellationToken cancellationToken);

    Task<ApiResponse<List<int>>> GetDbPermissionsId(CancellationToken cancellationToken);
}