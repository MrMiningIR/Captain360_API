using AutoMapper;
using Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateCompanyContentTypeNameAndDescription;
using Capitan360.Application.Services.CompanyContentTypeService.Dtos;
using Capitan360.Domain.Entities.CompanyContentEntity;

namespace Capitan360.Application.Services.CompanyContentTypeService.MapperProfiles;

public class CompanyContentTypeProfile : Profile
{
    public CompanyContentTypeProfile()
    {
        CreateMap<UpdateCompanyContentTypeNameAndDescriptionCommand, CompanyContentType>()
             .ForMember(dest => dest.CompanyContentTypeName, opt => opt.MapFrom(src => src.CompanyContentTypeName))
 .ForMember(dest => dest.CompanyContentTypeDescription, opt => opt.MapFrom(src => src.CompanyContentTypeDescription));
        CreateMap<CompanyContentType, CompanyContentTypeDto>()
            .ForMember(dest => dest.NewCompanyContentTypeName, opt => opt.MapFrom(src => src.CompanyContentTypeName))
            .ForMember(dest => dest.CompanyContentTypeName, opt => opt.MapFrom(src => src.ContentType.ContentTypeName));
    }
}