using AutoMapper;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.UpdateName;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Dtos;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.UpdateName;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Dtos;
using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.MapperProfiles;

public class CompanyPackageTypeProfile : Profile
{
    public CompanyPackageTypeProfile()
    {
        CreateMap<CompanyPackageType, CompanyPackageTypeDto>()
            .ForMember(d => d.CompanyName, o => o.MapFrom(s => s.Company != null ? s.Company.Name : null))
            .ForMember(d => d.PackageTypeName, o => o.MapFrom(s => s.PackageType != null ? s.PackageType.Name : null));

        CreateMap<CompanyPackageTypeDto, CompanyPackageType>()
            .ForMember(d => d.Company, o => o.Ignore())
            .ForMember(d => d.PackageType, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillPackageTypes, o => o.Ignore())
            .ForMember(d => d.ConcurrencyToken, o => o.Ignore());

        CreateMap<UpdateCompanyPackageTypeCommand, CompanyPackageType>()
            .ForMember(d => d.CompanyId, o => o.Ignore())
            .ForMember(d => d.PackageTypeId, o => o.Ignore())
            .ForMember(d => d.Company, o => o.Ignore())
            .ForMember(d => d.PackageType, o => o.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillPackageTypes, o => o.Ignore())
            .ForMember(d => d.ConcurrencyToken, o => o.Ignore())
            .ForAllMembers(o => o.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<UpdateCompanyPackageTypeNameCommand, CompanyPackageType>()
            .ForMember(d => d.ConcurrencyToken, o => o.Ignore());
    }
}