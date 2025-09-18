using AutoMapper;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyCommissionses.Dtos;

namespace Capitan360.Application.Features.Companies.CompanyCommissionses.MapperProfiles;

public class CompanyCommissionsProfile : Profile
{
    public CompanyCommissionsProfile()
    {
        CreateMap<Domain.Entities.Companies.CompanyCommissions, CompanyCommissionsDto>();
        CreateMap<CreateCompanyCommissionsCommand, Domain.Entities.Companies.CompanyCommissions>();
        CreateMap<UpdateCompanyCommissionsCommand, Domain.Entities.Companies.CompanyCommissions>();
        //CreateMap<UpdateCompanyCommissionsCommand, Domain.Entities.Companies.CompanyCommissions>()
        //    .ForMember(dest => dest.CommissionFromCaptainCargoWebSite, opt => opt.Condition(src => src.CommissionFromCaptainCargoWebSite.HasValue))
        //    .ForMember(dest => dest.CommissionFromCompanyWebSite, opt => opt.Condition(src => src.CommissionFromCompanyWebSite.HasValue))
        //    .ForMember(dest => dest.CommissionFromCaptainCargoWebService, opt => opt.Condition(src => src.CommissionFromCaptainCargoWebService.HasValue))
        //    .ForMember(dest => dest.CommissionFromCompanyWebService, opt => opt.Condition(src => src.CommissionFromCompanyWebService.HasValue))
        //    .ForMember(dest => dest.CommissionFromCaptainCargoPanel, opt => opt.Condition(src => src.CommissionFromCaptainCargoPanel.HasValue))
        //    .ForMember(dest => dest.CommissionFromCaptainCargoDesktop, opt => opt.Condition(src => src.CommissionFromCaptainCargoDesktop.HasValue))
        //    .ForMember(dest => dest.CompanyId, opt => opt.Ignore()) // CompanyId تغییر نمی‌کنه
        //    .ForMember(dest => dest.Company, opt => opt.Ignore());
    }
}