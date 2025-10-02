using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.CompanyBanks.Commands.Create;
using Capitan360.Application.Features.Companies.CompanyBanks.Commands.Delete;
using Capitan360.Application.Features.Companies.CompanyBanks.Commands.MoveDown;
using Capitan360.Application.Features.Companies.CompanyBanks.Commands.MoveUp;
using Capitan360.Application.Features.Companies.CompanyBanks.Commands.Update;
using Capitan360.Application.Features.Companies.CompanyBanks.Commands.UpdateActiveState;
using Capitan360.Application.Features.Companies.CompanyBanks.Dtos;
using Capitan360.Application.Features.Companies.CompanyBanks.Queries.GetAll;
using Capitan360.Application.Features.Companies.CompanyBanks.Queries.GetByCompanyId;
using Capitan360.Application.Features.Companies.CompanyBanks.Queries.GetById;

namespace Capitan360.Application.Features.Companies.CompanyBanks.Services;

public interface ICompanyBankService
{
    Task<ApiResponse<int>> CreateCompanyBankAsync(CreateCompanyBankCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> DeleteCompanyBankAsync(DeleteCompanyBankCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> MoveUpCompanyBankAsync(MoveUpCompanyBankCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> MoveDownCompanyBankAsync(MoveDownCompanyBankCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<int>> SetCompanyBankActivityStatusAsync(UpdateActiveStateCompanyBankCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyBankDto>> UpdateCompanyBankAsync(UpdateCompanyBankCommand command, CancellationToken cancellationToken);

    Task<ApiResponse<PagedResult<CompanyBankDto>>> GetAllCompanyBanksAsync(GetAllCompanyBanksQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<IReadOnlyList<CompanyBankDto>>> GetCompanyBankByCompanyIdAsync(GetCompanyBankByCompanyIdQuery query, CancellationToken cancellationToken);

    Task<ApiResponse<CompanyBankDto>> GetCompanyBankByIdAsync(GetCompanyBankByIdQuery query, CancellationToken cancellationToken);
}
