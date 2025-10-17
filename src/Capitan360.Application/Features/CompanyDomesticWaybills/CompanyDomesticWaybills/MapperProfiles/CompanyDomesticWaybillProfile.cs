using AutoMapper;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Commands.Issue;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Commands.IssueFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Dtos;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.AttachToCompanyManifestFormFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFormDeliveryStateFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFromCompanyManifestFormFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackToReadyStateFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToCollection;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToDelivery;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToDeliveryFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToDistribution;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.Issue;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.IssueFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.Update;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.UpdateFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Dtos;
using Capitan360.Domain.Entities.CompanyDomesticWaybills;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.MapperProfiles;

public class CompanyDomesticWaybillProfile : Profile
{
    public CompanyDomesticWaybillProfile()
    {
        CreateMap<CompanyDomesticWaybill, CompanyDomesticWaybillDto>()
            .ForMember(d => d.CompanySenderName, opt => opt.MapFrom(s => s.CompanySender != null ? s.CompanySender.Name : null))
            .ForMember(d => d.CompanyReceiverName, opt => opt.MapFrom(s => s.CompanyReceiver != null ? s.CompanyReceiver.Name : null))
            .ForMember(d => d.SourceCountryName, opt => opt.MapFrom(s => s.SourceCountry != null ? s.SourceCountry.PersianName : null))
            .ForMember(d => d.SourceProvinceName, opt => opt.MapFrom(s => s.SourceProvince != null ? s.SourceProvince.PersianName : null))
            .ForMember(d => d.SourceCityName, opt => opt.MapFrom(s => s.SourceCity != null ? s.SourceCity.PersianName : null))
            .ForMember(d => d.SourceMunicipalAreaName, opt => opt.MapFrom(s => s.SourceMunicipalArea != null ? s.SourceMunicipalArea.PersianName : null))
            .ForMember(d => d.DestinationCountryName, opt => opt.MapFrom(s => s.DestinationCountry != null ? s.DestinationCountry.PersianName : null))
            .ForMember(d => d.DestinationProvinceName, opt => opt.MapFrom(s => s.DestinationProvince != null ? s.DestinationProvince.PersianName : null))
            .ForMember(d => d.DestinationCityName, opt => opt.MapFrom(s => s.DestinationCity != null ? s.DestinationCity.PersianName : null))
            .ForMember(d => d.DestinationMunicipalAreaName, opt => opt.MapFrom(s => s.DestinationMunicipalArea != null ? s.DestinationMunicipalArea.PersianName : null))
            .ForMember(d => d.CompanyDomesticWaybillPeriodCode, opt => opt.MapFrom(s => s.CompanyDomesticWaybillPeriod != null ? s.CompanyDomesticWaybillPeriod.Code : null))
            .ForMember(d => d.CompanyManifestFormNo, opt => opt.MapFrom(s => s.CompanyManifestForm != null ? s.CompanyManifestForm.No.ToString() : null))
            .ForMember(d => d.CompanyInsuranceName, opt => opt.MapFrom(s => s.CompanyInsurance != null ? s.CompanyInsurance.Name : null))
            .ForMember(d => d.CompanySenderBankName, opt => opt.MapFrom(s => s.CompanySenderBank != null ? s.CompanySenderBank.Name : null))
            .ForMember(d => d.CustomerPanelNameFamily, opt => opt.MapFrom(s => s.CustomerPanel != null ? s.CustomerPanel.NameFamily : null))
            .ForMember(d => d.CompanyReceiverBankName, opt => opt.MapFrom(s => s.CompanyReceiverBank != null ? s.CompanyReceiverBank.Name : null))
            .ForMember(d => d.CompanyReceiverResponsibleCustomerNameFamily, opt => opt.MapFrom(s => s.CompanyReceiverResponsibleCustomer != null ? s.CompanyReceiverResponsibleCustomer.NameFamily : null))
            .ForMember(d => d.BikeDeliveryInSenderCompanyNameFamily, opt => opt.MapFrom(s => s.BikeDeliveryInSenderCompany != null ? s.BikeDeliveryInSenderCompany.NameFamily : null))
            .ForMember(d => d.BikeDeliveryInReceiverCompanyNameFamily, opt => opt.MapFrom(s => s.BikeDeliveryInReceiverCompany != null ? s.BikeDeliveryInReceiverCompany.NameFamily : null))
            .ForMember(d => d.CounterNameFamily, opt => opt.MapFrom(s => s.Counter != null ? s.Counter.NameFamily : null))
            .ForMember(d => d.TypeOfFactorInSamanehMoadian, opt => opt.MapFrom(s => s.TypeOfFactorInSamanehMoadianId))
            .ForMember(d => d.WaybillSate, opt => opt.MapFrom(s => s.State));

        CreateMap<CompanyDomesticWaybillPackageType, CompanyDomesticWaybillPackageTypeDto>()
            .ForMember(d => d.CompanyDomesticWaybillNo, opt => opt.MapFrom(s => s.CompanyDomesticWaybill != null ? s.CompanyDomesticWaybill.No.ToString() : null))
            .ForMember(d => d.CompanyPackageTypeName, opt => opt.MapFrom(s => s.CompanyPackageType != null ? s.CompanyPackageType.Name : null))
            .ForMember(d => d.CompanyContentTypeName, opt => opt.MapFrom(s => s.CompanyContentType != null ? s.CompanyContentType.Name : null));

        CreateMap<IssueCompanyDomesticWaybillCommand, CompanyDomesticWaybill>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.ConcurrencyToken, opt => opt.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillPackageTypes, opt => opt.MapFrom(s => s.CompanyDomesticWaybillPackageTypes))
            .ForMember(d => d.CompanyManifestFormId, opt => opt.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillPeriodId, opt => opt.Ignore());

        CreateMap<IssueCompanyDomesticWaybillPackageTypeCommand, CompanyDomesticWaybillPackageType>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.ConcurrencyToken, opt => opt.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillId, opt => opt.Ignore())
            .ForMember(d => d.CompanyDomesticWaybill, opt => opt.Ignore());

        CreateMap<UpdateCompanyDomesticWaybillCommand, CompanyDomesticWaybill>()
            .ForMember(d => d.ConcurrencyToken, opt => opt.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillPackageTypes, opt => opt.MapFrom(s => s.CompanyDomesticWaybillPackageTypes));

        CreateMap<IssueCompanyDomesticWaybillFromDesktopCommand, CompanyDomesticWaybill>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.ConcurrencyToken, opt => opt.Ignore())
            .ForMember(d => d.No, opt => opt.MapFrom(s => s.No))
            .ForMember(d => d.CompanyDomesticWaybillPackageTypes, opt => opt.MapFrom(s => s.CompanyDomesticWaybillPackageTypes))
            .ForMember(d => d.CompanySenderId, opt => opt.Ignore())
            .ForMember(d => d.CompanyReceiverId, opt => opt.Ignore())
            .ForMember(d => d.CompanyInsuranceId, opt => opt.Ignore())
            .ForMember(d => d.CompanySenderBankId, opt => opt.Ignore())
            .ForMember(d => d.CustomerPanelId, opt => opt.Ignore())
            .ForMember(d => d.State, opt => opt.Ignore());

        CreateMap<IssueCompanyDomesticWaybillPackageTypeFromDesktopCommand, CompanyDomesticWaybillPackageType>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.ConcurrencyToken, opt => opt.Ignore())
            .ForMember(d => d.CompanyDomesticWaybillId, opt => opt.Ignore())
            .ForMember(d => d.CompanyDomesticWaybill, opt => opt.Ignore())
            .ForMember(d => d.CompanyPackageTypeId, opt => opt.Ignore())
            .ForMember(d => d.CompanyContentTypeId, opt => opt.Ignore());

        CreateMap<UpdateCompanyDomesticWaybillFromDesktopCommand, CompanyDomesticWaybill>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.ConcurrencyToken, opt => opt.Ignore())
            .ForMember(d => d.No, opt => opt.MapFrom(s => s.No))
            .ForMember(d => d.CompanyDomesticWaybillPackageTypes, opt => opt.MapFrom(s => s.CompanyDomesticWaybillPackageTypes))
            .ForMember(d => d.CompanySenderId, opt => opt.Ignore())
            .ForMember(d => d.CompanyReceiverId, opt => opt.Ignore())
            .ForMember(d => d.CompanyInsuranceId, opt => opt.Ignore())
            .ForMember(d => d.CompanySenderBankId, opt => opt.Ignore())
            .ForMember(d => d.CustomerPanelId, opt => opt.Ignore());

        CreateMap<ChangeStateCompanyDomesticWaybillToCollectionCommand, CompanyDomesticWaybill>();

        CreateMap<ChangeStateCompanyDomesticWaybillToDistributionCommand, CompanyDomesticWaybill>();

        CreateMap<ChangeStateCompanyDomesticWaybillToDeliveryCommand, CompanyDomesticWaybill>();

        CreateMap<ChangeStateCompanyDomesticWaybillToDeliveryFromDesktopCommand, CompanyDomesticWaybill>()
            .ForMember(d => d.CompanyReceiverId, opt => opt.Ignore())
            .ForMember(d => d.CompanyReceiverBankId, opt => opt.Ignore())
            .ForMember(d => d.CompanyReceiverResponsibleCustomerId, opt => opt.Ignore());

        CreateMap<AttachCompanyDomesticWaybillToCompanyManifestFormFromDesktopCommand, CompanyDomesticWaybill>()
            .ForMember(d => d.CompanyManifestFormId, opt => opt.Ignore());

        CreateMap<BackCompanyDomesticWaybillFormDeliveryStateFromDesktopCommand, CompanyDomesticWaybill>();

        CreateMap<BackCompanyDomesticWaybillFromCompanyManifestFormFromDesktopCommand, CompanyDomesticWaybill>();

        CreateMap<BackCompanyDomesticWaybillToReadyStateFromDesktopCommand, CompanyDomesticWaybill>();
    }
}