using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyType.Commands.CreateCompanyType;
using Capitan360.Application.Features.Companies.CompanyType.Commands.DeleteCompanyType;
using Capitan360.Application.Features.Companies.CompanyType.Commands.UpdateCompanyType;
using Capitan360.Application.Features.Companies.CompanyType.Queries.GetAllCompanyTypes;
using Capitan360.Application.Features.Companies.CompanyType.Queries.GetCompanyTypeById;
using Capitan360.Application.Features.Companies.CompanyTypes.Dtos;

namespace Capitan360.Application.Features.Companies.CompanyType.Services;

public interface ICompanyTypeService
{
    Task<ApiResponse<int>> CreateCompanyTypeAsync(CreateCompanyTypeCommand createCompanyTypeCommand, CancellationToken cancellationToken);

    Task<ApiResponse<int>> DeleteCompanyTypeAsync(DeleteCompanyTypeCommand deleteCompanyTypeCommand, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyTypeDto>> UpdateCompanyTypeAsync(UpdateCompanyTypeCommand updateCompanyTypeCommand, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<CompanyTypeDto>>> GetAllCompanyTypesAsync(GetAllCompanyTypesQuery getAllCompanyTypesQuery, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyTypeDto>> GetCompanyTypeByIdAsync(GetCompanyTypeByIdQuery getCompanyTypeByIdQuery, CancellationToken cancellationToken);
}