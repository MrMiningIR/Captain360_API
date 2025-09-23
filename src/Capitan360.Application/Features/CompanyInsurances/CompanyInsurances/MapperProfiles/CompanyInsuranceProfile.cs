using AutoMapper;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsuranceCharges.Commands.Create;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsuranceCharges.Commands.Update;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Commands.Create;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Commands.Update;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Dtos;
using Capitan360.Domain.Entities.CompanyInsurances;

namespace Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.MapperProfiles;

public class CompanyInsuranceProfile : Profile
{
    public CompanyInsuranceProfile()
    {
        CreateMap<UpdateCompanyInsuranceCommand, CompanyInsurance>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<CompanyInsurance, CompanyInsuranceDto>();







        CreateMap<CompanyInsurance, UpdateCompanyInsuranceCommand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());



        // 
        CreateMap<CreateCompanyInsuranceCommand, CompanyInsurance>();
        CreateMap<CreateCompanyInsuranceChargePaymentCommand, CompanyInsuranceChargePayment>();
        CreateMap<CreateCompanyInsuranceChargePaymentContentTypeCommand, CompanyInsuranceChargePaymentContentType>();
        CreateMap<UpdateCompanyInsuranceChargeCommand, CompanyInsuranceCharge>();
        CreateMap<UpdateCompanyInsuranceChargePaymentCommand, CompanyInsuranceChargePayment>();
        CreateMap<UpdateCompanyInsuranceChargePaymentContentTypeCommand, CompanyInsuranceChargePaymentContentType>();



    }
}