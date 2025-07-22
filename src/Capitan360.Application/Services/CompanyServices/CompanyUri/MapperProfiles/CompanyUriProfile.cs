using AutoMapper;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.CreateCompanyUri;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.UpdateCompanyUri;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Dtos;

namespace Capitan360.Application.Services.CompanyServices.CompanyUri.MapperProfiles;

public class CompanyUriProfile : Profile
{
    public CompanyUriProfile()
    {
        CreateMap<CreateCompanyUriCommand, Domain.Entities.CompanyEntity.CompanyUri>();
        CreateMap<UpdateCompanyUriCommand, Domain.Entities.CompanyEntity.CompanyUri>();

        CreateMap<Domain.Entities.CompanyEntity.CompanyUri, CompanyUriDto>();
    }
}