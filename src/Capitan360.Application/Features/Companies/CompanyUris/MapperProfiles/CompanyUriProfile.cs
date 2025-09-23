using AutoMapper;
using Capitan360.Application.Features.Companies.CompanyUris.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyUris.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyUris.Dtos;

namespace Capitan360.Application.Features.Companies.CompanyUri.MapperProfiles;

public class CompanyUriProfile : Profile
{
    public CompanyUriProfile()
    {
        CreateMap<CreateCompanyUriCommand, Domain.Entities.Companies.CompanyUri>();
        CreateMap<UpdateCompanyUriCommand, Domain.Entities.Companies.CompanyUri>();

        CreateMap<Domain.Entities.Companies.CompanyUri, CompanyUriDto>();
    }
}