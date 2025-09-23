using Capitan360.Domain.Entities.CompanyDomesticWaybills;

namespace Capitan360.Domain.Interfaces.Repositories.CompanyDomesticWaybills;

public interface ICompanyDomesticWaybillRepository
{
    Task<CompanyDomesticWaybill?> GetDomesticWaybillByIdAsync(int companyDomesticWaybillId, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, bool tracked, CancellationToken cancellationToken);

    Task<CompanyDomesticWaybill?> GetDomesticWaybillByNoAsync(long companyDomesticWaybillNo, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, bool tracked, CancellationToken cancellationToken);

    Task<CompanyDomesticWaybill?> GetDomesticWaybillByNoAndCompanySenderIdAsync(long companyDomesticWaybillNo, int companySenderId, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, bool tracked, CancellationToken cancellationToken);

    Task<CompanyDomesticWaybill?> GetDomesticWaybillByNoAndCompanyReceiverIdAsync(long companyDomesticWaybillNo, int companyReceiverId, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, bool tracked, CancellationToken cancellationToken);

    Task<bool> CheckExistDomesticWaybillByIdAsync(int companyDomesticWaybillId, CancellationToken cancellationToken);

    Task<bool> CheckExistDomesticWaybillByNoAsync(long companyDomesticWaybillNo, CancellationToken cancellationToken);

    Task<bool> CheckExistDomesticWaybillByNoAndCompanySenderIdAsync(long companyDomesticWaybillNo, int companySenderId, CancellationToken cancellationToken);

    Task<bool> CheckExistDomesticWaybillByNoAndCompanyReceiverIdAsync(long companyDomesticWaybillNo, int companyReceiverId, CancellationToken cancellationToken);

    Task<int> InsertDomesticWaybillAsync(CompanyDomesticWaybill companyDomesticWaybill, CancellationToken cancellationToken);

    Task DeleteDomesticWaybillAsync(int companyDomesticWaybillId, CancellationToken cancellationToken);

    Task<List<int>> GetWaybillIdByManifestFormIdAsync(int companyManifestFormId, CancellationToken cancellationToken);

    Task<List<int>> GetWaybillIdByDomesticWaybillPeriodIdAndLessThanStartNumberAsync(int companyDomesticWaybillPeriodId, long startNumber, CancellationToken cancellationToken);

    Task<List<int>> GetWaybillIdByDomesticWaybillPeriodIdAndGreatherThanEndNumberAsync(int companyDomesticWaybillPeriodId, long endNumber, CancellationToken cancellationToken);

    Task<List<int>> GetWaybillIdByDomesticWaybillPeriodIdAsync(int companyDomesticWaybillPeriodId, CancellationToken cancellationToken);

    Task<bool> IsManifestStatusConsistentByManifestFormIdStateAsync(int companyManifestFormId, short state, CancellationToken cancellationToken);

    Task<bool> AnyIssunedDomesticWaybillByDomesticPeriodIdStartNumberEndNumberAsync(int domesticPeriod, long startNumber, long endNumber, CancellationToken cancellationToken);

    Task<bool> AnyIssunedDomesticWaybillByDomesticPeriodIdAsync(int domesticPeriod, CancellationToken cancellationToken);

    Task ChangeStateAsync(List<int> waybillIds, short state, CancellationToken cancellationToken);

    Task BackFromStateAsync(List<int> waybillIds, short state, CancellationToken cancellationToken);
}
