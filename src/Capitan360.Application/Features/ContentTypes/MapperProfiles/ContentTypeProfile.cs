using AutoMapper;
using Capitan360.Application.Features.ContentTypes.Dtos;
using Capitan360.Application.Features.ContentTypeService.Commands.CreateContentType;
using Capitan360.Application.Features.ContentTypeService.Commands.Update;
using Capitan360.Domain.Entities.ContentTypes;

namespace Capitan360.Application.Features.ContentTypeService.MapperProfiles;

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