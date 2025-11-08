using Capitan360.Domain.Enums;
using System.ComponentModel;

namespace Capitan360.Application.Features.CompanyInsurances.CompanyInsuranceCharges.Queries.GetAllCompanyInsuranceCharges;

public record GetAllCompanyInsuranceChargesQuery(
               [property: DefaultValue(null)] string? SearchPhrase,
                [property: DefaultValue(null)] string? SortBy,
               [property: DefaultValue(0)] int CompanyInsuranceId = 0,
                [property: DefaultValue(15)] int PageSize = 15,
              [property: DefaultValue(1)] int PageNumber = 1,
                [property: DefaultValue(SortDirection.Descending)] SortDirection SortDirection = SortDirection.Descending
);



