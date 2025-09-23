using AutoMapper;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Commands.Create;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Commands.Update;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Dtos;
using Capitan360.Domain.Entities.CompanyDomesticPaths;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.MapperProfiles;

//public class CompanyDomesticPathStructPricesProfile : Profile
//{
//    public CompanyDomesticPathStructPricesProfile()
//    {
//        CreateMap<CreateCompanyDomesticPathStructPriceItem, CompanyDomesticPathStructPrices>();
//        CreateMap<UpdateCompanyDomesticPathStructPriceCommand, CompanyDomesticPathStructPrices>();
//        CreateMap<CompanyDomesticPathStructPrices, CompanyDomesticPathStructPriceDto>();
//    }
//}

//public class CompanyDomesticPathStructPricesProfile : Profile
//{
//    public CompanyDomesticPathStructPricesProfile()
//    {
//        CreateMap<CreateCompanyDomesticPathStructPriceItem, CompanyDomesticPathStructPrices>();
//        CreateMap<UpdateCompanyDomesticPathStructPriceItem, CompanyDomesticPathStructPrices>()
//            .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
//            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
//            .ForMember(dest => dest.CompanyDomesticPathId, opt => opt.Ignore())
//            .ForMember(dest => dest.PathStructType, opt => opt.Ignore())
//            .ForMember(dest => dest.WeightType, opt => opt.Ignore());
//        CreateMap<CompanyDomesticPathStructPrices, CompanyDomesticPathStructPriceDto>();
//    }
//}

//public class CompanyDomesticPathStructPricesProfile : Profile
//{
//    public CompanyDomesticPathStructPricesProfile()
//    {
//        CreateMap<CreateCompanyDomesticPathStructPriceItem, CompanyDomesticPathStructPrices>();
//        CreateMap<UpdateCompanyDomesticPathStructPriceItem, CompanyDomesticPathStructPrices>()
//            .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
//            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
//            .ForMember(dest => dest.CompanyDomesticPathId, opt => opt.Ignore())
//            .ForMember(dest => dest.PathStructType, opt => opt.Ignore())
//            .ForMember(dest => dest.WeightType, opt => opt.Ignore());
//        CreateMap<CompanyDomesticPathStructPrices, CompanyDomesticPathStructPriceDto>();
//    }
//}
public class CompanyDomesticPathStructPricesProfile : Profile
{
    public CompanyDomesticPathStructPricesProfile()
    {
        CreateMap<CreateCompanyDomesticPathStructPrice, CompanyDomesticPathStructPrice>();
        CreateMap<UpdateCompanyDomesticPathStructPriceItem, CompanyDomesticPathStructPrice>()
            .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id ?? 0)) // Id null به 0 نگاشت می‌شود
            .ForMember(dest => dest.CompanyDomesticPathId, opt => opt.MapFrom(src => src.CompanyDomesticPathId))
            .ForMember(dest => dest.WeightType, opt => opt.MapFrom(src => src.WeightType))
            .ForMember(dest => dest.PathStructType, opt => opt.MapFrom(src => src.PathStructType)); // PathStructType توسط سیستم پر می‌شود
        CreateMap<CompanyDomesticPathStructPrice, CompanyDomesticPathStructPriceDto>();
        CreateMap<CreateCompanyDomesticPathStructPriceMunicipalAreasItem, CompanyDomesticPathStructPriceMunicipalArea>();
        CreateMap<UpdateCompanyDomesticPathStructPriceMunicipalAreasItem, CompanyDomesticPathStructPriceMunicipalArea>();

        CreateMap<CompanyDomesticPathStructPrice, CompanyDomesticPathStructPriceDto>()
            .ForMember(dest => dest.CompanyDomesticPathStructPriceMunicipalAreasDtos,
                opt => opt.MapFrom(src => src.CompanyDomesticPathStructPriceMunicipalAreas ?? new List<CompanyDomesticPathStructPriceMunicipalArea>()));

        CreateMap<CompanyDomesticPathStructPriceMunicipalArea, CompanyDomesticPathStructPriceMunicipalAreasDto>();


    }
}

