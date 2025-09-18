using AutoMapper;
using Capitan360.Application.Features.Identities.Identities.Commands.AddUserGroup;
using Capitan360.Domain.Entities.Identities;

namespace Capitan360.Application.Features.Identities.Identities.MapperProfiles;

public class UserGroupMapperProfile : Profile
{
    public UserGroupMapperProfile()
    {
        CreateMap<AddUserGroupCommand, UserGroup>().ReverseMap();

    }
}