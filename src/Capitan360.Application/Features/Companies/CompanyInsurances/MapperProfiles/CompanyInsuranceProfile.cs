using AutoMapper;
using Capitan360.Application.Features.Companies.CompanyInsuranceCharges.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyInsuranceCharges.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyInsurances.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyInsurances.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyInsurances.Dtos;
using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Application.Features.Companies.CompanyInsurances.MapperProfiles;

public class CompanyInsuranceProfile : Profile
{
    public CompanyInsuranceProfile()
    {
        CreateMap<UpdateCompanyInsuranceCommand, Domain.Entities.Companies.CompanyInsurance>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Domain.Entities.Companies.CompanyInsurance, CompanyInsuranceDto>();







        CreateMap<Domain.Entities.Companies.CompanyInsurance, UpdateCompanyInsuranceCommand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());



        // 
        CreateMap<CreateCompanyInsuranceCommand, Domain.Entities.Companies.CompanyInsurance>();
        CreateMap<CreateCompanyInsuranceChargePaymentCommand, CompanyInsuranceChargePayment>();
        CreateMap<CreateCompanyInsuranceChargePaymentContentTypeCommand, CompanyInsuranceChargePaymentContentType>();
        CreateMap<UpdateCompanyInsuranceChargeCommand, Domain.Entities.Companies.CompanyInsuranceCharge>();
        CreateMap<UpdateCompanyInsuranceChargePaymentCommand, CompanyInsuranceChargePayment>();
        CreateMap<UpdateCompanyInsuranceChargePaymentContentTypeCommand, CompanyInsuranceChargePaymentContentType>();



    }
}