using AutoMapper;
using Capitan360.Application.Services.Dtos;
using Capitan360.Application.Services.Identity.Commands.CreateUser;
using Capitan360.Application.Services.Identity.Commands.UpdateUser;
using Capitan360.Application.Services.Identity.Dtos;
using Capitan360.Domain.Entities.Users;
using Capitan360.Domain.Enums;

namespace Capitan360.Application.Services.Identity.MapperProfiles;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {


        CreateMap<CreateUserCommand, User>()
            .ForMember(x => x.UserName, opt => opt.MapFrom(y => y.PhoneNumber))
            .ForMember(x => x.Profile, opt => opt.MapFrom(y => new UserProfile()
            {
                MoadianFactorType = (MoadianFactorType)y.MoadianFactorType
            }));

        CreateMap<User, UserDto>()
             .ForMember(dest => dest.UserKind, opt => opt.MapFrom(src => src.UserKind))

            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src =>
                src.UserCompanies.Any()
                    ? src.UserCompanies.FirstOrDefault()!.Company.Name
                    : "-"))
             .ForMember(dest => dest.IsParentCompany, opt => opt.MapFrom(src =>
                src.UserCompanies.Any() && src.UserCompanies.FirstOrDefault()!.Company.IsParentCompany))
             .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src =>
                src.UserCompanies.Any()
                    ? src.UserCompanies.FirstOrDefault()!.Company.Id
                    : 0))
             .ForMember(dest => dest.MoadianFactorType, opt => opt.MapFrom(src =>
                src.Profile != null
                    ? src.Profile.MoadianFactorType
                    : MoadianFactorType.Haghigh))
              .ForMember(dest => dest.CompanyTypeId, opt => opt.MapFrom(src => src.CompanyType))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles));

        CreateMap<Domain.Entities.Authorizations.Role, RoleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RolePersianName, opt => opt.MapFrom(src => src.PersianName))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Visible, opt => opt.MapFrom(src => src.Visible))

                ;

        //CreateMap<CreateUserCommand, User>().ReverseMap()
        //    .ForMember(d => d.MoadianFactorType, opt => opt
        //        .MapFrom(op => new UserProfile
        //        {
        //            MoadianFactorType = op.Profile.MoadianFactorType
        //        }

        //        ));


        CreateMap<Domain.Entities.Companies.UserCompany, UserDto>()
    .ForMember(dest => dest.UserKind, opt => opt.MapFrom(src => src.User.UserKind))
    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User.Id))
    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName))
    .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
    .ForMember(dest => dest.MoadianFactorType, opt => opt.MapFrom(src => src.User.Profile != null ?
        src.User.Profile.MoadianFactorType : MoadianFactorType.Unknown))
    .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
    .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Company.Id))
    .ForMember(dest => dest.CompanyTypeId, opt => opt.MapFrom(src => src.User.CompanyType))
    .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.User.Roles));

        //CreateMap<CompanyType, CompanyTypeDto>();





        CreateMap<UpdateUserCommand, User>()
    .ForMember(x => x.UserName, opt => opt.MapFrom(y => y.PhoneNumber))
    .ForMember(x => x.Profile, opt => opt.MapFrom(y => new UserProfile()
    {
        MoadianFactorType = (MoadianFactorType)y.MoadianFactorType,
        UserId = y.UserId
    }));
    }

}