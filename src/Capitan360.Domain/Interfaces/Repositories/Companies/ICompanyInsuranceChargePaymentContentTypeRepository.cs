using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Domain.Repositories.Companies;

public interface ICompanyInsuranceChargePaymentContentTypeRepository
{
    Task<List<int>> CreateInsuranceChargePayment(List<CompanyInsuranceChargePaymentContentType> chargePaymentsContentTypes,
    CancellationToken cancellationToken);

    Task Delete(List<int> ids, CancellationToken cancellationToken);

    Task<List<int>> Update(List<CompanyInsuranceChargePaymentContentType> chargePaymentContentTypes, CancellationToken cancellationToken);

    Task<List<int>> ChargePaymentContentTypeReferenceIds(List<int> companyInsuranceChargeContentTypeIds,
        CancellationToken cancellationToken);
}