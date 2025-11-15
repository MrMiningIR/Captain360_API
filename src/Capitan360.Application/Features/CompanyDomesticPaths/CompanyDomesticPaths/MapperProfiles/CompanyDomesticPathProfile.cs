using AutoMapper;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.Create;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.Update;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Dtos;
using Capitan360.Domain.Entities.Addresses;
using Capitan360.Domain.Entities.CompanyDomesticPaths;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.MapperProfiles;

public class CompanyDomesticPathProfile : Profile
{
    public CompanyDomesticPathProfile()
    {
        CreateMap<CompanyDomesticPath, CompanyDomesticPathDto>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company != null ? src.Company.Name : null))
            .ForMember(dest => dest.SourceCountryName, opt => opt.MapFrom(src => src.SourceCountry != null ? src.SourceCountry.PersianName : null))
            .ForMember(dest => dest.SourceProvinceName, opt => opt.MapFrom(src => src.SourceProvince != null ? src.SourceProvince.PersianName : null))
            .ForMember(dest => dest.SourceCityName, opt => opt.MapFrom(src => src.SourceCity != null ? src.SourceCity.PersianName : null))
            .ForMember(dest => dest.DestinationCountryName, opt => opt.MapFrom(src => src.DestinationCountry != null ? src.DestinationCountry.PersianName : null))
            .ForMember(dest => dest.DestinationProvinceName, opt => opt.MapFrom(src => src.DestinationProvince != null ? src.DestinationProvince.PersianName : null))
            .ForMember(dest => dest.DestinationCityName, opt => opt.MapFrom(src => src.DestinationCity != null ? src.DestinationCity.PersianName : null));

        CreateMap<CompanyDomesticPathDto, CompanyDomesticPath>()
            .ForMember(dest => dest.Company, opt => opt.Ignore())
            .ForMember(dest => dest.SourceCountry, opt => opt.Ignore())
            .ForMember(dest => dest.SourceProvince, opt => opt.Ignore())
            .ForMember(dest => dest.SourceCity, opt => opt.Ignore())
            .ForMember(dest => dest.DestinationCountry, opt => opt.Ignore())
            .ForMember(dest => dest.DestinationProvince, opt => opt.Ignore())
            .ForMember(dest => dest.DestinationCity, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticPathStructPrices, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticPathCharges, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticPathStructPriceMunicipalAreas, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticPathChargeContentTypes, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyToken, opt => opt.Ignore());

        CreateMap<CreateCompanyDomesticPathCommand, CompanyDomesticPath>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticPathStructPrices, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticPathCharges, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticPathStructPriceMunicipalAreas, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticPathChargeContentTypes, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyToken, opt => opt.Ignore());

        CreateMap<UpdateCompanyDomesticPathCommand, CompanyDomesticPath>()
            .ForMember(dest => dest.CompanyId, opt => opt.Ignore())
            .ForMember(dest => dest.Company, opt => opt.Ignore())
            .ForMember(dest => dest.SourceCountry, opt => opt.Ignore())
            .ForMember(dest => dest.SourceProvince, opt => opt.Ignore())
            .ForMember(dest => dest.SourceCity, opt => opt.Ignore())
            .ForMember(dest => dest.DestinationCountry, opt => opt.Ignore())
            .ForMember(dest => dest.DestinationProvince, opt => opt.Ignore())
            .ForMember(dest => dest.DestinationCity, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticPathStructPrices, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticPathCharges, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticPathStructPriceMunicipalAreas, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticPathChargeContentTypes, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyToken, opt => opt.Ignore());

        CreateMap<Area, CompanyDomesticPathReceiverCompany>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticPathId, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticPath, opt => opt.Ignore())
            .ForMember(dest => dest.MunicipalAreaId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.MunicipalArea, opt => opt.Ignore())
            .ForMember(dest => dest.ReceiverCompanyId, opt => opt.Ignore())
            .ForMember(dest => dest.ReceiverCompany, opt => opt.Ignore())
            .ForMember(dest => dest.ReceiverCompanyUserInsertedCode, opt => opt.Ignore())
            .ForMember(dest => dest.ReceiverCompanyUserInsertedName, opt => opt.Ignore())
            .ForMember(dest => dest.ReceiverCompanyUserInsertedTelephone, opt => opt.Ignore())
            .ForMember(dest => dest.ReceiverCompanyUserInsertedAddress, opt => opt.Ignore())
            .ForMember(dest => dest.DescriptionForPrint1, opt => opt.MapFrom(src => string.Empty))
            .ForMember(dest => dest.DescriptionForPrint2, opt => opt.MapFrom(src => string.Empty))
            .ForMember(dest => dest.DescriptionForPrint3, opt => opt.MapFrom(src => string.Empty));
    }
}