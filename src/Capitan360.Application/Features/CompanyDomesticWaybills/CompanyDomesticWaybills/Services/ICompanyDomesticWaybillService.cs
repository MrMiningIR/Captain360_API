using Capitan360.Application.Common;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.AttachToCompanyManifestForm;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.AttachToCompanyManifestFormFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFormCollectiongState;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFormDeliveryState;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFormDeliveryStateFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFormReceivedAtSenderCompanyState;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFromCompanyManifestForm;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFromCompanyManifestFormFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFromDistributionState;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackToReadyStateFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackToReayState;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToCollection;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToDelivery;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToDeliveryFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToDistribution;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToReceivedAtSenderCompany;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.Issue;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.IssueFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.Update;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.UpdateFromDesktop;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Dtos;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Services;

public interface ICompanyDomesticWaybillService
{
    //Task<ApiResponse<int>> InsertCompanyDomesticWaybillFromDesktopAsync(IssueCompanyDomesticWaybillFromDesktopCommand command, CancellationToken cancellationToken);
    //
    //Task<ApiResponse<int>> InsertCompanyDomesticWaybillAsync(IssueCompanyDomesticWaybillCommand command, CancellationToken cancellationToken);
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
