using AutoMapper;
using Capitan360.Application.Features.Companies.Companies.Commands.Create;
using Capitan360.Application.Features.Companies.Companies.Commands.Update;
using Capitan360.Application.Features.Companies.Companies.Dtos;
using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Application.Features.Companies.Companies.MapperProfiles;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<Company, CompanyDto>()
            .ForMember(
                dest => dest.CompanyTypeName,
                opt => opt.MapFrom(src => src.CompanyType != null ? src.CompanyType.DisplayName : null)
            )
            .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country != null ? src.Country.PersianName : null))
            .ForMember(dest => dest.ProvinceName, opt => opt.MapFrom(src => src.Province != null ? src.Province.PersianName : null))
            .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City != null ? src.City.PersianName : null));

        CreateMap<CompanyDto, Company>()
            .ForMember(dest => dest.CompanyType, opt => opt.Ignore())
            .ForMember(dest => dest.Country, opt => opt.Ignore())
            .ForMember(dest => dest.Province, opt => opt.Ignore())
            .ForMember(dest => dest.City, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyCommissions, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyPreferences, opt => opt.Ignore())
            .ForMember(dest => dest.CompanySmsPatterns, opt => opt.Ignore())

            .ForMember(dest => dest.CompanyUris, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyContentTypes, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyPackageTypes, opt => opt.Ignore())
            .ForMember(dest => dest.Addresses, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticPaths, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyInsurances, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyBanks, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyManifestFormPeriods, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyManifestFormCompanySenders, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyManifestFormCompanyReceivers, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticWaybillPeriods, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticWaybillCompanySenders, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticWaybillCompanyReceivers, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyToken, opt => opt.Ignore());

        CreateMap<CreateCompanyCommand, Company>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyType, opt => opt.Ignore())
            .ForMember(dest => dest.Country, opt => opt.Ignore())
            .ForMember(dest => dest.Province, opt => opt.Ignore())
            .ForMember(dest => dest.City, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyCommissions, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyPreferences, opt => opt.Ignore())
            .ForMember(dest => dest.CompanySmsPatterns, opt => opt.Ignore())

            .ForMember(dest => dest.CompanyUris, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyContentTypes, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyPackageTypes, opt => opt.Ignore())
            .ForMember(dest => dest.Addresses, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticPaths, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyInsurances, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyBanks, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyManifestFormPeriods, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyManifestFormCompanySenders, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyManifestFormCompanyReceivers, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticWaybillPeriods, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticWaybillCompanySenders, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticWaybillCompanyReceivers, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyToken, opt => opt.Ignore());

        CreateMap<UpdateCompanyCommand, Company>()
            .ForMember(dest => dest.CompanyTypeId, opt => opt.Ignore())
            .ForMember(dest => dest.CountryId, opt => opt.Ignore())
            .ForMember(dest => dest.ProvinceId, opt => opt.Ignore())
            .ForMember(dest => dest.CityId, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyType, opt => opt.Ignore())
            .ForMember(dest => dest.Country, opt => opt.Ignore())
            .ForMember(dest => dest.Province, opt => opt.Ignore())
            .ForMember(dest => dest.City, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyCommissions, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyPreferences, opt => opt.Ignore())
            .ForMember(dest => dest.CompanySmsPatterns, opt => opt.Ignore())

            .ForMember(dest => dest.CompanyUris, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyContentTypes, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyPackageTypes, opt => opt.Ignore())
            .ForMember(dest => dest.Addresses, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticPaths, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyInsurances, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyBanks, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyManifestFormPeriods, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyManifestFormCompanySenders, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyManifestFormCompanyReceivers, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticWaybillPeriods, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticWaybillCompanySenders, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticWaybillCompanyReceivers, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyToken, opt => opt.Ignore());
    }
}