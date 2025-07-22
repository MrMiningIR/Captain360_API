using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyType.Commands.CreateCompanyType;
using Capitan360.Application.Services.CompanyServices.CompanyType.Commands.DeleteCompanyType;
using Capitan360.Application.Services.CompanyServices.CompanyType.Commands.UpdateCompanyType;
using Capitan360.Application.Services.CompanyServices.CompanyType.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyType.Queries.GetAllCompanyTypes;
using Capitan360.Application.Services.CompanyServices.CompanyType.Queries.GetCompanyTypeById;

namespace Capitan360.Application.Services.CompanyServices.CompanyType.Services;

public interface ICompanyTypeService
{
    Task<int> CreateCompanyTypeAsync(CreateCompanyTypeCommand companyType, CancellationToken cancellationToken);
    Task<ApiResponse<PagedResult<CompanyTypeDto>>> GetAllCompanyTypes(GetAllCompanyTypesQuery allCompanyTypesQuery,
        CancellationToken cancellationToken);
    Task<ApiResponse<CompanyTypeDto>> GetCompanyTypeByIdAsync(GetCompanyTypeByIdQuery id,
        CancellationToken cancellationToken);
    Task DeleteCompanyTypeAsync(DeleteCompanyTypeCommand id, CancellationToken cancellationToken);
    Task UpdateCompanyTypeAsync(UpdateCompanyTypeCommand command, CancellationToken cancellationToken);
}