using AutoMapper;
using Capitan360.Application.Features.Companies.CompanySmsPatterns.Commands.CreateCompanySmsPatterns;
using Capitan360.Application.Features.Companies.CompanySmsPatterns.Commands.UpdateCompanySmsPatterns;
using Capitan360.Application.Features.Companies.CompanySmsPatterns.Dtos;

namespace Capitan360.Application.Features.Companies.CompanySmsPatterns.MapperProfiles;

public class CompanySmsPatternsProfile : Profile
{
    public CompanySmsPatternsProfile()
    {
        CreateMap<CreateCompanySmsPatternsCommand, Domain.Entities.Companies.CompanySmsPatterns>();
        CreateMap<UpdateCompanySmsPatternsCommand, Domain.Entities.Companies.CompanySmsPatterns>()
            // .ForMember(dest => dest.CompanyId, opt => opt.Condition(src => src.CompanyId.HasValue))
            .ForMember(dest => dest.PatternSmsIssueSender, opt => opt.Condition(src => src.PatternSmsIssueSender != null))
            .ForMember(dest => dest.PatternSmsIssueReceiver, opt => opt.Condition(src => src.PatternSmsIssueReceiver != null))
            .ForMember(dest => dest.PatternSmsIssueCompany, opt => opt.Condition(src => src.PatternSmsIssueCompany != null))
            .ForMember(dest => dest.PatternSmsSendSenderPeakSender, opt => opt.Condition(src => src.PatternSmsSendSenderPeakSender != null))
            .ForMember(dest => dest.PatternSmsSendSenderPeakReceiver, opt => opt.Condition(src => src.PatternSmsSendSenderPeakReceiver != null))
            .ForMember(dest => dest.PatternSmsPackageInCompanySender, opt => opt.Condition(src => src.PatternSmsPackageInCompanySender != null))
            .ForMember(dest => dest.PatternSmsPackageInCompanyReceiver, opt => opt.Condition(src => src.PatternSmsPackageInCompanyReceiver != null))
            .ForMember(dest => dest.PatternSmsManifestSender, opt => opt.Condition(src => src.PatternSmsManifestSender != null))
            .ForMember(dest => dest.PatternSmsManifestReceiver, opt => opt.Condition(src => src.PatternSmsManifestReceiver != null))
            .ForMember(dest => dest.PatternSmsReceivedInReceiverCompanySender, opt => opt.Condition(src => src.PatternSmsReceivedInReceiverCompanySender != null))
            .ForMember(dest => dest.PatternSmsReceivedInReceiverCompanyReceiver, opt => opt.Condition(src => src.PatternSmsReceivedInReceiverCompanyReceiver != null))
            .ForMember(dest => dest.PatternSmsSendReceiverPeakSender, opt => opt.Condition(src => src.PatternSmsSendReceiverPeakSender != null))
            .ForMember(dest => dest.PatternSmsSendReceiverPeakReceiver, opt => opt.Condition(src => src.PatternSmsSendReceiverPeakReceiver != null))
            .ForMember(dest => dest.PatternSmsDeliverSender, opt => opt.Condition(src => src.PatternSmsDeliverSender != null))
            .ForMember(dest => dest.PatternSmsDeliverReceiver, opt => opt.Condition(src => src.PatternSmsDeliverReceiver != null))
            .ForMember(dest => dest.PatternSmsCancelSender, opt => opt.Condition(src => src.PatternSmsCancelSender != null))
            .ForMember(dest => dest.PatternSmsCancelReceiver, opt => opt.Condition(src => src.PatternSmsCancelReceiver != null))
            .ForMember(dest => dest.PatternSmsCancelByCustomerSender, opt => opt.Condition(src => src.PatternSmsCancelByCustomerSender != null))
            .ForMember(dest => dest.PatternSmsCancelByCustomerReceiver, opt => opt.Condition(src => src.PatternSmsCancelByCustomerReceiver != null))
            .ForMember(dest => dest.PatternSmsCancelByCustomerCompany, opt => opt.Condition(src => src.PatternSmsCancelByCustomerCompany != null))
            .ForMember(dest => dest.PatternSmsSendManifestReceiverCompany, opt => opt.Condition(src => src.PatternSmsSendManifestReceiverCompany != null))
            .ForMember(dest => dest.Company, opt => opt.Ignore());
        CreateMap<Domain.Entities.Companies.CompanySmsPatterns, CompanySmsPatternsDto>();
    }
}