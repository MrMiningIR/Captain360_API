using AutoMapper;
using Capitan360.Application.Features.Companies.CompanyTypes.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyTypes.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyTypes.Dtos;
using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Application.Features.Companies.CompanyTypes.MapperProfiles;

public class CompanyTypeProfile : Profile
{
    public CompanyTypeProfile()
    {
        CreateMap<CompanyType, CompanyTypeDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<CompanyTypeDto, CompanyType>()
            .ForMember(dest => dest.PackageTypes, opt => opt.Ignore())
            .ForMember(dest => dest.ContentTypes, opt => opt.Ignore())
            .ForMember(dest => dest.Companies, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyToken, opt => opt.Ignore());

        CreateMap<CreateCompanyTypeCommand, CompanyType>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PackageTypes, opt => opt.Ignore())
            .ForMember(dest => dest.ContentTypes, opt => opt.Ignore())
            .ForMember(dest => dest.Companies, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyToken, opt => opt.Ignore());

        CreateMap<UpdateCompanyTypeCommand, CompanyType>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.PackageTypes, opt => opt.Ignore())
            .ForMember(dest => dest.ContentTypes, opt => opt.Ignore())
            .ForMember(dest => dest.Companies, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyToken, opt => opt.Ignore());
    }
}