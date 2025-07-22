using AutoMapper;
using Capitan360.Application.Services.CompanyPackageTypeService.Commands.UpdateCompanyPackageType;
using Capitan360.Application.Services.CompanyPackageTypeService.Dtos;
using Capitan360.Domain.Entities.PackageEntity;

namespace Capitan360.Application.Services.CompanyPackageTypeService.MapperProfiles;

public class CompanyPackageTypeProfile : Profile
{
    public CompanyPackageTypeProfile()
    {
        CreateMap<UpdateCompanyPackageTypeCommand, CompanyPackageType>();
        CreateMap<CompanyPackageType, CompanyPackageTypeDto>()
            .ForMember(dest => dest.NewPackageTypeName, opt => opt.MapFrom(src => src.PackageTypeName))
            .ForMember(dest => dest.PackageTypeName, opt => opt.MapFrom(src => src.PackageType.PackageTypeName))


            ;
    }
}