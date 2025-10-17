using AutoMapper;
using Capitan360.Application.Features.Identities.Roles.Roles.Commands.Create;
using Capitan360.Application.Features.Identities.Roles.Roles.Commands.Update;
using Capitan360.Application.Features.Identities.Roles.Roles.Dtos;
using Capitan360.Domain.Entities.Identities;

namespace Capitan360.Application.Features.Identities.Roles.Roles.MapperProfiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleDto>();

        CreateMap<CreateRoleCommand, Role>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.ConcurrencyStamp, opt => opt.Ignore())


            .ForMember(d => d.NormalizedName, opt => opt.MapFrom(s => s.Name != null ? s.Name.Trim().ToUpperInvariant() : null));

        CreateMap<UpdateRoleCommand, Role>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.ConcurrencyStamp, opt => opt.Ignore())


            .ForMember(d => d.Visible, opt => opt.Ignore())
            .ForMember(d => d.NormalizedName, opt => opt.MapFrom(s => s.Name != null ? s.Name.Trim().ToUpperInvariant() : null));
    }
}
