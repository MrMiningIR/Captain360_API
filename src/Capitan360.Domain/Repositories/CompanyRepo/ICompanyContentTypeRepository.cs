using Capitan360.Domain.Constants;
using Capitan360.Domain.Dtos.TransferObject;
using Capitan360.Domain.Entities.ContentEntity;

namespace Capitan360.Domain.Repositories.CompanyRepo;

//public interface ICompanyContentTypeRepository
//{
//    Task<(IReadOnlyList<CompanyContentType>, int)> GetCompanyContentTypes(string? searchPhrase,
//        int companyId, int active, int pageSize,
//        int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);

//    Task<int> UpdateCompanyContentTypeForCompany(
//      CompanyContentType companyContentType,

//        CancellationToken ct);
//    Task<int> InsertCompanyContentTypeForCompany(
//        CompanyContentType companyContentType,

//        CancellationToken ct);


//    Task<CompanyContentType?> CheckExistCompanyContentTypeName(int commandCompanyId, int commandContentTypeId, CancellationToken cancellationToken);

//    Task MoveContentTypeUpAsync(int companyId, int contentTypeId, CancellationToken cancellationToken);
//    Task MoveContentTypeDownAsync(int companyId, int contentTypeId, CancellationToken cancellationToken);

//    Task<int> OrderContentType(int companyId, CancellationToken cancellationToken);


//    Task CreateCompanyContentTypes(List<int> getEligibleCommandlines, ContentType contentType,
//        CancellationToken cancellationToken);

//    Task AddContentTypesToCompanyContentType(List<CompanyContentTypeTransfer> relatedContentTypes, int companyId,
//        CancellationToken cancellationToken);

//    Task DeleteAllContentsByCompanyId(int companyId, CancellationToken cancellationToken);
//    Task<bool> CheckExistAnyItem(int companyId, CancellationToken cancellationToken);
//}

public interface ICompanyContentTypeRepository
{
    Task<(IReadOnlyList<CompanyContentType>, int)> GetCompanyContentTypes(string? searchPhrase,
        int companyId, int active, int pageSize,
        int pageNumber, string? sortBy, SortDirection sortDirection, CancellationToken cancellationToken);

    Task<int> UpdateCompanyContentTypeForCompany(
        CompanyContentType companyContentType,
        CancellationToken ct);

    Task<int> InsertCompanyContentTypeForCompany(
        CompanyContentType companyContentType,
        CancellationToken ct);

    Task<CompanyContentType?> CheckExistCompanyContentTypeName(int commandCompanyId, int commandContentTypeId, CancellationToken cancellationToken);

    Task MoveContentTypeUpAsync(int companyId, int contentTypeId, CancellationToken cancellationToken);

    Task MoveContentTypeDownAsync(int companyId, int contentTypeId, CancellationToken cancellationToken);

    Task<int> OrderContentType(int companyId, CancellationToken cancellationToken);

    Task CreateCompanyContentTypes(List<int> getEligibleCommandlines, ContentType contentType,
        CancellationToken cancellationToken);

    Task AddContentTypesToCompanyContentType(List<CompanyContentTypeTransfer> relatedContentTypes, int companyId,
        CancellationToken cancellationToken);

    Task DeleteAllContentsByCompanyId(int companyId, CancellationToken cancellationToken);

    Task<bool> CheckExistAnyItem(int companyId, CancellationToken cancellationToken);

    Task<CompanyContentType?> GetCompanyContentTypeById(int id, CancellationToken cancellationToken,
        bool tracking = false);
}