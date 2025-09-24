using AutoMapper;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Dtos;
using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Application.Features.Companies.CompanyCommissionses.MapperProfiles;

public class CompanyCommissionsProfile : Profile
{
    public CompanyCommissionsProfile()
    {
        CreateMap<CompanyCommissions, CompanyCommissionsDto>()
            .ForMember(d => d.CompanyName, o => o.MapFrom(s => s.Company != null ? s.Company.Name : null));

        CreateMap<CompanyCommissionsDto, CompanyCommissions>()
            .ForMember(d => d.Company, o => o.Ignore())
            .ForMember(d => d.ConcurrencyToken, o => o.Ignore());

        CreateMap<CreateCompanyCommissionsCommand, CompanyCommissions>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Company, o => o.Ignore())
            .ForMember(d => d.ConcurrencyToken, o => o.Ignore());

        CreateMap<UpdateCompanyCommissionsCommand, CompanyCommissions>()
            .ForMember(d => d.CompanyId, o => o.Ignore())
            .ForMember(d => d.Company, o => o.Ignore())
            .ForMember(d => d.ConcurrencyToken, o => o.Ignore());
    }
}