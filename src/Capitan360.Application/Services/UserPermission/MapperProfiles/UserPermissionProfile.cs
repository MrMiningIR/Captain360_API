using AutoMapper;
using Capitan360.Application.Services.UserPermission.Commands.AssignUserPermission;
using Capitan360.Application.Services.UserPermission.Commands.RemoveUserPermission;
using Capitan360.Application.Services.UserPermission.Dtos;

namespace Capitan360.Application.Services.UserPermission.MapperProfiles;

public class UserPermissionProfile : Profile
{
    public UserPermissionProfile()
    {
        CreateMap<AssignUserPermissionCommand, Domain.Entities.AuthorizationEntity.UserPermission>();
        CreateMap<RemoveUserPermissionCommand, Domain.Entities.AuthorizationEntity.UserPermission>();
        CreateMap<Domain.Entities.AuthorizationEntity.UserPermission, UserPermissionDto>()
            .ForMember(des => des.PermissionName, opt =>
                opt.MapFrom(x => x.Permission.Name))
            .ForMember(des => des.DisplayPermissionName, opt =>
                opt.MapFrom(x => x.Permission.DisplayName))
            .ForMember(des => des.ParentCode, opt =>
                opt.MapFrom(x => x.Permission.ParentCode))

            ;
        //CreateMap<UpdateContentTypeCommand, ContentType>();
        //CreateMap<ContentType, ContentTypeDto>()
        //    .ForMember(des => des.CompanyTypeName, opt => opt.MapFrom(des => des.CompanyType.DisplayName));
    }
}