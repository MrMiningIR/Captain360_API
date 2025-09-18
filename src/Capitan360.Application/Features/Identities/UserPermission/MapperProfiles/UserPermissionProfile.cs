using AutoMapper;
using Capitan360.Application.Features.UserPermission.Commands.AssignUserPermission;
using Capitan360.Application.Features.UserPermission.Commands.RemoveUserPermission;
using Capitan360.Application.Features.UserPermission.Dtos;

namespace Capitan360.Application.Features.UserPermission.MapperProfiles;

public class UserPermissionProfile : Profile
{
    public UserPermissionProfile()
    {
        CreateMap<AssignUserPermissionCommand, Domain.Entities.Identities.UserPermission>();
        CreateMap<RemoveUserPermissionCommand, Domain.Entities.Identities.UserPermission>();
        CreateMap<Domain.Entities.Identities.UserPermission, UserPermissionDto>()
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