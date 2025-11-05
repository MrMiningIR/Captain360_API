using AutoMapper;
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
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Features.Companies.CompanyBanks.Services
{
    public class CompanyBankService(
    ILogger<CompanyBankService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ICompanyBankRepository companyBankRepository,
    ICompanyRepository companyRepository
    ) : ICompanyBankService
    {
        public async Task<ApiResponse<int>> CreateCompanyBankAsync(CreateCompanyBankCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("CreateCompanyBank is Called with {@CreateCompanyBankCommand}", command);

            var company = await companyRepository.GetCompanyByIdAsync(command.CompanyId, false, false, cancellationToken);
            if (company == null)
                return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

            var user = userContext.GetCurrentUser();
            if (user == null)
                return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

            if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
                return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

            if (await companyBankRepository.CheckExistCompanyBankNameAsync(command.Name, command.CompanyId, null, cancellationToken))
                return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "نام شرکت بیمه تکراری است");

            if (await companyBankRepository.CheckExistCompanyBankCodeAsync(command.Code, command.CompanyId, null, cancellationToken))
                return ApiResponse<int>.Error(StatusCodes.Status409Conflict, "کد شرکت بیمه تکراری است");

            int existingCount = await companyBankRepository.GetCountCompanyBankAsync(command.CompanyId, cancellationToken);

            var companyBank = mapper.Map<CompanyBank>(command) ?? null;
            if (companyBank == null)
                return ApiResponse<int>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

            companyBank.Order = existingCount + 1;

            var CompanyBankId = await companyBankRepository.CreateCompanyBankAsync(companyBank, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation("CompanyBank created successfully with {@CompanyBank}", companyBank);
            return ApiResponse<int>.Ok(CompanyBankId, "محتوی بار با موفقیت ایجاد شد");
        }

        public async Task<ApiResponse<int>> DeleteCompanyBankAsync(DeleteCompanyBankCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("DeleteCompanyBank is Called with {@Id}", command.Id);

            var companyBank = await companyBankRepository.GetCompanyBankByIdAsync(command.Id, false, false, cancellationToken);
            if (companyBank is null)
                return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "URI شرکت نامعتبر است");

            var company = await companyRepository.GetCompanyByIdAsync(companyBank.CompanyId, false, false, cancellationToken);
            if (company == null)
                return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

            var user = userContext.GetCurrentUser();
            if (user == null)
                return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

            if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
                return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

            await companyBankRepository.DeleteCompanyBankAsync(companyBank.Id);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation("CompanyBank Deleted successfully with {@Id}", command.Id);
            return ApiResponse<int>.Ok(command.Id, "بانک با موفقیت حذف شد");
        }

        public async Task<ApiResponse<int>> MoveUpCompanyBankAsync(MoveUpCompanyBankCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("MoveUpCompanyBank is Called with {@Id}", command.Id);

            var companyBank = await companyBankRepository.GetCompanyBankByIdAsync(command.Id, false, false, cancellationToken);
            if (companyBank == null)
                return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "بانک نامعتبر است");

            var company = await companyRepository.GetCompanyByIdAsync(companyBank.CompanyId, false, false, cancellationToken);
            if (company == null)
                return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

            var user = userContext.GetCurrentUser();
            if (user == null)
                return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

            if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
                return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

            if (companyBank.Order == 1)
                return ApiResponse<int>.Ok(command.Id, "انجام شد");

            var count = await companyBankRepository.GetCountCompanyBankAsync(companyBank.CompanyId, cancellationToken);

            if (count <= 1)
                return ApiResponse<int>.Ok(command.Id, "انجام شد");

            await companyBankRepository.MoveUpCompanyBankAsync(command.Id, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation("CompanyBank moved up successfully with {@Id}", command.Id);
            return ApiResponse<int>.Ok(command.Id, "بانک با موفقیت جابجا شد");
        }

        public async Task<ApiResponse<int>> MoveDownCompanyBankAsync(MoveDownCompanyBankCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("MoveDownCompanyBank is Called with {@Id}", command.Id);

            var companyBank = await companyBankRepository.GetCompanyBankByIdAsync(command.Id, false, false, cancellationToken);
            if (companyBank == null)
                return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "بانک نامعتبر است");

            var company = await companyRepository.GetCompanyByIdAsync(companyBank.CompanyId, false, false, cancellationToken);
            if (company == null)
                return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

            var user = userContext.GetCurrentUser();
            if (user == null)
                return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

            if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
                return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

            var count = await companyBankRepository.GetCountCompanyBankAsync(companyBank.CompanyId, cancellationToken);

            if (companyBank.Order == count)
                return ApiResponse<int>.Ok(command.Id, "انجام شد");

            if (count <= 1)
                return ApiResponse<int>.Ok(command.Id, "انجام شد");

            await companyBankRepository.MoveDownCompanyBankAsync(command.Id, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation("CompanyBank moved down successfully with {@Id}", command.Id);
            return ApiResponse<int>.Ok(command.Id, "بانک با موفقیت جابجا شد");
        }

        public async Task<ApiResponse<int>> SetCompanyBankActivityStatusAsync(UpdateActiveStateCompanyBankCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("SetCompanyBankActivityStatus Called with {@Id}", command.Id);

            var companyBank = await companyBankRepository.GetCompanyBankByIdAsync(command.Id, false, true, cancellationToken);
            if (companyBank is null)
                return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "URI بانک نامعتبر است");

            var company = await companyRepository.GetCompanyByIdAsync(companyBank.CompanyId, false, false, cancellationToken);
            if (company == null)
                return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

            var user = userContext.GetCurrentUser();
            if (user == null)
                return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

            if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
                return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

            companyBank.Active = !companyBank.Active;
            await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation("CompanyBank activity status updated successfully with {@Id}", command.Id);
            return ApiResponse<int>.Ok(command.Id, "وضعیت بانک با موفقیت به روز رسانی شد");
        }

        public async Task<ApiResponse<CompanyBankDto>> UpdateCompanyBankAsync(UpdateCompanyBankCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateCompanyBank is Called with {@UpdateCompanyBankCommand}", command);

            var companyBank = await companyBankRepository.GetCompanyBankByIdAsync(command.Id, false, true, cancellationToken);
            if (companyBank is null)
                return ApiResponse<CompanyBankDto>.Error(StatusCodes.Status404NotFound, "URI بانک نامعتبر است");

            var company = await companyRepository.GetCompanyByIdAsync(companyBank.CompanyId, false, false, cancellationToken);
            if (company == null)
                return ApiResponse<CompanyBankDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

            var user = userContext.GetCurrentUser();
            if (user == null)
                return ApiResponse<CompanyBankDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

            if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
                return ApiResponse<CompanyBankDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

            if (await companyBankRepository.CheckExistCompanyBankNameAsync(command.Name, command.Id, companyBank.CompanyId, cancellationToken))
                return ApiResponse<CompanyBankDto>.Error(StatusCodes.Status409Conflict, "نام بانک تکراری است");

            if (await companyBankRepository.CheckExistCompanyBankCodeAsync(command.Code, command.Id, companyBank.CompanyId, cancellationToken))
                return ApiResponse<CompanyBankDto>.Error(StatusCodes.Status409Conflict, "کد بانک تکراری است");

            var updatedCompanyBank = mapper.Map(command, companyBank);
            if (updatedCompanyBank == null)
                return ApiResponse<CompanyBankDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

            await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation("CompanyBank updated successfully with {@UpdateCompanyBankCommand}", command);

            var updatedCompanyBankDto = mapper.Map<CompanyBankDto>(updatedCompanyBank);
            if (updatedCompanyBankDto == null)
                return ApiResponse<CompanyBankDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

            return ApiResponse<CompanyBankDto>.Ok(updatedCompanyBankDto, "URI بانک با موفقیت به‌روزرسانی شد");
        }

        public async Task<ApiResponse<PagedResult<CompanyBankDto>>> GetAllCompanyBanksAsync(GetAllCompanyBanksQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetAllCompanyBanks is Called");
            var user = userContext.GetCurrentUser();
            if (user == null)
                return ApiResponse<PagedResult<CompanyBankDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

            if (query.CompanyId != 0)
            {
                var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, true, false, cancellationToken);
                if (company is null)
                    return ApiResponse<PagedResult<CompanyBankDto>>.Error(400, "شرکت نامعتبر است");

                if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
                    return ApiResponse<PagedResult<CompanyBankDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
            }
            else if (query.CompanyId == 0)
            {
                if (!user.IsSuperAdmin())
                    return ApiResponse<PagedResult<CompanyBankDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");
            }

            var (companyBanks, totalCount) = await companyBankRepository.GetAllCompanyBanksAsync(
                query.SearchPhrase,
                query.SortBy,
                query.CompanyId,
                true,
                query.PageNumber,
                query.PageSize,
                query.SortDirection,
                cancellationToken);

            var companyBankDto = mapper.Map<IReadOnlyList<CompanyBankDto>>(companyBanks) ?? Array.Empty<CompanyBankDto>();
            if (companyBankDto == null)
                return ApiResponse<PagedResult<CompanyBankDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

            logger.LogInformation("Retrieved {Count} company banks", companyBankDto.Count);

            var data = new PagedResult<CompanyBankDto>(companyBankDto, totalCount, query.PageSize, query.PageNumber);
            return ApiResponse<PagedResult<CompanyBankDto>>.Ok(data, "بانک ها با موفقیت دریافت شدند");
        }

        public async Task<ApiResponse<IReadOnlyList<CompanyBankDto>>> GetCompanyBankByCompanyIdAsync(GetCompanyBankByCompanyIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetCompanyBankByCompanyId is Called with {@Id}", query.CompanyId);

            var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, false, false, cancellationToken);
            if (company is null)
                return ApiResponse<IReadOnlyList<CompanyBankDto>>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

            var user = userContext.GetCurrentUser();
            if (user == null)
                return ApiResponse<IReadOnlyList<CompanyBankDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

            if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
                return ApiResponse<IReadOnlyList<CompanyBankDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

            var companyBanks = await companyBankRepository.GetCompanyBankByCompanyIdAsync(query.CompanyId, cancellationToken);
            if (companyBanks is null)
                return ApiResponse<IReadOnlyList<CompanyBankDto>>.Error(StatusCodes.Status404NotFound, "بانک  یافت نشد");

            var companyBankDtos = mapper.Map<IReadOnlyList<CompanyBankDto>>(companyBanks) ?? Array.Empty<CompanyBankDto>();
            if (companyBankDtos == null)
                return ApiResponse<IReadOnlyList<CompanyBankDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

            logger.LogInformation("Retrieved {Count} company bsnk GetByCompanyId", companyBankDtos.Count);

            return ApiResponse<IReadOnlyList<CompanyBankDto>>.Ok(companyBankDtos, "بانک ها با موفقیت دریافت شدند");
        }

        public async Task<ApiResponse<CompanyBankDto>> GetCompanyBankByIdAsync(GetCompanyBankByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetCompanyBankById is Called with {@Id}", query.Id);

            var companyBank = await companyBankRepository.GetCompanyBankByIdAsync(query.Id, false, false, cancellationToken);
            if (companyBank is null)
                return ApiResponse<CompanyBankDto>.Error(StatusCodes.Status404NotFound, "بانک نامعتبر است");

            var company = await companyRepository.GetCompanyByIdAsync(companyBank.CompanyId, false, false, cancellationToken);
            if (company is null)
                return ApiResponse<CompanyBankDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

            var user = userContext.GetCurrentUser();
            if (user == null)
                return ApiResponse<CompanyBankDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

            if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
                return ApiResponse<CompanyBankDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

            var companyBankDto = mapper.Map<CompanyBankDto>(companyBank);
            if (companyBankDto == null)
                return ApiResponse<CompanyBankDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

            logger.LogInformation("CompanyBank retrieved successfully with {@Id}", query.Id);
            return ApiResponse<CompanyBankDto>.Ok(companyBankDto, "بانک با موفقیت دریافت شد");
        }
    }
}