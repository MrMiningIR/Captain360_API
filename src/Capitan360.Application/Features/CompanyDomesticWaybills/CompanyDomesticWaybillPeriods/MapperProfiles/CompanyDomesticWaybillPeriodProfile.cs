using AutoMapper;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPeriods.Commands.Create;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPeriods.Commands.Update;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPeriods.Dtos;
using Capitan360.Domain.Entities.CompanyDomesticWaybills;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPeriods.MapperProfiles;

public class CompanyDomesticWaybillPeriodProfile : Profile
{
    public CompanyDomesticWaybillPeriodProfile()
    {
        CreateMap<CompanyDomesticWaybillPeriod, CompanyDomesticWaybillPeriodDto>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company != null ? src.Company.Name : null));

        CreateMap<CompanyDomesticWaybillPeriodDto, CompanyDomesticWaybillPeriod>()
            .ForMember(dest => dest.Company, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticWaybills, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyToken, opt => opt.Ignore());

        CreateMap<CreateDomesticWaybillPeriodCommand, CompanyDomesticWaybillPeriod>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticWaybills, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyToken, opt => opt.Ignore());

        CreateMap<UpdateDomesticWaybillPeriodCommand, CompanyDomesticWaybillPeriod>()
            .ForMember(dest => dest.CompanyId, opt => opt.Ignore())
            .ForMember(dest => dest.Company, opt => opt.Ignore())
            .ForMember(dest => dest.CompanyDomesticWaybills, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyToken, opt => opt.Ignore());
    }
}
