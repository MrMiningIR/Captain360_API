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
    Task<ApiResponse<int>> CreateCompanyTypeAsync(CreateCompanyTypeCommand createCompanyTypeCommand, CancellationToken cancellationToken);

    Task<ApiResponse<int>> DeleteCompanyTypeAsync(DeleteCompanyTypeCommand deleteCompanyTypeCommand, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyTypeDto>> UpdateCompanyTypeAsync(UpdateCompanyTypeCommand updateCompanyTypeCommand, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<CompanyTypeDto>>> GetAllCompanyTypesAsync(GetAllCompanyTypesQuery getAllCompanyTypesQuery, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyTypeDto>> GetCompanyTypeByIdAsync(GetCompanyTypeByIdQuery getCompanyTypeByIdQuery, CancellationToken cancellationToken);
}