using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Repositories.CompanyRepo;

public interface ICompanyInsuranceChargePaymentRepository
{
    Task<List<int>> CreateInsuranceChargePayment(List<CompanyInsuranceChargePayment> chargePayments,
        CancellationToken cancellationToken);
    Task Delete(List<int> ids, CancellationToken cancellationToken);

    Task<List<int>> ChargePaymentReferenceIds(List<int> companyInsuranceChargeIds, CancellationToken cancellationToken);

    Task<List<int>> Update(List<CompanyInsuranceChargePayment> chargePayments, CancellationToken cancellationToken);
}