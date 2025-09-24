using AutoMapper;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Commands.Create;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Commands.Update;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Dtos;
using Capitan360.Domain.Entities.CompanyManifestForms;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.MapperProfiles;

public class CompanyManifestFormPeriodProfile : Profile
{
    public CompanyManifestFormPeriodProfile()
    {
        CreateMap<CompanyManifestFormPeriod, CompanyManifestFormPeriodDto>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company != null ? src.Company.Name : null));

        CreateMap<CompanyManifestFormPeriodDto, CompanyManifestFormPeriod>()
            .ForMember(dest => dest.Company, opt => opt.Ignore()) 
            .ForMember(dest => dest.CompanyManifestForms, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyToken, opt => opt.Ignore());

        CreateMap<CreateManifestFormPeriodCommand, CompanyManifestFormPeriod>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) 
            .ForMember(dest => dest.CompanyManifestForms, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyToken, opt => opt.Ignore());

        CreateMap<UpdateManifestFormPeriodCommand, CompanyManifestFormPeriod>()
            .ForMember(dest => dest.CompanyId, opt => opt.Ignore()) 
            .ForMember(dest => dest.Company, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyManifestForms, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyToken, opt => opt.Ignore());
    }
}
