using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.Delete;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.UpdateInternationalAirlineCargoState;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.UpdateIssueDomesticWaybillState;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.UpdateShowInSearchEngineState;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.UpdateWebServiceSearchEngineState;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Dtos;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Queries.GetByCompanyId;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Queries.GetById;

namespace Capitan360.Application.Features.Companies.CompanyPreferenceses.Services;

public interface ICompanyPreferencesService
{
    Task<ApiResponse<int>> CreateCompanyPreferencesAsync(CreateCompanyPreferencesCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> DeleteCompanyPreferencesAsync(DeleteCompanyPreferencesCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> SetCompanyInternationalAirlineCargoStatusAsync(UpdateInternationalAirlineCargoStateCompanyPreferencesCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> SetCompanyIssueDomesticWaybillStatusAsync(UpdateIssueDomesticWaybillStateCompanyPreferencesCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> SetCompanyShowInSearchEngineStatusAsync(UpdateShowInSearchEngineStateCompanyPreferencesCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> SetCompanyWebServiceSearchEngineStatusAsync(UpdateWebServiceSearchEngineStateCompanyPreferencesCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyPreferencesDto>> UpdateCompanyPreferencesAsync(UpdateCompanyPreferencesCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<CompanyPreferencesDto>>> GetAllCompanyPreferencesAsync(GetAllCompanyPreferencesQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyPreferencesDto>> GetCompanyPreferencesByCompanyIdAsync(GetCompanyPreferencesByCompanyIdQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyPreferencesDto>> GetCompanyPreferencesByIdAsync(GetCompanyPreferencesByIdQuery query, CancellationToken cancellationToken);
}