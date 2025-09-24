using AutoMapper;
using Capitan360.Application.Features.Companies.CompanySmsPatternses.Commands.Create;
using Capitan360.Application.Features.Companies.CompanySmsPatternses.Commands.Update;
using Capitan360.Application.Features.Companies.CompanySmsPatternses.Dtos;
using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Application.Features.Companies.CompanySmsPatternses.MapperProfiles;

public class CompanySmsPatternsProfile : Profile
{
    public CompanySmsPatternsProfile()
    {
        CreateMap<CompanySmsPatterns, CompanySmsPatternsDto>()
            .ForMember(d => d.CompanyName, o => o.MapFrom(s => s.Company != null ? s.Company.Name : null));

        CreateMap<CompanySmsPatternsDto, CompanySmsPatterns>()
            .ForMember(d => d.Company, o => o.Ignore())
            .ForMember(d => d.ConcurrencyToken, o => o.Ignore());

        CreateMap<CreateCompanySmsPatternsCommand, CompanySmsPatterns>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Company, o => o.Ignore())
            .ForMember(d => d.ConcurrencyToken, o => o.Ignore());

        CreateMap<UpdateCompanySmsPatternsCommand, CompanySmsPatterns>()
            .ForMember(d => d.CompanyId, o => o.Ignore())
            .ForMember(d => d.Company, o => o.Ignore())
            .ForMember(d => d.ConcurrencyToken, o => o.Ignore())
            .ForAllMembers(o => o.Condition((src, dest, srcMember) => srcMember != null));
    }
}