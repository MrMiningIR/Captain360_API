using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.CreateCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.DeleteCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.UpdateCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.UpdateInternationalAirlineCargoStateCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.UpdateIssueDomesticWaybillStateCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.UpdateShowInSearchEngineStateCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.UpdateWebServiceSearchEngineStateCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Queries.GetAllCompanyPreferences;
using Capitan360.Application.Services.CompanyServices.CompanyPreferences.Queries.GetCompanyPreferencesById;

namespace Capitan360.Application.Services.CompanyServices.CompanyPreferences.Services;

public interface ICompanyPreferencesService
{
    Task<ApiResponse<int>> CreateCompanyPreferencesAsync(CreateCompanyPreferencesCommand command,
        CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<CompanyPreferencesDto>>> GetAllCompanyPreferences(
        GetAllCompanyPreferencesQuery allCompanyPreferencesQuery, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyPreferencesDto>> GetCompanyPreferencesByIdAsync(
        GetCompanyPreferencesByIdQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<int>> DeleteCompanyPreferencesAsync(DeleteCompanyPreferencesCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<CompanyPreferencesDto>> UpdateCompanyPreferencesAsync(UpdateCompanyPreferencesCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<int>> SetCompanyInternationalAirlineCargoStatusAsync(UpdateInternationalAirlineCargoStateCompanyPreferencesCommand updateInternationalAirlineCargoStateCompanyPreferencesCommand, CancellationToken cancellationToken);
    Task<ApiResponse<int>> SetCompanyIssueDomesticWaybillStatusAsync(UpdateIssueDomesticWaybillStateCompanyPreferencesCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<int>> SetCompanyShowInSearchEngineStatusAsync(UpdateShowInSearchEngineStateCompanyPreferencesCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<int>> SetCompanyWebServiceSearchEngineStatusAsync(UpdateWebServiceSearchEngineStateCompanyPreferencesCommand command, CancellationToken cancellationToken);

}