using Capitan360.Domain.Entities.CompanyInsurances;

namespace Capitan360.Domain.Interfaces.Repositories.CompanyInsurances;

public interface ICompanyInsuranceChargePaymentContentTypeRepository
{
    Task<List<int>> CreateInsuranceChargePayment(List<CompanyInsuranceChargePaymentContentType> chargePaymentsContentTypes,
    CancellationToken cancellationToken);

    Task Delete(List<int> ids, CancellationToken cancellationToken);

    Task<List<int>> Update(List<CompanyInsuranceChargePaymentContentType> chargePaymentContentTypes, CancellationToken cancellationToken);

    Task<List<int>> ChargePaymentContentTypeReferenceIds(List<int> companyInsuranceChargeContentTypeIds,
        CancellationToken cancellationToken);
}