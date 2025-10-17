using AutoMapper;
using Capitan360.Application.Features.Identities.Identities.Commands.CreateUser;
using Capitan360.Application.Features.Identities.Identities.Commands.UpdateUser;
using Capitan360.Application.Features.Identities.Roles.Roles.Dtos;
using Capitan360.Application.Features.Identities.Users.Users.Dtos;
using Capitan360.Domain.Entities.Identities;

namespace Capitan360.Application.Features.Identities.Identities.MapperProfiles;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {


        CreateMap<CreateUserCommand, User>()
            .ForMember(x => x.UserName, opt => opt.MapFrom(y => y.PhoneNumber));



        CreateMap<User, UserDto>()

         .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company!.Name))

              .ForMember(dest => dest.CompanyTypeId, opt => opt.MapFrom(src => src.CompanyTypeId))
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Roles.First().Id));

        CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PersianName, opt => opt.MapFrom(src => src.PersianName))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Visible, opt => opt.MapFrom(src => src.Visible))

                ;





        //CreateMap<CompanyType, CompanyTypeDto>();





        CreateMap<UpdateUserCommand, User>()
    .ForMember(x => x.UserName, opt => opt.MapFrom(y => y.PhoneNumber));
    }

}