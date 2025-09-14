using AutoMapper;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Commands.UpdateCompanyInsurance;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Commands.CreateCompanyInsuranceCharge;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Commands.UpdateCompanyInsuranceCharge;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.Dtos;
using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.MapperProfiles;

public class CompanyInsuranceChargeProfile : Profile
{
    public CompanyInsuranceChargeProfile()
    {
        CreateMap<UpdateCompanyInsuranceCommand, Domain.Entities.Companies.CompanyInsurance>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Domain.Entities.Companies.CompanyInsurance, CompanyInsuranceDto>();

        CreateMap<Domain.Entities.Companies.CompanyInsurance, UpdateCompanyInsuranceCommand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<CreateCompanyInsuranceChargeCommand, Domain.Entities.Companies.CompanyInsuranceCharge>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<UpdateCompanyInsuranceChargeCommand, Domain.Entities.Companies.CompanyInsuranceCharge>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());



        CreateMap<Domain.Entities.Companies.CompanyInsuranceCharge, UpdateCompanyInsuranceChargeCommand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());


        CreateMap<CompanyInsuranceChargePayment, CompanyInsuranceChargePaymentDto>();
        CreateMap<CompanyInsuranceChargePaymentContentType, CompanyInsuranceChargePaymentContentTypeDto>();

        CreateMap<Domain.Entities.Companies.CompanyInsuranceCharge, CompanyInsuranceChargeDto>()
  .ForMember(des => des.CompanyInsuranceChargePaymentsDto, opt
      => opt.MapFrom(src => src.CompanyInsuranceChargePayments))
  .ForMember(des => des.CompanyInsuranceChargePaymentContentTypesDto, opt
      => opt.MapFrom(src => src.CompanyInsuranceChargePaymentContentTypes));


    }
}
