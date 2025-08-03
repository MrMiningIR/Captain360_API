using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.CreateCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.DeleteCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.UpdateCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Queries.GetAllCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Queries.GetCompanyPreferencesById;

namespace Capitan360.Application.Services.CompanyServices.CompanyPreferences.Services;

public interface ICompanyPreferencesService
{
    Task<ApiResponse<int>> CreateCompanyPreferencesAsync(CreateCompanyPreferencesCommand companyPreferences, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyPreferencesDto>> GetCompanyPreferencesByIdAsync(GetCompanyPreferencesByIdQuery id, CancellationToken cancellationToken);
    Task<ApiResponse<int>> UpdateCompanyPreferencesAsync(UpdateCompanyPreferencesCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<CompanyPreferencesDto>>> GetAllCompanyPreferences(GetAllCompanyPreferencesQuery allCompanyPreferencesQuery, CancellationToken cancellationToken);

    Task<ApiResponse<int>> DeleteCompanyPreferencesAsync(DeleteCompanyPreferencesCommand id,
        CancellationToken cancellationToken);

}