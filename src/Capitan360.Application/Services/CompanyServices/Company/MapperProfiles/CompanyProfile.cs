using AutoMapper;
using Capitan360.Application.Services.CompanyServices.Company.Commands.CreateCompany;
using Capitan360.Application.Services.CompanyServices.Company.Commands.UpdateCompany;
using Capitan360.Application.Services.CompanyServices.Company.Dtos;

namespace Capitan360.Application.Services.CompanyServices.Company.MapperProfiles;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<CreateCompanyCommand, Domain.Entities.Companies.Company>();

        CreateMap<Domain.Entities.Companies.Company, CompanyDto>()

            .ForMember(dest => dest.CompanyTypeName,
                opt => opt.MapFrom(src => src.CompanyType.DisplayName));

        CreateMap<UpdateCompanyCommand, Domain.Entities.Companies.Company>();
        //.ForMember(dest => dest.Code, opt => opt.Condition(src => src.Code != null))
        //.ForMember(dest => dest.PhoneNumber, opt => opt.Condition(src => src.PhoneNumber != null))
        //.ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != null))
        //.ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null))
        //.ForMember(dest => dest.IsParentCompany, opt => opt.Condition(src => src.IsParentCompany.HasValue))
        //.ForMember(dest => dest.Active, opt => opt.Condition(src => src.Active.HasValue));
    }
}