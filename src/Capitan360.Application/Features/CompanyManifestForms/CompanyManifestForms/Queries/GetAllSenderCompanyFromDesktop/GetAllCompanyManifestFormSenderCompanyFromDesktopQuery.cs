using Capitan360.Domain.Enums;
using System.ComponentModel;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Queries.GetAllSenderCompanyFromDesktop;

public record GetAllCompanyManifestFormSenderCompanyFromDesktopQuery(
    [property: DefaultValue("")] string SearchPhrase = "",
    [property: DefaultValue("")] string SortBy = "",
    [property: DefaultValue(0)] long No = 0,
    [property: DefaultValue("")] string CompanySenderCaptain360Code = "",
    [property: DefaultValue("")] string CompanyReceiverUserInsertedCodeName = "",
    [property: DefaultValue("")] string DestinationCityCaptainCargoCode = "",
    [property: DefaultValue("")] string StartDate = "",
    [property: DefaultValue("")] string EndDateDate = "",
    [property: DefaultValue(0)] short State = 0,
    [property: DefaultValue(0)] int Dirty = 0,
    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(10)] int PageSize = 10,
    [property: DefaultValue(SortDirection.Ascending)] SortDirection SortDirection = SortDirection.Ascending);
