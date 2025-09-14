using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Repositories.Companies;

public interface ICompanyBankRepository
{
    Task<bool> CheckExistCompanyBankNameAsync(string companyBankName, int? currentCompanyBankId, int companyId, CancellationToken cancellationToken);

    Task<bool> CheckExistCompanyBankCodeAsync(string companyBankCode, int? currentCompanyBankId, int companyId, CancellationToken cancellationToken);

    Task<int> CreateCompanyBankAsync(CompanyBank companyBank, CancellationToken cancellationToken);

    Task<int> GetCountCompanyBankAsync(int companyId, CancellationToken cancellationToken);

    Task<CompanyBank?> GetCompanyBankByIdAsync(int companyBankId, bool tracked, bool loadData, CancellationToken cancellationToken);

    Task<IReadOnlyList<CompanyBank>?> GetCompanyBankByCompanyIdAsync(int companyBankCompanyId, bool tracked, bool loadData, CancellationToken cancellationToken);

    Task DeleteCompanyBankAsync(CompanyBank companyBank);

    Task MoveCompanyBankUpAsync(int companyBankId, CancellationToken cancellationToken);

    Task MoveCompanyBankDownAsync(int companyBankId, CancellationToken cancellationToken);

    Task<(IReadOnlyList<CompanyBank>, int)> GetAllCompanyBanksAsync(string? searchPhrase, string? sortBy, int companyId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<CompanyBank?> GetCompanyBankByCodeAsync(string companyBankCode, int companyId, bool tracked, bool loadData, CancellationToken cancellationToken);
}
