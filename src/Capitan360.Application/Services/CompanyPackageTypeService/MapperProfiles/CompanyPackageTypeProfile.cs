using AutoMapper;
using Capitan360.Application.Services.CompanyPackageTypeService.Commands.UpdateCompanyPackageTypeNameAndDescription;
using Capitan360.Application.Services.CompanyPackageTypeService.Dtos;
using Capitan360.Domain.Entities.PackageEntity;

namespace Capitan360.Application.Services.CompanyPackageTypeService.MapperProfiles;

public class CompanyPackageTypeProfile : Profile
{
    public CompanyPackageTypeProfile()
    {
        CreateMap<UpdateCompanyPackageTypeNameAndDescriptionCommand, CompanyPackageType>()
            .ForMember(dest => dest.CompanyPackageTypeDescription, opt => opt.MapFrom(src => src.CompanyPackageTypeDescription))
.ForMember(dest => dest.PackageTypeName, opt => opt.MapFrom(src => src.CompanyPackageTypeName)); ;
        CreateMap<CompanyPackageType, CompanyPackageTypeDto>()
            .ForMember(dest => dest.NewCompanyPackageTypeName, opt => opt.MapFrom(src => src.PackageTypeName))
            .ForMember(dest => dest.PackageTypeName, opt => opt.MapFrom(src => src.PackageType.PackageTypeName));
    }
}