using AutoMapper;
using Capitan360.Application.Services.PackageTypeService.Commands.CreatePackageType;
using Capitan360.Application.Services.PackageTypeService.Commands.UpdatePackageType;
using Capitan360.Application.Services.PackageTypeService.Dtos;
using Capitan360.Domain.Entities.PackageTypes;

namespace Capitan360.Application.Services.PackageTypeService.MapperProfiles;



public class PackageTypeProfile : Profile
{
    public PackageTypeProfile()
    {
        CreateMap<CreatePackageTypeCommand, PackageType>();
        CreateMap<UpdatePackageTypeCommand, PackageType>();
        CreateMap<PackageType, PackageTypeDto>()
            .ForMember(des => des.CompanyTypeName, opt => opt.MapFrom(des => des.CompanyType.DisplayName));
    }
}