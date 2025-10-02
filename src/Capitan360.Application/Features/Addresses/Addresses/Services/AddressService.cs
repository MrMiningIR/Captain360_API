using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Addresses.Addresses.Commands.AddNewAddressToCompany;
using Capitan360.Application.Features.Addresses.Addresses.Commands.Create;
using Capitan360.Application.Features.Addresses.Addresses.Commands.Delete;
using Capitan360.Application.Features.Addresses.Addresses.Commands.Move;
using Capitan360.Application.Features.Addresses.Addresses.Commands.Update;
using Capitan360.Application.Features.Addresses.Addresses.Dtos;
using Capitan360.Application.Features.Addresses.Addresses.Queries.GetAll;
using Capitan360.Application.Features.Addresses.Addresses.Queries.GetById;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Domain.Entities.Addresses;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Addresses;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Features.Addresses.Addresses.Services;

public class AddressService(
    ILogger<AddressService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    IAddressRepository addressRepository,
    ICompanyAddressRepository companyAddressRepository,
    //  ICompanyAddressService companyAddressService,
    ICompanyRepository companyRepository
) : IAddressService
{
    public async Task<ApiResponse<int>> CreateAddressAsync(CreateAddressCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateAddress is Called with {@CreateAddressCommand}", command);
        if (command == null)
            return ApiResponse<int>.Error(400, "ورودی ایجاد ادرس نمی‌تواند null باشد");

        var addressEntity = mapper.Map<Address>(command);
        if (addressEntity == null)
            return ApiResponse<int>.Error(500, "مشکل در عملیات تبدیل");

        var addressId = await addressRepository.CreateAddressAsync(addressEntity, cancellationToken);
        logger.LogInformation("Address created successfully with ID: {CompanyId}", addressId);
        return ApiResponse<int>.Created(addressId, "Address created successfully");
    }

    public async Task<ApiResponse<PagedResult<AddressDto>>> GetAllAddressesByCompany(GetAllAddressQuery query, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        logger.LogInformation("GetAllAddressesByCompany is Called");
        if (query.PageSize <= 0 || query.PageNumber <= 0)
            return ApiResponse<PagedResult<AddressDto>>.Error(400, "اندازه صفحه یا شماره صفحه نامعتبر است");
        //if (user is not null)
        //{

        //}
        var checkCompany =
            await companyRepository.ValidateCompanyDataWithUserCompanyTypeAsync(user.CompanyType, query.CompanyId,
                cancellationToken);
        if (!checkCompany)
            return ApiResponse<PagedResult<AddressDto>>.Error(400, "شرکت با این شناسه یافت نشد");

        var (addresses, totalCount) = await addressRepository.GetAllAddressesByCompany(
            query.SearchPhrase,
            query.CompanyId,
            query.Active,
            query.PageSize,
            query.PageNumber,
            query.SortBy,
            query.SortDirection,
            cancellationToken);
        var addressDto = mapper.Map<IReadOnlyList<AddressDto>>(addresses) ?? Array.Empty<AddressDto>(); ;
        logger.LogInformation("Retrieved {Count} addresses", addressDto.Count);

        var data = new PagedResult<AddressDto>(addressDto, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<AddressDto>>.Ok(data, "Companies retrieved successfully");
    }

    public async Task<ApiResponse<AddressDto>> GetAddressByIdAsync(GetAddressByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAddressById is Called with ID: {Id}", query.Id);
        if (query.Id <= 0)
            throw new ArgumentException("شناسه آدرس باید بزرگ‌تر از صفر باشد");
        var address = await addressRepository.GetAddressById(query.Id, cancellationToken);
        if (address is null)
            return ApiResponse<AddressDto>.Error(400, $"آدرس با شناسه {query.Id} یافت نشد");

        var result = mapper.Map<AddressDto>(address);
        logger.LogInformation("Address retrieved successfully with ID: {Id}", query.Id);

        return ApiResponse<AddressDto>.Ok(result, "Company retrieved successfully");
    }

    public async Task<ApiResponse<object>> DeleteAddressAsync(DeleteAddressCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteAddress is Called with ID: {Id}", command.Id);

        if (command.Id <= 0)
            return ApiResponse<object>.Error(400, "شناسه آدرس باید بزرگ‌تر از صفر باشد");

        var address = await addressRepository.GetAddressById(command.Id, cancellationToken);
        if (address is null)
            return ApiResponse<object>.Error(400, $"آدرس با شناسه {command.Id} یافت نشد");

        addressRepository.Delete(address);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Address soft-deleted successfully with ID: {Id}", command.Id);
        return ApiResponse<object>.Deleted("Company deleted successfully");
    }

    public async Task<ApiResponse<AddressDto>> UpdateAddressAsync(UpdateAddressCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateAddress is Called with {@UpdateAddressCommand}", command);
        if (command.Id <= 0)
            return ApiResponse<AddressDto>.Error(400, "شناسه آدرس باید بزرگ‌تر از صفر باشد یا ورودی نامعتبر است");

        var address = await addressRepository.GetAddressById(command.Id, cancellationToken);
        if (address is null)
            return ApiResponse<AddressDto>.Error(400, $"آدرس با شناسه {command.Id} یافت نشد");

        var updatedAddress = mapper.Map(command, address);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Address updated successfully with ID: {Id}", command.Id);

        var updatedCompanyDto = mapper.Map<AddressDto>(updatedAddress);
        return ApiResponse<AddressDto>.Updated(updatedCompanyDto);
    }

    public async Task<ApiResponse<int>> AddNewAddressToCompanyAsync(AddNewAddressToCompanyCommand command, CancellationToken cancellationToken)

    {
        logger.LogInformation("AddNewAddressToCompany is Called with {@AddNewAddressToCompanyCommand}", command);

        if (command.CompanyId <= 0)
            return ApiResponse<int>.Error(400, "شناسه شرکت باید بزرگ‌تر از صفر باشد یا ورودی نامعتبر است");

        // await unitOfWork.BeginTransactionAsync(cancellationToken);
        var company =
            await companyRepository.GetCompanyByIdAsync(command.CompanyId, tracked: false, loadData: false, cancellationToken: cancellationToken);

        if (company == null)
            return ApiResponse<int>.Error(400, $"شرکت با شناسه {command.CompanyId} یافت نشد");

        var lastOrderAddress = await addressRepository.OrderAddress(command.CompanyId, cancellationToken);

        var addressEntity = mapper.Map<Address>(command);
        if (addressEntity == null)
            return ApiResponse<int>.Error(500, "مشکل در عملیات تبدیل");
        addressEntity.Order = lastOrderAddress + 1;
        //addressEntity.AddressType = AddressType.CompanyAddress;

        var addressId = await addressRepository.CreateAddressAsync(addressEntity, cancellationToken);
        logger.LogInformation("Address created successfully with ID: {CompanyId}", addressId);

        // await unitOfWork.SaveChangesAsync(cancellationToken);
        //  await unitOfWork.CommitTransactionAsync(cancellationToken);

        logger.LogInformation("New address added to company successfully. CompanyId: {CompanyId}, AddressId: {AddressId}", command.CompanyId, addressId);
        return ApiResponse<int>.Ok(addressId, "Address created successfully");
    }

    public async Task<ApiResponse<object>> MoveAddressUpAsync(MoveAddressUpCommand command, CancellationToken cancellationToken)
    {
        var company =
            await companyRepository.GetCompanyByIdAsync(command.CompanyId, false, false, cancellationToken);

        if (company == null)
            return ApiResponse<object>.Error(400, $"شرکت با شناسه {command.CompanyId} یافت نشد");
        await addressRepository.MoveAddressUpAsync(command.CompanyId, command.AddressId, cancellationToken);

        logger.LogInformation("Address moved up successfully. CompanyId: {CompanyId}, AddressId: {AddressId}", command.CompanyId, command.AddressId);
        return ApiResponse<object>.Ok("Address moved up  successfully");
    }

    public async Task<ApiResponse<object>> MoveAddressDownAsync(MoveAddressDownCommand command, CancellationToken cancellationToken)
    {
        var company =
            await companyRepository.GetCompanyByIdAsync(command.CompanyId, false, false, cancellationToken);

        if (company == null)
            return ApiResponse<object>.Error(400, $"شرکت با شناسه {command.CompanyId} یافت نشد");
        await addressRepository.MoveAddressDownAsync(command.CompanyId, command.AddressId, cancellationToken);

        logger.LogInformation("Address moved down successfully. CompanyId: {CompanyId}, AddressId: {AddressId}", command.CompanyId, command.AddressId);
        return ApiResponse<object>.Ok("Address moved down  successfully");
    }
}