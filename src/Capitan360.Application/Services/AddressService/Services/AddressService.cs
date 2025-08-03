using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.AddressService.Commands.AddNewAddressToCompany;
using Capitan360.Application.Services.AddressService.Commands.CreateAddress;
using Capitan360.Application.Services.AddressService.Commands.DeleteAddress;
using Capitan360.Application.Services.AddressService.Commands.MoveAddress;
using Capitan360.Application.Services.AddressService.Commands.UpdateAddress;
using Capitan360.Application.Services.AddressService.Dtos;
using Capitan360.Application.Services.AddressService.Queries.GetAddressById;
using Capitan360.Application.Services.AddressService.Queries.GetAllAddresses;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.AddressEntity;
using Capitan360.Domain.Repositories.AddressRepo;
using Capitan360.Domain.Repositories.CompanyRepo;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.AddressService.Services;

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
    public async Task<ApiResponse<int>> CreateAddressAsync(CreateAddressCommand addressCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateAddress is Called with {@CreateAddressCommand}", addressCommand);
        if (addressCommand == null)
            return ApiResponse<int>.Error(400, "ورودی ایجاد ادرس نمی‌تواند null باشد");

        var addressEntity = mapper.Map<Address>(addressCommand);
        if (addressEntity == null)
            return ApiResponse<int>.Error(500, "مشکل در عملیات تبدیل");

        var addressId = await addressRepository.CreateAddressAsync(addressEntity, cancellationToken);
        logger.LogInformation("Address created successfully with ID: {CompanyId}", addressId);
        return ApiResponse<int>.Created(addressId, "Address created successfully");
    }

    public async Task<ApiResponse<PagedResult<AddressDto>>> GetAllAddressesByCompany(GetAllAddressQuery allAddressQuery, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        logger.LogInformation("GetAllAddressesByCompany is Called");
        if (allAddressQuery.PageSize <= 0 || allAddressQuery.PageNumber <= 0)
            return ApiResponse<PagedResult<AddressDto>>.Error(400, "اندازه صفحه یا شماره صفحه نامعتبر است");
        //if (user is not null)
        //{

        //}
        var checkCompany =
            await companyRepository.ValidateCompanyDataWithUserCompanyType(user.CompanyType, allAddressQuery.CompanyId,
                cancellationToken);
        if (!checkCompany)
            return ApiResponse<PagedResult<AddressDto>>.Error(400, "شرکت با این شناسه یافت نشد");

        var (addresses, totalCount) = await addressRepository.GetMatchingAllAddressesByCompany(
            allAddressQuery.SearchPhrase,
            allAddressQuery.CompanyId,
            allAddressQuery.Active,
            allAddressQuery.PageSize,
            allAddressQuery.PageNumber,
            allAddressQuery.SortBy,
            allAddressQuery.SortDirection,
            cancellationToken);
        var addressDto = mapper.Map<IReadOnlyList<AddressDto>>(addresses) ?? Array.Empty<AddressDto>(); ;
        logger.LogInformation("Retrieved {Count} addresses", addressDto.Count);

        var data = new PagedResult<AddressDto>(addressDto, totalCount, allAddressQuery.PageSize, allAddressQuery.PageNumber);
        return ApiResponse<PagedResult<AddressDto>>.Ok(data, "Companies retrieved successfully");
    }

    public async Task<ApiResponse<AddressDto>> GetAddressByIdAsync(GetAddressByIdQuery getAddressByIdQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAddressById is Called with ID: {Id}", getAddressByIdQuery.Id);
        if (getAddressByIdQuery.Id <= 0)
            throw new ArgumentException("شناسه آدرس باید بزرگ‌تر از صفر باشد");
        var address = await addressRepository.GetAddressById(getAddressByIdQuery.Id, cancellationToken);
        if (address is null)
            return ApiResponse<AddressDto>.Error(404, $"آدرس با شناسه {getAddressByIdQuery.Id} یافت نشد");

        var result = mapper.Map<AddressDto>(address);
        logger.LogInformation("Address retrieved successfully with ID: {Id}", getAddressByIdQuery.Id);

        return ApiResponse<AddressDto>.Ok(result, "Company retrieved successfully");
    }

    public async Task<ApiResponse<object>> DeleteAddressAsync(DeleteAddressCommand deleteAddressCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteAddress is Called with ID: {Id}", deleteAddressCommand.Id);

        if (deleteAddressCommand.Id <= 0)
            return ApiResponse<object>.Error(400, "شناسه آدرس باید بزرگ‌تر از صفر باشد");

        var address = await addressRepository.GetAddressById(deleteAddressCommand.Id, cancellationToken);
        if (address is null)
            return ApiResponse<object>.Error(404, $"آدرس با شناسه {deleteAddressCommand.Id} یافت نشد");

        addressRepository.Delete(address);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Address soft-deleted successfully with ID: {Id}", deleteAddressCommand.Id);
        return ApiResponse<object>.Deleted("Company deleted successfully");
    }

    public async Task<ApiResponse<AddressDto>> UpdateAddressAsync(UpdateAddressCommand updateAddressCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateAddress is Called with {@UpdateAddressCommand}", updateAddressCommand);
        if (updateAddressCommand.Id <= 0)
            return ApiResponse<AddressDto>.Error(400, "شناسه آدرس باید بزرگ‌تر از صفر باشد یا ورودی نامعتبر است");

        var address = await addressRepository.GetAddressById(updateAddressCommand.Id, cancellationToken);
        if (address is null)
            return ApiResponse<AddressDto>.Error(404, $"آدرس با شناسه {updateAddressCommand.Id} یافت نشد");

        var updatedAddress = mapper.Map(updateAddressCommand, address);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Address updated successfully with ID: {Id}", updateAddressCommand.Id);

        var updatedCompanyDto = mapper.Map<AddressDto>(updatedAddress);
        return ApiResponse<AddressDto>.Updated(updatedCompanyDto);
    }

    public async Task<ApiResponse<int>> AddNewAddressToCompanyAsync(AddNewAddressToCompanyCommand addNewAddressToCompanyCommand, CancellationToken cancellationToken)

    {
        logger.LogInformation("AddNewAddressToCompany is Called with {@AddNewAddressToCompanyCommand}", addNewAddressToCompanyCommand);

        if (addNewAddressToCompanyCommand.CompanyId <= 0)
            return ApiResponse<int>.Error(400, "شناسه شرکت باید بزرگ‌تر از صفر باشد یا ورودی نامعتبر است");

        // await unitOfWork.BeginTransactionAsync(cancellationToken);
        var company =
            await companyRepository.GetCompanyById(addNewAddressToCompanyCommand.CompanyId, cancellationToken, tracked: true, userCompanyTypeId: addNewAddressToCompanyCommand.UserCompanyTypeId);

        if (company == null)
            return ApiResponse<int>.Error(404, $"شرکت با شناسه {addNewAddressToCompanyCommand.CompanyId} یافت نشد");

        var lastOrderAddress = await addressRepository.OrderAddress(addNewAddressToCompanyCommand.CompanyId, cancellationToken);

        var addressEntity = mapper.Map<Address>(addNewAddressToCompanyCommand);
        if (addressEntity == null)
            return ApiResponse<int>.Error(500, "مشکل در عملیات تبدیل");
        addressEntity.OrderAddress = lastOrderAddress + 1;
        addressEntity.AddressType = AddressType.CompanyAddress;

        var addressId = await addressRepository.CreateAddressAsync(addressEntity, cancellationToken);
        logger.LogInformation("Address created successfully with ID: {CompanyId}", addressId);

        // await unitOfWork.SaveChangesAsync(cancellationToken);
        //  await unitOfWork.CommitTransactionAsync(cancellationToken);

        logger.LogInformation("New address added to company successfully. CompanyId: {CompanyId}, AddressId: {AddressId}", addNewAddressToCompanyCommand.CompanyId, addressId);
        return ApiResponse<int>.Ok(addressId, "Address created successfully");
    }

    public async Task<ApiResponse<object>> MoveAddressUpAsync(MoveAddressUpCommand moveAddressUpCommand, CancellationToken cancellationToken)
    {
        var company =
            await companyRepository.GetCompanyById(moveAddressUpCommand.CompanyId, cancellationToken, false);

        if (company == null)
            return ApiResponse<object>.Error(404, $"شرکت با شناسه {moveAddressUpCommand.CompanyId} یافت نشد");
        await addressRepository.MoveAddressUpAsync(moveAddressUpCommand.CompanyId, moveAddressUpCommand.AddressId, cancellationToken);

        logger.LogInformation("Address moved up successfully. CompanyId: {CompanyId}, AddressId: {AddressId}", moveAddressUpCommand.CompanyId, moveAddressUpCommand.AddressId);
        return ApiResponse<object>.Ok("Address moved up  successfully");
    }

    public async Task<ApiResponse<object>> MoveAddressDownAsync(MoveAddressDownCommand moveAddressDownCommand, CancellationToken cancellationToken)
    {
        var company =
            await companyRepository.GetCompanyById(moveAddressDownCommand.CompanyId, cancellationToken, false);

        if (company == null)
            return ApiResponse<object>.Error(404, $"شرکت با شناسه {moveAddressDownCommand.CompanyId} یافت نشد");
        await addressRepository.MoveAddressDownAsync(moveAddressDownCommand.CompanyId, moveAddressDownCommand.AddressId, cancellationToken);

        logger.LogInformation("Address moved down successfully. CompanyId: {CompanyId}, AddressId: {AddressId}", moveAddressDownCommand.CompanyId, moveAddressDownCommand.AddressId);
        return ApiResponse<object>.Ok("Address moved down  successfully");
    }
}