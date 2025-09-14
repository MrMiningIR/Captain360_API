using AutoMapper;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.CreateCompanyUri;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.UpdateCompanyUri;
using Capitan360.Application.Services.CompanyServices.CompanyUri.Dtos;

namespace Capitan360.Application.Services.CompanyServices.CompanyUri.MapperProfiles;

public class CompanyUriProfile : Profile
{
    public CompanyUriProfile()
    {
        CreateMap<CreateCompanyUriCommand, Domain.Entities.Companies.CompanyUri>();
        CreateMap<UpdateCompanyUriCommand, Domain.Entities.Companies.CompanyUri>();

        CreateMap<Domain.Entities.Companies.CompanyUri, CompanyUriDto>();
    }
}