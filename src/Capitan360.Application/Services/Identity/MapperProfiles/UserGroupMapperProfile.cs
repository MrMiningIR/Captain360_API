using AutoMapper;
using Capitan360.Application.Services.Identity.Commands.AddUserGroup;
using Capitan360.Domain.Entities.Identities;

namespace Capitan360.Application.Services.Identity.Profiles;

public class UserGroupMapperProfile : Profile
{
    public UserGroupMapperProfile()
    {
        CreateMap<AddUserGroupCommand, UserGroup>().ReverseMap();

    }
}