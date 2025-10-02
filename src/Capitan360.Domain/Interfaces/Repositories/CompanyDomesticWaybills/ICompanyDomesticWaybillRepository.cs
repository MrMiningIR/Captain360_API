using Capitan360.Domain.Entities.CompanyDomesticWaybills;

namespace Capitan360.Domain.Interfaces.Repositories.CompanyDomesticWaybills;

public interface ICompanyDomesticWaybillRepository
{
    Task<CompanyDomesticWaybill?> GetCompanyDomesticWaybillByIdAsync(int companyDomesticWaybillId, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, bool tracked, CancellationToken cancellationToken);

    Task<CompanyDomesticWaybill?> GetCompanyDomesticWaybillByNoAsync(long companyDomesticWaybillNo, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, bool tracked, CancellationToken cancellationToken);

    Task<CompanyDomesticWaybill?> GetCompanyDomesticWaybillByNoAndCompanySenderIdAsync(long companyDomesticWaybillNo, int companySenderId, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, bool tracked, CancellationToken cancellationToken);

    Task<CompanyDomesticWaybill?> GetCompanyDomesticWaybillByNoAndCompanyReceiverIdAsync(long companyDomesticWaybillNo, int companyReceiverId, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, bool tracked, CancellationToken cancellationToken);

    Task<bool> CheckExistCompanyDomesticWaybillByIdAsync(int companyDomesticWaybillId, CancellationToken cancellationToken);

    Task<bool> CheckExistCompanyDomesticWaybillByNoAsync(long companyDomesticWaybillNo, CancellationToken cancellationToken);

    Task<bool> CheckExistCompanyDomesticWaybillByNoAndCompanySenderIdAsync(long companyDomesticWaybillNo, int companySenderId, CancellationToken cancellationToken);

    Task<bool> CheckExistCompanyDomesticWaybillByNoAndCompanyReceiverIdAsync(long companyDomesticWaybillNo, int companyReceiverId, CancellationToken cancellationToken);

    Task<int> InsertCompanyDomesticWaybillAsync(CompanyDomesticWaybill companyDomesticWaybill, CancellationToken cancellationToken);

    Task DeleteCompanyDomesticWaybillAsync(int companyDomesticWaybillId, CancellationToken cancellationToken);

    Task<List<int>> GetCompanyDomesticWaybillIdByCompanyManifestFormIdAsync(int companyManifestFormId, CancellationToken cancellationToken);

    Task<List<int>> GetCompanyDomesticWaybillIdByCompanyDomesticWaybillPeriodIdAndLessThanStartNumberAsync(int companyDomesticWaybillPeriodId, long startNumber, CancellationToken cancellationToken);

    Task<List<int>> GetCompanyDomesticWaybillIdByCompanyDomesticWaybillPeriodIdAndGreatherThanEndNumberAsync(int companyDomesticWaybillPeriodId, long endNumber, CancellationToken cancellationToken);

    Task<List<int>> GetCompanyDomesticWaybillIdByCompanyDomesticWaybillPeriodIdAsync(int companyDomesticWaybillPeriodId, CancellationToken cancellationToken);

    Task<bool> IsCompanyManifestStatusConsistentByCompanyManifestFormIdStateAsync(int companyManifestFormId, short state, CancellationToken cancellationToken);

    Task<bool> AnyIssunedCompanyDomesticWaybillByCompanyDomesticPeriodIdStartNumberEndNumberAsync(int companyDomesticWaybillPeriod, long startNumber, long endNumber, CancellationToken cancellationToken);

    Task<bool> AnyIssunedCompanyDomesticWaybillByCompanyDomesticPeriodIdAsync(int companyDomesticWaybillPeriod, CancellationToken cancellationToken);

    Task ChangeStateCompanyDomesticWaybillAsync(List<int> companyDomesticWaybills, short state, CancellationToken cancellationToken);

    Task BackCompanyDomesticWaybillFromStateAsync(List<int> companyDomesticWaybills, short state, CancellationToken cancellationToken);
}
