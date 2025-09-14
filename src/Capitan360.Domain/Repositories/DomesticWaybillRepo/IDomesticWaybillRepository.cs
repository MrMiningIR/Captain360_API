namespace Capitan360.Domain.Repositories.DomesticWaybillRepo;

public interface IDomesticWaybillRepository
{
    Task<Domain.Entities.DomesticWaybillEntity.DomesticWaybill?> GetDomesticWaybillByIdAsync(int domesticWaybillId, bool tracked, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, CancellationToken cancellationToken);

    Task<Domain.Entities.DomesticWaybillEntity.DomesticWaybill?> GetDomesticWaybillByNoAsync(long domesticWaybillNo, bool tracked, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, CancellationToken cancellationToken);

    Task<Domain.Entities.DomesticWaybillEntity.DomesticWaybill?> GetDomesticWaybillByNoAndCompanySenderIdAsync(long domesticWaybillNo, int companySenderId, bool tracked, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, CancellationToken cancellationToken);

    Task<Domain.Entities.DomesticWaybillEntity.DomesticWaybill?> GetDomesticWaybillByNoAndCompanyReceiverIdAsync(long domesticWaybillNo, int companyReceiverId, bool tracked, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, CancellationToken cancellationToken);

    Task<bool> CheckExistDomesticWaybillByIdAsync(int domesticWaybillId, CancellationToken cancellationToken);

    Task<bool> CheckExistDomesticWaybillByNoAsync(long domesticWaybillNo, CancellationToken cancellationToken);

    Task<bool> CheckExistDomesticWaybillByNoAndCompanySenderIdAsync(long domesticWaybillNo, int companySenderId, CancellationToken cancellationToken);

    Task<bool> CheckExistDomesticWaybillByNoAndCompanyReceiverIdAsync(long domesticWaybillNo, int companyReceiverId, CancellationToken cancellationToken);

    Task<int> InsertDomesticWaybillAsync(Capitan360.Domain.Entities.DomesticWaybillEntity.DomesticWaybill domesticWaybill, CancellationToken cancellationToken);

    Task DeleteDomesticWaybillAsync(int domesticWaybillId, CancellationToken cancellationToken);

    Task<List<int>> GetWaybillIdByManifestFormIdAsync(int manifestFormId, CancellationToken cancellationToken);

    Task<List<int>> GetWaybillIdByDomesticWaybillPeriodIdAndLessThanStartNumberAsync(int domesticWaybillPeriodId, long startNumber, CancellationToken cancellationToken);

    Task<List<int>> GetWaybillIdByDomesticWaybillPeriodIdAndGreatherThanEndNumberAsync(int domesticWaybillPeriodId, long endNumber, CancellationToken cancellationToken);

    Task<List<int>> GetWaybillIdByDomesticWaybillPeriodIdAsync(int domesticWaybillPeriodId, CancellationToken cancellationToken);

    Task<bool> IsManifestStatusConsistentByManifestFormIdStateAsync(int manifestFormId, short state, CancellationToken cancellationToken);

    Task<bool> AnyIssunedDomesticWaybillByDomesticPeriodIdStartNumberEndNumberAsync(int domesticPeriod, long startNumber, long endNumber, CancellationToken cancellationToken);

    Task<bool> AnyIssunedDomesticWaybillByDomesticPeriodIdAsync(int domesticPeriod, CancellationToken cancellationToken);

    Task ChangeStateAsync(List<int> waybillIds, short state, CancellationToken cancellationToken);

    Task BackFromStateAsync(List<int> waybillIds, short state, CancellationToken cancellationToken);
}
