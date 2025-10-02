using AutoMapper;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Commands.Issue;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Commands.IssueFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Dtos;
using Capitan360.Domain.Entities.CompanyDomesticWaybills;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.MapperProfiles;

public class CompanyDomesticWaybillPackageTypeProfile : Profile
{
    public CompanyDomesticWaybillPackageTypeProfile()
    {
        CreateMap<CompanyDomesticWaybillPackageType, CompanyDomesticWaybillPackageTypeDto>()
            .ForMember(d => d.CompanyDomesticWaybillNo, o => o.MapFrom(s => s.CompanyDomesticWaybill != null ? s.CompanyDomesticWaybill.No.ToString() : null))
            .ForMember(d => d.CompanyPackageTypeName, o => o.MapFrom(s => s.CompanyPackageType != null ? s.CompanyPackageType.Name : null))
            .ForMember(d => d.CompanyContentTypeName, o => o.MapFrom(s => s.CompanyContentType != null ? s.CompanyContentType.Name : null));

        CreateMap<IssueCompanyDomesticWaybillPackageTypeCommand, CompanyDomesticWaybillPackageType>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillId, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticWaybill, o => o.Ignore())
            .ForMember(d => d.CompanyPackageType, o => o.Ignore())
            .ForMember(d => d.CompanyContentType, o => o.Ignore())
            .ForMember(d => d.ConcurrencyToken, o => o.Ignore());

        CreateMap<IssueCompanyDomesticWaybillPackageTypeFromDesktopCommand, CompanyDomesticWaybillPackageType>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillId, o => o.Ignore())
            .ForMember(d => d.CompanyPackageTypeId, o => o.Ignore())
            .ForMember(d => d.CompanyContentTypeId, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticWaybill, o => o.Ignore())
            .ForMember(d => d.CompanyPackageType, o => o.Ignore())
            .ForMember(d => d.CompanyContentType, o => o.Ignore())
            .ForMember(d => d.ConcurrencyToken, o => o.Ignore());
    }
}