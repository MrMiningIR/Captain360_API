using AutoMapper;

using Capitan360.Application.Services.Identity.Users.Commands;
using Capitan360.Domain.Entities.UserEntity;

namespace Capitan360.Application.Services.Identity.Users;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {

        CreateMap<CreateUserCommand, User>().ReverseMap()
            .ForMember(d => d.MoadianFactorType, opt => opt
                .MapFrom(op => new UserProfile
                {
                    MoadianFactorType = op.Profile.MoadianFactorType
                }

                ));
    }
}