using AutoMapper;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.CreateCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.UpdateCompanyCommissions;
using Capitan360.Application.Services.CompanyServices.CompanyCommissions.Dtos;

namespace Capitan360.Application.Services.CompanyServices.CompanyCommissions.MapperProfiles;

public class CompanyCommissionsProfile : Profile
{
    public CompanyCommissionsProfile()
    {
        CreateMap<CreateCompanyCommissionsCommand, Domain.Entities.CompanyEntity.CompanyCommissions>();
        CreateMap<UpdateCompanyCommissionsCommand, Domain.Entities.CompanyEntity.CompanyCommissions>()
            .ForMember(dest => dest.CommissionFromCaptainCargoWebSite, opt => opt.Condition(src => src.CommissionFromCaptainCargoWebSite.HasValue))
            .ForMember(dest => dest.CommissionFromCompanyWebSite, opt => opt.Condition(src => src.CommissionFromCompanyWebSite.HasValue))
            .ForMember(dest => dest.CommissionFromCaptainCargoWebService, opt => opt.Condition(src => src.CommissionFromCaptainCargoWebService.HasValue))
            .ForMember(dest => dest.CommissionFromCompanyWebService, opt => opt.Condition(src => src.CommissionFromCompanyWebService.HasValue))
            .ForMember(dest => dest.CommissionFromCaptainCargoPanel, opt => opt.Condition(src => src.CommissionFromCaptainCargoPanel.HasValue))
            .ForMember(dest => dest.CommissionFromCaptainCargoDesktop, opt => opt.Condition(src => src.CommissionFromCaptainCargoDesktop.HasValue))
            .ForMember(dest => dest.CompanyId, opt => opt.Ignore()) // CompanyId تغییر نمی‌کنه
            .ForMember(dest => dest.Company, opt => opt.Ignore());
        CreateMap<Domain.Entities.CompanyEntity.CompanyCommissions, CompanyCommissionsDto>();
    }
}