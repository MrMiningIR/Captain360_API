using AutoMapper;
using Capitan360.Application.Features.Identities.Users.UserPermissions.Dtos;
using Capitan360.Domain.Entities.Identities;

namespace Capitan360.Application.Features.Identities.Users.UserPermissions.MapperProfiles;

public class UserPermissionProfile : Profile
{
    public UserPermissionProfile()
    {
        CreateMap<UserPermission, UserPermissionDto>()
         .ForMember(d => d.UserNameFamily, opt => opt.MapFrom(s => s.User != null ? s.User.NameFamily : null))
         .ForMember(d => d.PermissionDisplayName, opt => opt.MapFrom(s => s.Permission != null ? s.Permission.DisplayName : null));

        CreateMap<UserPermissionDto, UserPermission>()
            .ForMember(d => d.User, opt => opt.Ignore())
            .ForMember(d => d.Permission, opt => opt.Ignore())
            .ForMember(d => d.ConcurrencyToken, opt => opt.Ignore());
    }
}
