using AutoMapper;
using Capitan360.Application.Features.PackageTypes.Dtos;
using Capitan360.Application.Features.PackageTypeService.Commands.CreatePackageType;
using Capitan360.Application.Features.PackageTypeService.Commands.Update;
using Capitan360.Domain.Entities.PackageTypes;

namespace Capitan360.Application.Features.PackageTypeService.MapperProfiles;



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