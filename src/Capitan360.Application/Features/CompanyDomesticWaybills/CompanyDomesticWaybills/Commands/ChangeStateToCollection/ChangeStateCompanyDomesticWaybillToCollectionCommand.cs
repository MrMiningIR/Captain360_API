namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToCollection;

public record ChangeStateCompanyDomesticWaybillToCollectionCommand(
    string BikeDeliveryInSenderCompanyId,
    string BikeDeliveryInSenderCompanyAgent) 
    {
        public int Id { get; set; }
    };