using AutoMapper;
using Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateCompanyContentType;
using Capitan360.Application.Services.CompanyContentTypeService.Dtos;
using Capitan360.Domain.Entities.ContentEntity;

namespace Capitan360.Application.Services.CompanyContentTypeService.MapperProfiles;

public class CompanyContentTypeProfile : Profile
{
    public CompanyContentTypeProfile()
    {
        CreateMap<UpdateCompanyContentTypeCommand, CompanyContentType>();
        CreateMap<CompanyContentType, CompanyContentTypeDto>()
            .ForMember(dest => dest.NewContentTypeName, opt => opt.MapFrom(src => src.ContentTypeName))
            .ForMember(dest => dest.ContentTypeName, opt => opt.MapFrom(src => src.ContentType.ContentTypeName));
    }
}