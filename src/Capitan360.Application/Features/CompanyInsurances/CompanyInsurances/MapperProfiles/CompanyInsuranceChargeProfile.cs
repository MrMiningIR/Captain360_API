using AutoMapper;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsuranceCharges.Commands.Create;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsuranceCharges.Commands.Update;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Commands.Update;
using Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Dtos;
using Capitan360.Domain.Entities.CompanyInsurances;

namespace Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.MapperProfiles;

public class CompanyInsuranceChargeProfile : Profile
{
    public CompanyInsuranceChargeProfile()
    {
        CreateMap<UpdateCompanyInsuranceCommand, CompanyInsurance>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<CompanyInsurance, CompanyInsuranceDto>();

        CreateMap<CompanyInsurance, UpdateCompanyInsuranceCommand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<CreateCompanyInsuranceChargeCommand, CompanyInsuranceCharge>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<UpdateCompanyInsuranceChargeCommand, CompanyInsuranceCharge>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());



        CreateMap<CompanyInsuranceCharge, UpdateCompanyInsuranceChargeCommand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());


        CreateMap<CompanyInsuranceChargePayment, CompanyInsuranceChargePaymentDto>();
        CreateMap<CompanyInsuranceChargePaymentContentType, CompanyInsuranceChargePaymentContentTypeDto>();

        CreateMap<CompanyInsuranceCharge, CompanyInsuranceChargeDto>()
  .ForMember(des => des.CompanyInsuranceChargePaymentsDto, opt
      => opt.MapFrom(src => src.CompanyInsuranceChargePayments))
  .ForMember(des => des.CompanyInsuranceChargePaymentContentTypesDto, opt
      => opt.MapFrom(src => src.CompanyInsuranceChargePaymentContentTypes));


    }
}
