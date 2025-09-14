using AutoMapper;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Commands.CreateCompanyDomesticPath;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Commands.UpdateCompanyDomesticPath;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Dtos;
using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.MapperProfiles;

public class CompanyDomesticPathsProfile : Profile
{
    public CompanyDomesticPathsProfile()
    {
        CreateMap<CreateCompanyDomesticPathCommand, CompanyDomesticPaths>();
        CreateMap<UpdateCompanyDomesticPathCommand, CompanyDomesticPaths>();
        CreateMap<CompanyDomesticPaths, CompanyDomesticPathDto>()
            .ForMember(des => des.SourceCountryName, opt => opt.MapFrom(src => src.SourceCountry.PersianName))
            .ForMember(des => des.SourceProvinceName, opt => opt.MapFrom(src => src.SourceProvince.PersianName))
            .ForMember(des => des.SourceCityName, opt => opt.MapFrom(src => src.SourceCity.PersianName))
            .ForMember(des => des.DestinationCountryName, opt => opt.MapFrom(src => src.DestinationCountry.PersianName))
            .ForMember(des => des.DestinationProvinceName, opt => opt.MapFrom(src => src.DestinationProvince.PersianName))
            .ForMember(des => des.DestinationCityName, opt => opt.MapFrom(src => src.DestinationCity.PersianName))
            
            ;
    }
}