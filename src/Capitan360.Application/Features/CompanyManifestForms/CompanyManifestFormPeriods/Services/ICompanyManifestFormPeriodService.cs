using Capitan360.Application.Common;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Commands.Create;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Commands.Delete;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Commands.Update;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Commands.UpdateActiveState;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Dtos;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Queries.GetAll;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Queries.GetById;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Services;

public interface ICompanyManifestFormPeriodService
{
    Task<ApiResponse<int>> CreateCompanyManifestFormPeriodAsync(CreateCompanyManifestFormPeriodCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> SetCompanyManifestFormPeriodActivityStatusAsync(UpdateActiveStateCompanyManifestFormPeriodCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyManifestFormPeriodDto>> UpdateCompanyManifestFormPeriodAsync(UpdateCompanyManifestFormPeriodCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> DeleteCompanyManifestFormPeriodAsync(DeleteCompanyManifestFormPeriodCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<CompanyManifestFormPeriodDto>>> GetAllCompanyManifestFormPeriodsAsync(GetAllCompanyManifestFormPeriodsQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyManifestFormPeriodDto>> GetCompanyManifestFormPeriodByIdAsync(GetCompanyManifestFormPeriodByIdQuery query, CancellationToken cancellationToken);
}
