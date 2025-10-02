using Capitan360.Domain.Entities.CompanyDomesticPaths;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Interfaces.Repositories.CompanyDomesticPaths;

public interface ICompanyDomesticPathReceiverCompanyRepository
{
    Task<bool> CheckExistCompanyDomesticPathReceiverCompanyAsync(int companyDomesticPathId, int municipalAreaId, int? currentCompanyDomesticPathReceiverCompanyId, CancellationToken cancellationToken);

    Task<int> CreateCompanyDomesticPathCompanyReceiverAsync(CompanyDomesticPathReceiverCompany companyDomesticPathReceiverCompany, CancellationToken cancellationToken);

    Task<CompanyDomesticPathReceiverCompany?> GetCompanyDomesticPathReceiverCompanyByIdAsync(int companyDomesticPathReceiverCompanyId, bool loadData, bool tracked, CancellationToken cancellationToken);

    Task DeleteCompanyDomesticPathReceiverCompanyAsync(int companyDomesticPathReceiverCompanyId);

    Task<(IReadOnlyList<CompanyDomesticPathReceiverCompany>, int)> GetAllCompanyDomesticPathReceiverCompaniesAsync(string searchPhrase, string? sortBy, int companyPathId, bool loadData, int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<IReadOnlyList<CompanyDomesticPathReceiverCompany>?> GetCompanyDomesticPathReceiverCompanyByDomesticPathIdAsync(int companyDomesticPathId, CancellationToken cancellationToken);

    Task<bool> CheckExistCompanyDomesticPathReceiverCompanyByCompanySenderIdAndCompanyReceiverCodeAsync(int companySenderId, string receiverCompanyUserInsertedCode, CancellationToken cancellationToken);

    Task<IReadOnlyList<CompanyDomesticPathReceiverCompany>?> GetCompanyDomesticPathReceiverCompanyByCompanySenderIdAndCompanyReceiverCodeAsync(int companySenderId, string receiverCompanyUserInsertedCode, CancellationToken cancellationToken);

    Task<IReadOnlyList<CompanyDomesticPathReceiverCompany>?> GetCompanyDomesticPathReceiverCompanyByCompanySenderIdAndReceiverCompanyIdAsync(int companySenderId, int receiverCompanyId, CancellationToken cancellationToken);
}