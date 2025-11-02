using Capitan360.Application.Common;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.Issue;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.IssueFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Dtos;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Services;

public interface ICompanyDomesticWaybillService
{
    Task<ApiResponse<CompanyDomesticWaybillDto>> IssueCompanyDomesticWaybillAsync(IssueCompanyDomesticWaybillCommand command, CancellationToken cancellationToken);
    
    Task<ApiResponse<CompanyDomesticWaybillDto>> IssueCompanyDomesticWaybillFromDesktopAsync(IssueCompanyDomesticWaybillFromDesktopCommand command, CancellationToken cancellationToken);






    //
    //Task<ApiResponse<CompanyDomesticWaybillDto>> UpdateCompanyDomesticWaybillFromDesktopAsync(UpdateCompanyDomesticWaybillFromDesktopCommand command, CancellationToken cancellationToken);
    //
    //Task<ApiResponse<CompanyDomesticWaybillDto>> UpdateCompanyDomesticWaybillAsync(UpdateCompanyDomesticWaybillCommand command, CancellationToken cancellationToken);
    //
    //Task<ApiResponse<bool>> ChangeStateCompanyDomesticWaybillToCollectionAsync(ChangeStateCompanyDomesticWaybillToCollectionCommand command, CancellationToken cancellationToken);
    //
    //Task<ApiResponse<bool>> BackCompanyDomesticWaybillFromCollectionStateAsync(BackCompanyDomesticWaybillFromCollectiongStateCommand command, CancellationToken cancellationToken);
    //
    //Task<ApiResponse<bool>> ChangeStateCompanyDomesticWaybillToReceivedAtSenderCompanyAsync(ChangeStateCompanyDomesticWaybillToReceivedAtSenderCompanyCommand command, CancellationToken cancellationToken);
    //
    //Task<ApiResponse<bool>> BackCompanyDomesticWaybillFromReceivedAtSenderCompanyStateAsync(BackCompanyDomesticWaybillFromReceivedAtSenderCompanyStateCommand command, CancellationToken cancellationToken);
    //
    //Task<ApiResponse<int>> AttachCompanyDomesticWaybillToCompanyManifestFormFromDesktopAsync(AttachCompanyDomesticWaybillToCompanyManifestFormFromDesktopCommand command, CancellationToken cancellationToken);
    //
    //Task<ApiResponse<int>> AttachCompanyDomesticWaybillToCompanyManifestFormAsync(AttachCompanyDomesticWaybillToCompanyManifestFormCommand command, CancellationToken cancellationToken);
    //
    //Task<ApiResponse<int>> BackCompanyDomesticWaybillFromCompanyManifestFormFromDesktopAsync(BackCompanyDomesticWaybillFromCompanyManifestFormFromDesktopCommand command, CancellationToken cancellationToken);
    //
    //Task<ApiResponse<int>> BackCompanyDomesticWaybillFromCompanyManifestFormAsync(BackCompanyDomesticWaybillFromCompanyManifestFormCommand command, CancellationToken cancellationToken);
    //
    //Task<ApiResponse<bool>> ChangeStateCompanyDomesticWaybillToDistributionAsync(ChangeStateCompanyDomesticWaybillToDistributionCommand command, CancellationToken cancellationToken);
    //
    //Task<ApiResponse<bool>> BackCompanyDomesticWaybillFromDistributionStateAsync(BackCompanyDomesticWaybillFromDistributionStateCommand command, CancellationToken cancellationToken);
    //
    //Task<ApiResponse<bool>> ChangeStateCompanyDomesticWaybillToDeliveryAsync(ChangeStateCompanyDomesticWaybillToDeliveryCommand command, CancellationToken cancellationToken);
    //
    //Task<ApiResponse<bool>> ChangeStateCompanyDomesticWaybillToDeliveryFromDesktopAsync(ChangeStateCompanyDomesticWaybillToDeliveryFromDesktopCommand command, CancellationToken cancellationToken);
    //
    //Task<ApiResponse<bool>> BackCompanyDomesticWaybillFormDeliveryStateAsync(BackCompanyDomesticWaybillFormDeliveryStateCommand command, CancellationToken cancellationToken);
    //
    //Task<ApiResponse<bool>> BackCompanyDomesticWaybillFormDeliveryStateFromDesktopAsync(BackCompanyDomesticWaybillFormDeliveryStateFromDesktopCommand command, CancellationToken cancellationToken);
    //
    //Task<ApiResponse<int>> BackCompanyDomesticWaybillToReadyStateFromDesktopAsync(BackCompanyDomesticWaybillToReadyStateFromDesktopCommand command, CancellationToken cancellationToken);
    //
    //Task<ApiResponse<bool>> BackCompanyDomesticWaybillToReayStateAsync(BackCompanyDomesticWaybillToReayStateCommand command, CancellationToken cancellationToken);
}
