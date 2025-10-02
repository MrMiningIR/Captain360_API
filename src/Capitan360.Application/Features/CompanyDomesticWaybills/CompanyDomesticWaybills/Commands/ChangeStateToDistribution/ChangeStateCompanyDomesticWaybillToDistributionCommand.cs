namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToDistribution;

public record ChangeStateCompanyDomesticWaybillToDistributionCommand(
    string BikeDeliveryInReceiverCompanyId,
    string BikeDeliveryInReceiverCompanyAgent)
    {
        public int Id { get; set; }
    };
