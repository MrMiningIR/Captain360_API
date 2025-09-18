using AutoMapper;
using Capitan360.Application.Features.Companies.CompanyType.Commands.CreateCompanyType;
using Capitan360.Application.Features.Companies.CompanyType.Commands.UpdateCompanyType;
using Capitan360.Application.Features.Companies.CompanyTypes.Dtos;

namespace Capitan360.Application.Features.Companies.CompanyType.MapperProfiles;

public class CompanyTypeProfile : Profile
{
    public CompanyTypeProfile()
    {
        CreateMap<CreateCompanyTypeCommand, Domain.Entities.Companies.CompanyType>();
        CreateMap<UpdateCompanyTypeCommand, Domain.Entities.Companies.CompanyType>()
            .ForMember(dest => dest.TypeName, opt => opt.Condition(src => src.TypeName != null))
            .ForMember(dest => dest.DisplayName, opt => opt.Condition(src => src.DisplayName != null))
            .ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null));
        CreateMap<Domain.Entities.Companies.CompanyType, CompanyTypeDto>();
    }
}