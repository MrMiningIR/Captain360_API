using AutoMapper;
using Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateCompanyContentTypeName;
using Capitan360.Application.Services.CompanyContentTypeService.Dtos;
using Capitan360.Domain.Entities.ContentEntity;

namespace Capitan360.Application.Services.CompanyContentTypeService.MapperProfiles;

public class CompanyContentTypeProfile : Profile
{
    public CompanyContentTypeProfile()
    {
        CreateMap<UpdateCompanyContentTypeNameAndDescriptionCommand, CompanyContentType>()
             .ForMember(dest => dest.ContentTypeName, opt => opt.MapFrom(src => src.ContentTypeName))
 .ForMember(dest => dest.CompanyContentTypeDescription, opt => opt.MapFrom(src => src.ContentTypeDescription));
        CreateMap<CompanyContentType, CompanyContentTypeDto>()
            .ForMember(dest => dest.NewCompanyContentTypeName, opt => opt.MapFrom(src => src.ContentTypeName))
            .ForMember(dest => dest.ContentTypeName, opt => opt.MapFrom(src => src.ContentType.ContentTypeName));
    }
}