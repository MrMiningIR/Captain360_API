using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Interfaces.Repositories.Companies;

public interface ICompanyBankRepository
{
    Task<bool> CheckExistCompanyBankNameAsync(string companyBankName, int companyId, int? currentCompanyBankId, CancellationToken cancellationToken);

    Task<bool> CheckExistCompanyBankCodeAsync(string companyBankCode, int companyId, int? currentCompanyBankId, CancellationToken cancellationToken);

    Task<int> CreateCompanyBankAsync(CompanyBank companyBank, CancellationToken cancellationToken);

    Task<int> GetCountCompanyBankAsync(int companyId, CancellationToken cancellationToken);

    Task<CompanyBank?> GetCompanyBankByIdAsync(int companyBankId, bool loadData, bool tracked, CancellationToken cancellationToken);

    Task<IReadOnlyList<CompanyBank>?> GetCompanyBankByCompanyIdAsync(int companyBankCompanyId, CancellationToken cancellationToken);

    Task DeleteCompanyBankAsync(int companyBankId);

    Task MoveUpCompanyBankAsync(int companyBankId, CancellationToken cancellationToken);

    Task MoveDownCompanyBankAsync(int companyBankId, CancellationToken cancellationToken);

    Task<(IReadOnlyList<CompanyBank>, int)> GetAllCompanyBanksAsync(string searchPhrase, string? sortBy, int companyId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<CompanyBank?> GetCompanyBankByCodeAsync(string companyBankCode, int companyId, bool loadData, bool tracked, CancellationToken cancellationToken);
}
