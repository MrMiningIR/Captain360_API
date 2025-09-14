using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capitan360.Domain.Repositories.DomesticWaybillRepo;

public interface IManifestFormRepository
{
    Task<Domain.Entities.DomesticWaybillEntity.ManifestForm?> GetManifestFormByIdAsync(int manifestFormId, bool tracked, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, CancellationToken cancellationToken);

    Task<Domain.Entities.DomesticWaybillEntity.ManifestForm?> GetManifestFormByNoAsync(long manifestFormNo, bool tracked, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, CancellationToken cancellationToken);

    Task<Domain.Entities.DomesticWaybillEntity.ManifestForm?> GetManifestFormByNoAndCompanySenderIdAsync(long manifestFormNo, int companySenderId, bool tracked, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, CancellationToken cancellationToken);

    Task<Domain.Entities.DomesticWaybillEntity.ManifestForm?> GetManifestFormByNoAndCompanyReceiverIdAsync(long manifestFormNo, int companyReceiverId, bool tracked, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, CancellationToken cancellationToken);

    Task<bool> CheckExistManifestFormByIdAsync(int manifestFormId, CancellationToken cancellationToken);

    Task<bool> CheckExistManifestFormByNoAsync(long manifestFormNo, CancellationToken cancellationToken);

    Task<bool> CheckExistManifestFormByNoAndCompanySenderIdAsync(long manifestFormNo, int companySenderId, CancellationToken cancellationToken);

    Task<bool> CheckExistManifestFormByNoAndCompanyReceiverIdAsync(long manifestFormNo, int companyReceiverId, CancellationToken cancellationToken);

    Task<int> InsertManifestFormAsync(Capitan360.Domain.Entities.DomesticWaybillEntity.ManifestForm manifestForm, CancellationToken cancellationToken);

    Task DeleteManifestFormAsync(int manifestFormId, CancellationToken cancellationToken);

    Task<List<int>> GetManifestFormIdByManifestFormPeriodIdAndLessThanStartNumberAsync(int manifestFormPeriodId, long startNumber, CancellationToken cancellationToken);

    Task<List<int>> GetManifestFormIdByManifestFormPeriodIdAndGreatherThanEndNumberAsync(int manifestFormPeriodId, long endNumber, CancellationToken cancellationToken);

    Task<List<int>> GetManifestFormIdByManifestFormPeriodIdAsync(int manifestFormPeriodId, CancellationToken cancellationToken);

    Task<bool> AnyIssunedManifestFormByDomesticPeriodIdStartNumberEndNumberAsync(int domesticPeriod, long startNumber, long endNumber, CancellationToken cancellationToken);

    Task<bool> AnyIssunedManifestFormByDomesticPeriodIdAsync(int domesticPeriod, CancellationToken cancellationToken);

    Task ChangeStateAsync(List<int> waybillIds, short state, CancellationToken cancellationToken);
}
