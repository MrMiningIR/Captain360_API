using AutoMapper;
using Capitan360.Application.Services.CompanyContentTypeService.Commands.UpdateCompanyContentTypeNameAndDescription;
using Capitan360.Application.Services.CompanyContentTypeService.Dtos;
using Capitan360.Domain.Entities.CompanyContentEntity;

namespace Capitan360.Application.Services.CompanyContentTypeService.MapperProfiles;

public class CompanyContentTypeProfile : Profile
{
    public CompanyContentTypeProfile()
    {
        CreateMap<UpdateCompanyContentTypeNameCommand, CompanyContentType>()
            .ForMember(dest => dest.CompanyContentTypeName, opt => opt.MapFrom(src => src.CompanyContentTypeName));

        CreateMap<CompanyContentType, CompanyContentTypeDto>()
            .ForMember(dest => dest.CompanyContentTypeName, opt => opt.MapFrom(src => src.CompanyContentTypeName))
            .ForMember(dest => dest.ContentTypeName, opt => opt.MapFrom(src => src.ContentType.ContentTypeName));
    }
}