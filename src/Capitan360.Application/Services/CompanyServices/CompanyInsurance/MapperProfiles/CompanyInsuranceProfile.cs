using AutoMapper;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Commands.CreateCompanyInsurance;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Commands.UpdateCompanyInsurance;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Commands.CreateCompanyInsuranceCharge;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Commands.UpdateCompanyInsuranceCharge;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.Dtos;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.MapperProfiles;

public class CompanyInsuranceProfile : Profile
{
    public CompanyInsuranceProfile()
    {
        CreateMap<UpdateCompanyInsuranceCommand, Domain.Entities.CompanyEntity.CompanyInsurance>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Domain.Entities.CompanyEntity.CompanyInsurance, CompanyInsuranceDto>();







        CreateMap<Domain.Entities.CompanyEntity.CompanyInsurance, UpdateCompanyInsuranceCommand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());



        // 
        CreateMap<CreateCompanyInsuranceCommand, Domain.Entities.CompanyEntity.CompanyInsurance>();
        CreateMap<CreateCompanyInsuranceChargePaymentCommand, CompanyInsuranceChargePayment>();
        CreateMap<CreateCompanyInsuranceChargePaymentContentTypeCommand, CompanyInsuranceChargePaymentContentType>();
        CreateMap<UpdateCompanyInsuranceChargeCommand, Domain.Entities.CompanyEntity.CompanyInsuranceCharge>();
        CreateMap<UpdateCompanyInsuranceChargePaymentCommand, CompanyInsuranceChargePayment>();
        CreateMap<UpdateCompanyInsuranceChargePaymentContentTypeCommand, CompanyInsuranceChargePaymentContentType>();



    }
}