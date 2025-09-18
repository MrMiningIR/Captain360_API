using AutoMapper;
using Capitan360.Application.Features.Companies.CompanyDomesticPaths.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyDomesticPaths.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyDomesticPaths.Dtos;
using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Application.Features.Companies.CompanyDomesticPaths.MapperProfiles;

public class CompanyDomesticPathsProfile : Profile
{
    public CompanyDomesticPathsProfile()
    {
        CreateMap<CreateCompanyDomesticPathCommand, CompanyDomesticPath>();
        CreateMap<UpdateCompanyDomesticPathCommand, CompanyDomesticPath>();
        CreateMap<CompanyDomesticPath, CompanyDomesticPathDto>()
            .ForMember(des => des.SourceCountryName, opt => opt.MapFrom(src => src.SourceCountry.PersianName))
            .ForMember(des => des.SourceProvinceName, opt => opt.MapFrom(src => src.SourceProvince.PersianName))
            .ForMember(des => des.SourceCityName, opt => opt.MapFrom(src => src.SourceCity.PersianName))
            .ForMember(des => des.DestinationCountryName, opt => opt.MapFrom(src => src.DestinationCountry.PersianName))
            .ForMember(des => des.DestinationProvinceName, opt => opt.MapFrom(src => src.DestinationProvince.PersianName))
            .ForMember(des => des.DestinationCityName, opt => opt.MapFrom(src => src.DestinationCity.PersianName))
            
            ;
    }
}