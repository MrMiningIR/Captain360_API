using AutoMapper;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.AssignMasterWaybill;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.AssignMasterWaybillFromDesktop;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.BackFromAirlineDeliveryStateFromDesktop;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.BackFromReceivedAtReceiverCompanyStateFromDesktop;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.ChangeStateToAirlineDeliveryFromDesktop;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.ChangeStateToReceivedAtReceiverCompanyFromDesktop;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.DetachMasterWaybillFromDesktop;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.Issue;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.IssueFromDesktop;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.Update;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.UpdateFromDesktop;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Dtos;
using Capitan360.Domain.Entities.CompanyManifestForms;
using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.MapperProfiles;

public class CompanyManifestFormProfile : Profile
{
    public CompanyManifestFormProfile()
    {
        CreateMap<CompanyManifestForm, CompanyManifestFormDto>()
            .ForMember(d => d.CompanySenderName, opt => opt.MapFrom(s => s.CompanySender != null ? s.CompanySender.Name : null))
            .ForMember(d => d.CompanyReceiverName, opt => opt.MapFrom(s => s.CompanyReceiver != null ? s.CompanyReceiver.Name : null))
            .ForMember(d => d.SourceCountryName, opt => opt.MapFrom(s => s.SourceCountry != null ? s.SourceCountry.PersianName : null))
            .ForMember(d => d.SourceProvinceName, opt => opt.MapFrom(s => s.SourceProvince != null ? s.SourceProvince.PersianName : null))
            .ForMember(d => d.SourceCityName, opt => opt.MapFrom(s => s.SourceCity != null ? s.SourceCity.PersianName : null))
            .ForMember(d => d.DestinationCountryName, opt => opt.MapFrom(s => s.DestinationCountry != null ? s.DestinationCountry.PersianName : null))
            .ForMember(d => d.DestinationProvinceName, opt => opt.MapFrom(s => s.DestinationProvince != null ? s.DestinationProvince.PersianName : null))
            .ForMember(d => d.DestinationCityName, opt => opt.MapFrom(s => s.DestinationCity != null ? s.DestinationCity.PersianName : null))
            .ForMember(d => d.ManifestFormPeriodId, opt => opt.MapFrom(s => s.CompanyManifestFormPeriodId))
            .ForMember(d => d.ManifestFormPeriodCode, opt => opt.MapFrom(s => s.CompanyManifestFormPeriod != null ? s.CompanyManifestFormPeriod.Code : null))
            .ForMember(d => d.CounterNameFamily, opt => opt.MapFrom(s => s.Counter != null ? s.Counter.FullName : null))
            .ForMember(d => d.CompanyManifestSate, opt => opt.MapFrom(s => (CompanyManifestFormState)s.State));

        CreateMap<IssueCompanyManifestFormCommand, CompanyManifestForm>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.ConcurrencyToken, opt => opt.Ignore())
            .ForMember(d => d.CompanySenderId, opt => opt.Ignore());

        CreateMap<UpdateCompanyManifestFormCommand, CompanyManifestForm>()
            .ForMember(d => d.ConcurrencyToken, opt => opt.Ignore());

        CreateMap<AssignMasterWaybillToCompanyManifestFormCommand, CompanyManifestForm>();

        CreateMap<IssueCompanyManifestFormFromDesktopCommand, CompanyManifestForm>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.ConcurrencyToken, opt => opt.Ignore())
            .ForMember(d => d.No, opt => opt.MapFrom(s => s.No))
            .ForMember(d => d.CompanySenderId, opt => opt.Ignore())
            .ForMember(d => d.CompanyReceiverId, opt => opt.Ignore());

        CreateMap<UpdateCompanyManifestFormFromDesktopCommand, CompanyManifestForm>()
            .ForMember(d => d.CompanySenderId, opt => opt.Ignore());

        CreateMap<AssignMasterWaybillToCompanyManifestFormFromDesktopCommand, CompanyManifestForm>()
            .ForMember(d => d.CompanySenderId, opt => opt.Ignore());

        CreateMap<DetachMasterWaybillFromCompanyManifestFormFromDesktopCommand, CompanyManifestForm>();

        CreateMap<BackCompanyManifestFormFromAirlineDeliveryStateFromDesktopCommand, CompanyManifestForm>();

        CreateMap<BackCompanyManifestFormFromReceivedAtReceiverCompanyStateFromDesktopCommand, CompanyManifestForm>();

        CreateMap<ChangeStateCompanyManifestFormToAirlineDeliveryFromDesktopCommand, CompanyManifestForm>();

        CreateMap<ChangeStateCompanyManifestFormToReceivedAtReceiverCompanyFromDesktopCommand, CompanyManifestForm>();
    }
}
