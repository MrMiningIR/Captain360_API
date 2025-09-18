using AutoMapper;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.UpdateNameAndDescription;
using Capitan360.Application.Features.Companies.CompanyContentTypes.Dtos;
using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Application.Features.Companies.CompanyContentTypes.MapperProfiles;

public class CompanyContentTypeProfile : Profile
{
    public CompanyContentTypeProfile()
    {
        CreateMap<UpdateCompanyContentTypeNameCommand, CompanyContentType>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CompanyContentTypeName));

        CreateMap<CompanyContentType, CompanyContentTypeDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ContentTypeName, opt => opt.MapFrom(src => src.ContentType.Name));
    }
}