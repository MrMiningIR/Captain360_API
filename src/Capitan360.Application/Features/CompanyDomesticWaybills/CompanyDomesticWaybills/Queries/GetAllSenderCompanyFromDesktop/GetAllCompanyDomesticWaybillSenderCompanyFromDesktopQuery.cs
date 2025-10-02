using Capitan360.Domain.Enums;
using System.ComponentModel;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Queries.GetAllSenderCompanyFromDesktop;

public record GetAllCompanyDomesticWaybillSenderCompanyFromDesktopQuery(
    [property: DefaultValue("")] string SearchPhraseNameFamilySenderyReceiver = "",
    [property: DefaultValue("")] string SearchPhraseMobileSenderyReceiver = "",
    [property: DefaultValue("")] string SortBy = "",
    [property: DefaultValue(0)] long No = 0,
    [property: DefaultValue("")] string CompanySenderCaptain360Code = "",
    [property: DefaultValue("")] string CompanyReceiverUserInsertedCodeName = "",
    [property: DefaultValue("")] string DestinationCityCaptainCargoCode = "",
    [property: DefaultValue(0)] int IsManifested = 0,
    [property: DefaultValue(0)] long CompanyManifestFormNo = 0,
    [property: DefaultValue("")] string StartDate = "",
    [property: DefaultValue("")] string EndDateDate = "",
    [property: DefaultValue(0)] int PaymentType = 0,
    [property: DefaultValue("")] string BankCode = "",
    [property: DefaultValue("")] string ResponsibleCustomerId = "",
    [property: DefaultValue("")] string CustomerPanelNameFamily = "",
    [property: DefaultValue("")] string CustomerPanelMobile = "",
    [property: DefaultValue(0)] short State = 0,
    [property: DefaultValue("")] string BikeName = "",
    [property: DefaultValue(0)] int Lock = 0,
    [property: DefaultValue(0)] int TypeCaptainCargoPrice = 0,
    [property: DefaultValue(0)] int Dirty = 0,
    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(10)] int PageSize = 10,
    [property: DefaultValue(SortDirection.Ascending)] SortDirection SortDirection = SortDirection.Ascending);

