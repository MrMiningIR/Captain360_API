using AutoMapper;
using Capitan360.Application.Services.ContentTypeService.Commands.CreateContentType;
using Capitan360.Application.Services.ContentTypeService.Commands.UpdateContentType;
using Capitan360.Application.Services.ContentTypeService.Dtos;
using Capitan360.Domain.Entities.ContentEntity;

namespace Capitan360.Application.Services.ContentTypeService.MapperProfiles;

public class ContentTypeProfile : Profile
{
    public ContentTypeProfile()
    {
        CreateMap<CreateContentTypeCommand, ContentType>();
        CreateMap<UpdateContentTypeCommand, ContentType>();
        CreateMap<ContentType, ContentTypeDto>()
            .ForMember(des => des.CompanyTypeName, opt => opt.MapFrom(des => des.CompanyType.DisplayName));
    }
}