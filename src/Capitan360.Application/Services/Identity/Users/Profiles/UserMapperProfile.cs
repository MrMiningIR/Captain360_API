using AutoMapper;
using Capitan360.Application.Services.Identity.Users.Commands.CreateUser;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.UserEntity;

namespace Capitan360.Application.Services.Identity.Users.Profiles;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {

        
        CreateMap<CreateUserCommand, User>()
            .ForMember(x => x.UserName, opt => opt.MapFrom(y => y.PhoneNumber))
            .ForMember(x => x.UserProfile, opt => opt.MapFrom(y => new UserProfile()
            {
                MoadianFactorType = (MoadianFactorType) y.MoadianType
            }));
    }
    
    
}