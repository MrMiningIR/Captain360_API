using Capitan360.Domain.Constants;
using System.ComponentModel;

namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Queries.GetAllCompanyInsurances;

public record GetAllCompanyInsurancesQuery(
    [property: DefaultValue(1)] int Active = 1,
    [property: DefaultValue(null)] string? SearchPhrase = null,
    [property: DefaultValue(0)] int CompanyTypeId = 0,
    [property: DefaultValue(0)] int CompanyId = 0,
    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(10)] int PageSize = 10,
    [property: DefaultValue(null)] string? SortBy = null,
    [property: DefaultValue(SortDirection.Descending)]
    SortDirection SortDirection = SortDirection.Descending);



