using AutoMapper;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Dtos;
using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Application.Features.Companies.CompanyPreferenceses.MapperProfiles;

public class CompanyPreferencesProfile : Profile
{
    public CompanyPreferencesProfile()
    {
        CreateMap<CompanyPreferences, CompanyPreferencesDto>()
            .ForMember(d => d.CompanyName, o => o.MapFrom(s => s.Company != null ? s.Company.Name : null));

        CreateMap<CompanyPreferencesDto, CompanyPreferences>()
            .ForMember(d => d.Company, o => o.Ignore())
            .ForMember(d => d.ConcurrencyToken, o => o.Ignore());

        CreateMap<CreateCompanyPreferencesCommand, CompanyPreferences>()
            .ForMember(d => d.Id, o => o.Ignore()) 
            .ForMember(d => d.Company, o => o.Ignore())
            .ForMember(d => d.ConcurrencyToken, o => o.Ignore());

        CreateMap<UpdateCompanyPreferencesCommand, CompanyPreferences>()
           .ForMember(d => d.CompanyId, o => o.Ignore())
           .ForMember(d => d.Company, o => o.Ignore())
           .ForMember(d => d.ConcurrencyToken, o => o.Ignore())
           .ForAllMembers(o => o.Condition((src, dest, srcMember) => srcMember != null));
    }
}