using Capitan360.Domain.Entities.CompanyManifestForms;

namespace Capitan360.Domain.Interfaces.Repositories.ComapnyManifestForms;

public interface IManifestFormRepository
{
    Task<CompanyManifestForm?> GetManifestFormByIdAsync(int companyManifestFormId, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, bool tracked, CancellationToken cancellationToken);

    Task<CompanyManifestForm?> GetManifestFormByNoAsync(long companyManifestFormNo, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, bool tracked, CancellationToken cancellationToken);

    Task<CompanyManifestForm?> GetManifestFormByNoAndCompanySenderIdAsync(long companyManifestFormNo, int companySenderId, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, bool tracked, CancellationToken cancellationToken);

    Task<CompanyManifestForm?> GetManifestFormByNoAndCompanyReceiverIdAsync(long companyManifestFormNo, int companyReceiverId, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, bool tracked, CancellationToken cancellationToken);

    Task<bool> CheckExistManifestFormByIdAsync(int companyManifestFormId, CancellationToken cancellationToken);

    Task<bool> CheckExistManifestFormByNoAsync(long companyManifestFormNo, CancellationToken cancellationToken);

    Task<bool> CheckExistManifestFormByNoAndCompanySenderIdAsync(long companyManifestFormNo, int companySenderId, CancellationToken cancellationToken);

    Task<bool> CheckExistManifestFormByNoAndCompanyReceiverIdAsync(long companyManifestFormNo, int companyReceiverId, CancellationToken cancellationToken);

    Task<int> InsertManifestFormAsync(CompanyManifestForm companyManifestForm, CancellationToken cancellationToken);

    Task DeleteManifestFormAsync(int companyManifestFormId, CancellationToken cancellationToken);

    Task<List<int>> GetManifestFormIdByManifestFormPeriodIdAndLessThanStartNumberAsync(int companyManifestFormPeriodId, long startNumber, CancellationToken cancellationToken);

    Task<List<int>> GetManifestFormIdByManifestFormPeriodIdAndGreatherThanEndNumberAsync(int companyManifestFormPeriodId, long endNumber, CancellationToken cancellationToken);

    Task<List<int>> GetManifestFormIdByManifestFormPeriodIdAsync(int companyManifestFormPeriodId, CancellationToken cancellationToken);

    Task<bool> AnyIssunedManifestFormByDomesticPeriodIdStartNumberEndNumberAsync(int domesticPeriod, long startNumber, long endNumber, CancellationToken cancellationToken);

    Task<bool> AnyIssunedManifestFormByDomesticPeriodIdAsync(int domesticPeriod, CancellationToken cancellationToken);
}
