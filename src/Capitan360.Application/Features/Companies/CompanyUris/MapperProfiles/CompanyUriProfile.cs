using AutoMapper;
using Capitan360.Application.Features.Companies.CompanyUris.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyUris.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyUris.Dtos;
using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Application.Features.Companies.CompanyUris.MapperProfiles;

public class CompanyUriProfile : Profile
{
    public CompanyUriProfile()
    {
        CreateMap<CompanyUri, CompanyUriDto>()
            .ForMember(d => d.CompanyName, o => o.MapFrom(s => s.Company != null ? s.Company.Name : null));

        CreateMap<CompanyUriDto, CompanyUri>()
            .ForMember(d => d.Company, o => o.Ignore())
            .ForMember(d => d.ConcurrencyToken, o => o.Ignore());

        CreateMap<CreateCompanyUriCommand, CompanyUri>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Company, o => o.Ignore())
            .ForMember(d => d.ConcurrencyToken, o => o.Ignore());

        CreateMap<UpdateCompanyUriCommand, CompanyUri>()
            .ForMember(d => d.CompanyId, o => o.Ignore())
            .ForMember(d => d.Company, o => o.Ignore())
            .ForMember(d => d.ConcurrencyToken, o => o.Ignore())
            .ForAllMembers(o => o.Condition((src, dest, srcMember) => srcMember != null));
    }
}