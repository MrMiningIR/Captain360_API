using AutoMapper;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Dtos;
using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.MapperProfiles;

public class CompanyPackageTypeProfile : Profile
{
    public CompanyPackageTypeProfile()
    {
        //CreateMap<UpdateCompanyPackageTypeNameAndDescriptionCommand, CompanyPackageType>()
        //    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.CompanyPackageTypeDescription))
        //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CompanyPackageTypeName)); ;
        CreateMap<CompanyPackageType, CompanyPackageTypeDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PackageTypeName, opt => opt.MapFrom(src => src.PackageType.Name));
    }
}