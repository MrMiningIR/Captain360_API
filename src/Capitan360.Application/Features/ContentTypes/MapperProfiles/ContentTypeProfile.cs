using AutoMapper;
using Capitan360.Application.Features.ContentTypes.Commands.Create;
using Capitan360.Application.Features.ContentTypes.Commands.Update;
using Capitan360.Application.Features.ContentTypes.Dtos;
using Capitan360.Domain.Entities.ContentTypes;

namespace Capitan360.Application.Features.ContentTypes.MapperProfiles;

public class ContentTypeProfile : Profile
{
    public ContentTypeProfile()
    {
        CreateMap<ContentType, ContentTypeDto>()
            .ForMember(
                dest => dest.CompanyTypeName,
                opt => opt.MapFrom(src =>
                    src.CompanyType != null ? src.CompanyType.DisplayName : null
                )
            );

        CreateMap<ContentTypeDto, ContentType>()
            .ForMember(dest => dest.CompanyType, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyContentTypes, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyToken, opt => opt.Ignore());

        CreateMap<CreateContentTypeCommand, ContentType>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyType, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyContentTypes, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyToken, opt => opt.Ignore());

        CreateMap<UpdateContentTypeCommand, ContentType>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CompanyTypeId, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyType, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyContentTypes, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyToken, opt => opt.Ignore());
    }

}