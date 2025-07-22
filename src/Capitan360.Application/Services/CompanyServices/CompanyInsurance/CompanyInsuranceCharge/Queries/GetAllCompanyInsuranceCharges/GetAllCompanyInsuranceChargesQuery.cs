using System.ComponentModel;
using Capitan360.Domain.Constants;

namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Queries.GetAllCompanyInsuranceCharges;

public record GetAllCompanyInsuranceChargesQuery(
               [property: DefaultValue(null)] string? SearchPhrase,
                [property: DefaultValue(null)] string? SortBy,
               [property: DefaultValue(0)] int CompanyInsuranceId = 0,
                [property: DefaultValue(15)] int PageSize = 15,
              [property: DefaultValue(1)] int PageNumber = 1,
                [property: DefaultValue(SortDirection.Descending)] SortDirection SortDirection = SortDirection.Descending
);



