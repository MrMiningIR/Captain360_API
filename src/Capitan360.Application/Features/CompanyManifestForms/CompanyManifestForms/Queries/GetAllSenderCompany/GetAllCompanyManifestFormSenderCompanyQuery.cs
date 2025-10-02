using Capitan360.Domain.Enums;
using System.ComponentModel;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Queries.GetAllSenderCompany;

public record GetAllCompanyManifestFormSenderCompanyQuery(
    [property: DefaultValue("")] string SearchPhrase = "",
    [property: DefaultValue("")] string SortBy = "",
    [property: DefaultValue(0)] long No = 0,
    [property: DefaultValue(0)] int CompanyReceiverId = 0,
    [property: DefaultValue(0)] int DestinationCityId = 0,
    [property: DefaultValue("")] string StartDate = "",
    [property: DefaultValue("")] string EndDateDate = "",
    [property: DefaultValue(0)] short State = 0,
    [property: DefaultValue(0)] int Dirty = 0,
    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(10)] int PageSize = 10,
    [property: DefaultValue(SortDirection.Ascending)] SortDirection SortDirection = SortDirection.Ascending);