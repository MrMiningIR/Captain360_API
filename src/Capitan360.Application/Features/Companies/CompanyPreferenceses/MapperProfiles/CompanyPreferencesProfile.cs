using AutoMapper;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.UpdateCompanyPreferences;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Dtos;

namespace Capitan360.Application.Features.Companies.CompanyPreferenceses.MapperProfiles;

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