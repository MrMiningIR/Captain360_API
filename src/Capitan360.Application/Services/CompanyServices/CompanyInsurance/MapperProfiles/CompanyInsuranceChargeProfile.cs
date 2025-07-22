using AutoMapper;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Commands.UpdateCompanyInsurance;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Commands.CreateCompanyInsuranceCharge;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Commands.UpdateCompanyInsuranceCharge;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.Dtos;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.MapperProfiles;

public class CompanyInsuranceChargeProfile : Profile
{
    public CompanyInsuranceChargeProfile()
    {
        CreateMap<UpdateCompanyInsuranceCommand, Domain.Entities.CompanyEntity.CompanyInsurance>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Domain.Entities.CompanyEntity.CompanyInsurance, CompanyInsuranceDto>();

        CreateMap<Domain.Entities.CompanyEntity.CompanyInsurance, UpdateCompanyInsuranceCommand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<CreateCompanyInsuranceChargeCommand, Domain.Entities.CompanyEntity.CompanyInsuranceCharge>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<UpdateCompanyInsuranceChargeCommand, Domain.Entities.CompanyEntity.CompanyInsuranceCharge>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());



        CreateMap<Domain.Entities.CompanyEntity.CompanyInsuranceCharge, UpdateCompanyInsuranceChargeCommand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());


        CreateMap<CompanyInsuranceChargePayment, CompanyInsuranceChargePaymentDto>();
        CreateMap<CompanyInsuranceChargePaymentContentType, CompanyInsuranceChargePaymentContentTypeDto>();

        CreateMap<Domain.Entities.CompanyEntity.CompanyInsuranceCharge, CompanyInsuranceChargeDto>()
  .ForMember(des => des.CompanyInsuranceChargePaymentsDto, opt
      => opt.MapFrom(src => src.CompanyInsuranceChargePayments))
  .ForMember(des => des.CompanyInsuranceChargePaymentContentTypesDto, opt
      => opt.MapFrom(src => src.CompanyInsuranceChargePaymentContentTypes));


    }
}
