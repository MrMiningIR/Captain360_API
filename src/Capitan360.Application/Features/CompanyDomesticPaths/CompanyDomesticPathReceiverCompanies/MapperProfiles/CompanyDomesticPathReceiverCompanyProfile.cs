using AutoMapper;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Commands.Create;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Commands.Update;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Dtos;
using Capitan360.Domain.Entities.CompanyDomesticPaths;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.MapperProfiles;

public class CompanyDomesticPathReceiverCompanyProfile : Profile
{
    public CompanyDomesticPathReceiverCompanyProfile()
    {
        // Entity -> DTO
        CreateMap<CompanyDomesticPathReceiverCompany, CompanyDomesticPathReceiverCompanyDto>()
            .ForMember(d => d.CompanyDomesticPathDestinationCityName,
                o => o.MapFrom(s =>
                    s.CompanyDomesticPath != null
                        ? s.CompanyDomesticPath.DestinationCity != null
                            ? s.CompanyDomesticPath.DestinationCity.PersianName
                            : null
                        : null))
            .ForMember(d => d.MunicipalAreaName,
                o => o.MapFrom(s => s.MunicipalArea != null ? s.MunicipalArea.PersianName : null))
            .ForMember(d => d.ReceiverCompanyName,
                o => o.MapFrom(s => s.ReceiverCompany != null ? s.ReceiverCompany.Name : null));

        CreateMap<CreateCompanyDomesticPathReceiverCompanyCommand, CompanyDomesticPathReceiverCompany>()
            .ForMember(e => e.CompanyDomesticPath, o => o.Ignore())
            .ForMember(e => e.MunicipalArea, o => o.Ignore())
            .ForMember(e => e.ReceiverCompany, o => o.Ignore())
            .ForMember(e => e.Id, o => o.Ignore())
            .ForMember(e => e.ConcurrencyToken, o => o.Ignore());

        CreateMap<UpdateCompanyDomesticPathReceiverCompanyCommand, CompanyDomesticPathReceiverCompany>()
            .ForMember(e => e.CompanyDomesticPath, o => o.Ignore())
            .ForMember(e => e.MunicipalArea, o => o.Ignore())
            .ForMember(e => e.ReceiverCompany, o => o.Ignore())
            .ForMember(e => e.ConcurrencyToken, o => o.Ignore())
            .ForMember(e => e.Id, o => o.Ignore());
    }
}
