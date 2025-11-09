using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Addresses.Addresses.Commands.Create;
using Capitan360.Application.Features.Addresses.Addresses.Commands.Delete;
using Capitan360.Application.Features.Addresses.Addresses.Commands.MoveDown;
using Capitan360.Application.Features.Addresses.Addresses.Commands.MoveUp;
using Capitan360.Application.Features.Addresses.Addresses.Commands.Update;
using Capitan360.Application.Features.Addresses.Addresses.Commands.UpdateActiveState;
using Capitan360.Application.Features.Addresses.Addresses.Dtos;
using Capitan360.Application.Features.Addresses.Addresses.Queries.GetAll;
using Capitan360.Application.Features.Addresses.Addresses.Queries.GetByCompanyId;
using Capitan360.Application.Features.Addresses.Addresses.Queries.GetById;
using Capitan360.Application.Features.Addresses.Addresses.Queries.GetByUserId;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Domain.Entities.Addresses;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Addresses;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Capitan360.Domain.Interfaces.Repositories.Identities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Features.Addresses.Addresses.Services;

public class AddressService(
    ILogger<AddressService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    IAddressRepository addressRepository,
    IUserRepository userRepository,
    IAreaRepository areaRepository,
    ICompanyRepository companyRepository
) : IAddressService
{
    public async Task<ApiResponse<int>> CreateAddressAsync(CreateAddressCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateAddress is Called with {@CreateAddressCommand}", command);

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        Company? company = new Company();
        if (command.CompanyId != null)
        {
            company = await companyRepository.GetCompanyByIdAsync((int)command.CompanyId, false, false, cancellationToken);
            if (company == null)
                return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");
        }
        else if (command.UserId != null)
        {
            var selectedUser = await userRepository.GetUserByIdAsync(command.UserId, false, false, cancellationToken);
            if (selectedUser == null)
                return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "کاربر نامعتبر است");

            company = await companyRepository.GetCompanyByIdAsync(selectedUser.CompanyId, false, false, cancellationToken);
            if (company == null)
                return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");
        }

        if (command.CompanyId.HasValue && !user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (command.UserId != null &&
            ((!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id)) ||
             (!user.IsUser(company.Id))))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (!await areaRepository.CheckExistAreaByIdAndParentId(command.CityId, (int)AreaType.City, command.ProvinceId, cancellationToken) ||
            !await areaRepository.CheckExistAreaByIdAndParentId(command.ProvinceId, (int)AreaType.Province, command.CountryId, cancellationToken) ||
            !await areaRepository.CheckExistAreaByIdAndParentId(command.CountryId, (int)AreaType.Country, null, cancellationToken))
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "اطلاعات شهر نامعتبر است");

        int existingCount = command.CompanyId != null ? await addressRepository.GetCountAddressOfCompanyIdAsync((int)command.CompanyId, cancellationToken) :
                                                        await addressRepository.GetCountAddressOfUserAsync(command.UserId!, cancellationToken);

        var address = mapper.Map<Address>(command) ?? null;
        if (address == null)
            return ApiResponse<int>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        address.Order = existingCount + 1;
        address.IsCompanyAddress = true;

        var addressId = await addressRepository.CreateAddressAsync(address, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Address created successfully with {@Address}", address);
        return ApiResponse<int>.Ok(addressId, "آدرس با موفقیت ایجاد شد");
    }

    public async Task<ApiResponse<int>> DeleteAddressAsync(DeleteAddressCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteAddress is Called with {@Id}", command.Id);

        var Address = await addressRepository.GetAddressByIdAsync(command.Id, false, false, cancellationToken);
        if (Address is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "محتوی بار نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        Company? company = new Company();
        if (Address.CompanyId != null)
        {
            company = await companyRepository.GetCompanyByIdAsync((int)Address.CompanyId, false, false, cancellationToken);
            if (company == null)
                return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");
        }
        else if (Address.UserId != null)
        {
            var selectedUser = await userRepository.GetUserByIdAsync(Address.UserId, false, false, cancellationToken);
            if (selectedUser == null)
                return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "کاربر نامعتبر است");

            company = await companyRepository.GetCompanyByIdAsync(selectedUser.CompanyId, false, false, cancellationToken);
            if (company == null)
                return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");
        }

        if (Address.CompanyId.HasValue && !user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (Address.UserId != null &&
            ((!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id)) ||
             (!user.IsUser(company.Id))))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        await addressRepository.DeleteAddressAsync(Address.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Address Deleted successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "محتوی بار با موفقیت حذف شد");
    }

    public async Task<ApiResponse<int>> MoveUpAddressAsync(MoveUpAddressCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("MoveUpAddress is Called with {@Id}", command.Id);

        var address = await addressRepository.GetAddressByIdAsync(command.Id, false, false, cancellationToken);
        if (address == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "محتوی بار نامعتبر است");

        Company? company = new Company();
        if (address.CompanyId != null)
        {
            company = await companyRepository.GetCompanyByIdAsync((int)address.CompanyId, false, false, cancellationToken);
            if (company == null)
                return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");
        }
        else if (address.UserId != null)
        {
            var selectedUser = await userRepository.GetUserByIdAsync(address.UserId, false, false, cancellationToken);
            if (selectedUser == null)
                return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "کاربر نامعتبر است");

            company = await companyRepository.GetCompanyByIdAsync(selectedUser.CompanyId, false, false, cancellationToken);
            if (company == null)
                return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");
        }

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (address.CompanyId.HasValue && !user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (address.UserId != null &&
            ((!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id)) ||
            (!user.IsUser(company.Id))))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (address.Order == 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        var count = address.CompanyId != null ? await addressRepository.GetCountAddressOfCompanyIdAsync((int)address.CompanyId, cancellationToken) :
                                                await addressRepository.GetCountAddressOfUserAsync(address.UserId!, cancellationToken);

        if (count <= 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        await addressRepository.MoveAddressUpAsync(command.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Address moved up successfully with {@AddressId}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "آدرس با موفقیت جابجا شد");
    }

    public async Task<ApiResponse<int>> MoveDownAddressAsync(MoveDownAddressCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("MoveDownAddress is Called with {@Id}", command.Id);

        var address = await addressRepository.GetAddressByIdAsync(command.Id, false, false, cancellationToken);
        if (address == null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "محتوی بار نامعتبر است");

        Company? company = new Company();
        if (address.CompanyId != null)
        {
            company = await companyRepository.GetCompanyByIdAsync((int)address.CompanyId, false, false, cancellationToken);
            if (company == null)
                return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");
        }
        else if (address.UserId != null)
        {
            var selectedUser = await userRepository.GetUserByIdAsync(address.UserId, false, false, cancellationToken);
            if (selectedUser == null)
                return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "کاربر نامعتبر است");

            company = await companyRepository.GetCompanyByIdAsync(selectedUser.CompanyId, false, false, cancellationToken);
            if (company == null)
                return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");
        }

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (address.CompanyId.HasValue && !user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (address.UserId != null &&
            ((!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id)) ||
            (!user.IsUser(company.Id))))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var count = address.CompanyId != null ? await addressRepository.GetCountAddressOfCompanyIdAsync((int)address.CompanyId, cancellationToken) :
                                                await addressRepository.GetCountAddressOfUserAsync(address.UserId!, cancellationToken);

        if (address.Order == count)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        if (count <= 1)
            return ApiResponse<int>.Ok(command.Id, "انجام شد");

        await addressRepository.MoveAddressDownAsync(command.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Address moved down successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "آدرس با موفقیت جابجا شد");
    }

    public async Task<ApiResponse<int>> SetAddressActivityStatusAsync(UpdateActiveStateAddressCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetAddressActivityStatus Called with {@Id}", command.Id);

        var address = await addressRepository.GetAddressByIdAsync(command.Id, false, true, cancellationToken);
        if (address is null)
            return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "محتوی بار نامعتبر است");

        Company? company = new Company();
        if (address.CompanyId != null)
        {
            company = await companyRepository.GetCompanyByIdAsync((int)address.CompanyId, false, false, cancellationToken);
            if (company == null)
                return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");
        }
        else if (address.UserId != null)
        {
            var selectedUser = await userRepository.GetUserByIdAsync(address.UserId, false, false, cancellationToken);
            if (selectedUser == null)
                return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "کاربر نامعتبر است");

            company = await companyRepository.GetCompanyByIdAsync(selectedUser.CompanyId, false, false, cancellationToken);
            if (company == null)
                return ApiResponse<int>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");
        }

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<int>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (address.CompanyId.HasValue && !user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (address.UserId != null &&
            ((!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id)) ||
            (!user.IsUser(company.Id))))
            return ApiResponse<int>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        address.Active = !address.Active;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Address activity status updated successfully with {@Id}", command.Id);
        return ApiResponse<int>.Ok(command.Id, "وضعیت آدرس با موفقیت به‌روزرسانی شد");
    }

    public async Task<ApiResponse<PagedResult<AddressDto>>> GetAllAddresssAsync(GetAllAddressQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllAddresss is Called");

        Company? company = new Company();
        if (query.CompanyId != null)
        {
            company = await companyRepository.GetCompanyByIdAsync((int)query.CompanyId, false, false, cancellationToken);
            if (company == null)
                return ApiResponse<PagedResult<AddressDto>>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");
        }
        else if (query.UserId != null)
        {
            var selectedUser = await userRepository.GetUserByIdAsync(query.UserId, false, false, cancellationToken);
            if (selectedUser == null)
                return ApiResponse<PagedResult<AddressDto>>.Error(StatusCodes.Status404NotFound, "کاربر نامعتبر است");

            company = await companyRepository.GetCompanyByIdAsync(selectedUser.CompanyId, false, false, cancellationToken);
            if (company == null)
                return ApiResponse<PagedResult<AddressDto>>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");
        }

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<PagedResult<AddressDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (query.CompanyId.HasValue && !user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<PagedResult<AddressDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (query.UserId != null &&
            ((!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id)) ||
            (!user.IsUser(company.Id))))
            return ApiResponse<PagedResult<AddressDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var (addresss, totalCount) = await addressRepository.GetAllAddresssesAsync(
            query.SearchPhrase,
            query.SortBy,
            query.CompanyId,
            query.UserId,
            true,
            query.PageNumber,
            query.PageSize,
            query.SortDirection,
            cancellationToken);

        var addressDtos = mapper.Map<IReadOnlyList<AddressDto>>(addresss) ?? Array.Empty<AddressDto>();
        if (addressDtos == null)
            return ApiResponse<PagedResult<AddressDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} adresses", addressDtos.Count);

        var data = new PagedResult<AddressDto>(addressDtos, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<AddressDto>>.Ok(data, "آدرس ها با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<IReadOnlyList<AddressDto>>> GetAddressByCompanyIdAsync(GetAddressByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAddressByCompanyId is Called with {@Id}", query.CompanyId);

        var company = await companyRepository.GetCompanyByIdAsync(query.CompanyId, false, false, cancellationToken);
        if (company is null)
            return ApiResponse<IReadOnlyList<AddressDto>>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<IReadOnlyList<AddressDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<IReadOnlyList<AddressDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var addresss = await addressRepository.GetAddressByCompanyIdAsync(query.CompanyId, cancellationToken);
        if (addresss is null)
            return ApiResponse<IReadOnlyList<AddressDto>>.Error(StatusCodes.Status404NotFound, "محتوی بار یافت نشد");

        var addressDtos = mapper.Map<IReadOnlyList<AddressDto>>(addresss) ?? Array.Empty<AddressDto>();
        if (addressDtos == null)
            return ApiResponse<IReadOnlyList<AddressDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} addresses GetByCompanyId", addressDtos.Count);

        return ApiResponse<IReadOnlyList<AddressDto>>.Ok(addressDtos, "محتوهای بار با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<IReadOnlyList<AddressDto>>> GetAddressByUserIdAsync(GetAddressByUserIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAddressByUserId is Called with {@Id}", query.UserId);

        var selectedUser = await userRepository.GetUserByIdAsync(query.UserId, false, false, cancellationToken);
        if (selectedUser == null)
            return ApiResponse<IReadOnlyList<AddressDto>>.Error(StatusCodes.Status404NotFound, "کاربر نامعتبر است");

        var company = await companyRepository.GetCompanyByIdAsync(selectedUser.CompanyId, false, false, cancellationToken);
        if (company == null)
            return ApiResponse<IReadOnlyList<AddressDto>>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<IReadOnlyList<AddressDto>>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if ((!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id)) ||
            (!user.IsUser(company.Id)))
            return ApiResponse<IReadOnlyList<AddressDto>>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var addresss = await addressRepository.GetAddressByUserIdAsync(query.UserId, cancellationToken);
        if (addresss is null)
            return ApiResponse<IReadOnlyList<AddressDto>>.Error(StatusCodes.Status404NotFound, "محتوی بار یافت نشد");

        var addressDtos = mapper.Map<IReadOnlyList<AddressDto>>(addresss) ?? Array.Empty<AddressDto>();
        if (addressDtos == null)
            return ApiResponse<IReadOnlyList<AddressDto>>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Retrieved {Count} addresses GetByUserId", addressDtos.Count);

        return ApiResponse<IReadOnlyList<AddressDto>>.Ok(addressDtos, "آدرس ها با موفقیت دریافت شدند");
    }

    public async Task<ApiResponse<AddressDto>> GetAddressByIdAsync(GetAddressByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAddressById is Called with {@Id}", query.Id);

        var address = await addressRepository.GetAddressByIdAsync(query.Id, false, false, cancellationToken);
        if (address is null)
            return ApiResponse<AddressDto>.Error(StatusCodes.Status404NotFound, "محتوی بار نامعتبر است");

        Company? company = new Company();
        if (address.CompanyId != null)
        {
            company = await companyRepository.GetCompanyByIdAsync((int)address.CompanyId, false, false, cancellationToken);
            if (company == null)
                return ApiResponse<AddressDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");
        }
        else if (address.UserId != null)
        {
            var selectedUser = await userRepository.GetUserByIdAsync(address.UserId, false, false, cancellationToken);
            if (selectedUser == null)
                return ApiResponse<AddressDto>.Error(StatusCodes.Status404NotFound, "کاربر نامعتبر است");

            company = await companyRepository.GetCompanyByIdAsync(selectedUser.CompanyId, false, false, cancellationToken);
            if (company == null)
                return ApiResponse<AddressDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");
        }

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<AddressDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (address.CompanyId.HasValue && !user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<AddressDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (address.UserId != null &&
            ((!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id)) ||
            (!user.IsUser(company.Id))))
            return ApiResponse<AddressDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        var addressDto = mapper.Map<AddressDto>(address);
        if (addressDto == null)
            return ApiResponse<AddressDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        logger.LogInformation("Address retrieved successfully with {@Id}", query.Id);
        return ApiResponse<AddressDto>.Ok(addressDto, "محتوی بار با موفقیت دریافت شد");
    }

    public async Task<ApiResponse<AddressDto>> UpdateAddressAsync(UpdateAddressCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateAddress is Called with {@UpdateAddressCommand}", command);

        var address = await addressRepository.GetAddressByIdAsync(command.Id, false, true, cancellationToken);
        if (address == null)
            return ApiResponse<AddressDto>.Error(StatusCodes.Status404NotFound, "محتوی بار نامعتبر است");

        Company? company = new Company();
        if (address.CompanyId != null)
        {
            company = await companyRepository.GetCompanyByIdAsync((int)address.CompanyId, false, false, cancellationToken);
            if (company == null)
                return ApiResponse<AddressDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");
        }
        else if (address.UserId != null)
        {
            var selectedUser = await userRepository.GetUserByIdAsync(address.UserId, false, false, cancellationToken);
            if (selectedUser == null)
                return ApiResponse<AddressDto>.Error(StatusCodes.Status404NotFound, "کاربر نامعتبر است");

            company = await companyRepository.GetCompanyByIdAsync(selectedUser.CompanyId, false, false, cancellationToken);
            if (company == null)
                return ApiResponse<AddressDto>.Error(StatusCodes.Status404NotFound, "شرکت نامعتبر است");
        }

        var user = userContext.GetCurrentUser();
        if (user == null)
            return ApiResponse<AddressDto>.Error(StatusCodes.Status401Unauthorized, "مشکل در احراز هویت کاربر");

        if (address.CompanyId.HasValue && !user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id))
            return ApiResponse<AddressDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (address.UserId != null &&
            ((!user.IsSuperAdmin() && !user.IsSuperManager(company.CompanyTypeId) && !user.IsManager(company.Id)) ||
            (!user.IsUser(company.Id))))
            return ApiResponse<AddressDto>.Error(StatusCodes.Status403Forbidden, "مجوز این فعالیت را ندارید");

        if (!await areaRepository.CheckExistAreaByIdAndParentId(command.MunicipalAreaId, (int)AreaType.RegionMunicipality, address.CityId, cancellationToken))
            return ApiResponse<AddressDto>.Error(StatusCodes.Status404NotFound, "اطلاعات منطقه شهر نامعتبر است");

        var updatedComapnyAddress = mapper.Map(command, address);
        if (updatedComapnyAddress is null)
            return ApiResponse<AddressDto>.Error(StatusCodes.Status500InternalServerError, "خطا در عملیات تبدیل");

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Address updated successfully with {@UpdateAddressCommand}", command);

        var updatedComapnyAddressDto = mapper.Map<AddressDto>(updatedComapnyAddress);
        if (updatedComapnyAddressDto == null)
            return ApiResponse<AddressDto>.Error(StatusCodes.Status500InternalServerError, "مشکل در عملیات تبدیل");

        return ApiResponse<AddressDto>.Ok(updatedComapnyAddressDto, "آدرس با موفقیت به‌روزرسانی شد");
    }

}