using AutoMapper;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.UpdateName;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Dtos;
using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Application.Features.Companies.CompanyContentTypes.MapperProfiles;

public class CompanyContentTypeProfile : Profile
{
    public CompanyContentTypeProfile()
    {
        CreateMap<CompanyContentType, CompanyContentTypeDto>()
            .ForMember(d => d.CompanyName, o => o.MapFrom(s => s.Company != null ? s.Company.Name : null))
            .ForMember(d => d.ContentTypeName, o => o.MapFrom(s => s.ContentType != null ? s.ContentType.Name : null));

        CreateMap<CompanyContentTypeDto, CompanyContentType>()
            .ForMember(d => d.Company, o => o.Ignore())
            .ForMember(d => d.ContentType, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillPackageTypes, o => o.Ignore())
            .ForMember(d => d.ConcurrencyToken, o => o.Ignore());

        CreateMap<UpdateCompanyContentTypeCommand, CompanyContentType>()
            .ForMember(d => d.CompanyId, o => o.Ignore())
            .ForMember(d => d.ContentTypeId, o => o.Ignore())
            .ForMember(d => d.Company, o => o.Ignore())
            .ForMember(d => d.ContentType, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillPackageTypes, o => o.Ignore())
            .ForMember(d => d.ConcurrencyToken, o => o.Ignore())
            .ForAllMembers(o => o.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<UpdateCompanyContentTypeNameCommand, CompanyContentType>()
            .ForMember(d => d.ConcurrencyToken, o => o.Ignore());
    }
}