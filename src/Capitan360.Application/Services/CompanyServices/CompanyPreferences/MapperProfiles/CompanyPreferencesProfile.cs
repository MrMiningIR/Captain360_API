using AutoMapper;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.CreateCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.UpdateCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Dtos;

namespace Capitan360.Application.Services.CompanyServices.CompanyPreferences.MapperProfiles;

public class CompanyPreferencesProfile : Profile
{
    public CompanyPreferencesProfile()
    {
        CreateMap<CreateCompanyPreferencesCommand, Domain.Entities.Companies.CompanyPreferences>();
        CreateMap<UpdateCompanyPreferencesCommand, Domain.Entities.Companies.CompanyPreferences>()
          .ForMember(dest => dest.Company, opt => opt.Ignore());
        CreateMap<Domain.Entities.Companies.CompanyPreferences, CompanyPreferencesDto>();
    }
}