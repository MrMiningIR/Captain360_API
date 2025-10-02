using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyTypes.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyTypes.Commands.Delete;
using Capitan360.Application.Features.Companies.CompanyTypes.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyTypes.Dtos;
using Capitan360.Application.Features.Companies.CompanyTypes.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyTypes.Queries.GetById;

namespace Capitan360.Application.Features.Companies.CompanyTypes.Services;

public interface ICompanyTypeService
{
    Task<ApiResponse<int>> CreateCompanyTypeAsync(CreateCompanyTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> DeleteCompanyTypeAsync(DeleteCompanyTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyTypeDto>> UpdateCompanyTypeAsync(UpdateCompanyTypeCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<CompanyTypeDto>>> GetAllCompanyTypesAsync(GetAllCompanyTypesQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyTypeDto>> GetCompanyTypeByIdAsync(GetCompanyTypeByIdQuery query, CancellationToken cancellationToken);
}