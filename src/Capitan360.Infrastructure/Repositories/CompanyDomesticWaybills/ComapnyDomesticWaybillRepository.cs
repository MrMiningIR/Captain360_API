using Capitan360.Application.Utils;
using Capitan360.Domain.Entities.CompanyDomesticWaybills;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.CompanyDomesticWaybills;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.CompanyDomesticWaybills;

public class CompanyDomesticWaybillRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : ICompanyDomesticWaybillRepository
{
    public async Task<CompanyDomesticWaybill?> GetCompanyDomesticWaybillByIdAsync(int companyDomesticWaybillId, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyDomesticWaybill> query = dbContext.CompanyDomesticWaybills;

        if (loadDataGeneralData)
        {
            query = query.Include(item => item.CompanyManifestForm);
            query = query.Include(item => item.CompanyReceiver);
            query = query.Include(item => item.CompanySender);
            query = query.Include(item => item.DestinationCity);
            query = query.Include(item => item.CompanyDomesticWaybillPackageTypes);
            query = query.Include(item => item.SourceCity);
        }

        if (loadDataSenderCompany)
        {
            query = query.Include(item => item.BikeDeliveryInSenderCompany);
            query = query.Include(item => item.CompanyDomesticWaybillPeriod);
            query = query.Include(item => item.CompanySenderBank);
            query = query.Include(item => item.Counter);
            query = query.Include(item => item.CustomerPanel);
            query = query.Include(item => item.CompanyInsurance);
        }

        if (loadDataReceiverCompany)
        {
            query = query.Include(item => item.BikeDeliveryInReceiverCompany);
            query = query.Include(item => item.CompanyReceiverBank);
            query = query.Include(item => item.CompanyReceiverResponsibleCustomer);
        }

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == companyDomesticWaybillId, cancellationToken);
    }

    public async Task<CompanyDomesticWaybill?> GetCompanyDomesticWaybillByNoAsync(long companyDomesticWaybillNo, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyDomesticWaybill> query = dbContext.CompanyDomesticWaybills;

        if (loadDataGeneralData)
        {
            query = query.Include(item => item.CompanyManifestForm);
            query = query.Include(item => item.CompanyReceiver);
            query = query.Include(item => item.CompanySender);
            query = query.Include(item => item.DestinationCity);
            query = query.Include(item => item.CompanyDomesticWaybillPackageTypes);
            query = query.Include(item => item.SourceCity);
        }

        if (loadDataSenderCompany)
        {
            query = query.Include(item => item.BikeDeliveryInSenderCompany);
            query = query.Include(item => item.CompanyDomesticWaybillPeriod);
            query = query.Include(item => item.CompanySenderBank);
            query = query.Include(item => item.Counter);
            query = query.Include(item => item.CustomerPanel);
            query = query.Include(item => item.CompanyInsurance);
        }

        if (loadDataReceiverCompany)
        {
            query = query.Include(item => item.BikeDeliveryInReceiverCompany);
            query = query.Include(item => item.CompanyReceiverBank);
            query = query.Include(item => item.CompanyReceiverResponsibleCustomer);
        }

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.No == companyDomesticWaybillNo, cancellationToken);
    }

    public async Task<CompanyDomesticWaybill?> GetCompanyDomesticWaybillByNoAndCompanySenderIdAsync(long companyDomesticWaybillNo, int companySenderId, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyDomesticWaybill> query = dbContext.CompanyDomesticWaybills;

        if (loadDataGeneralData)
        {
            query = query.Include(item => item.CompanyManifestForm);
            query = query.Include(item => item.CompanyReceiver);
            query = query.Include(item => item.CompanySender);
            query = query.Include(item => item.DestinationCity);
            query = query.Include(item => item.CompanyDomesticWaybillPackageTypes);
            query = query.Include(item => item.SourceCity);
        }

        if (loadDataSenderCompany)
        {
            query = query.Include(item => item.BikeDeliveryInSenderCompany);
            query = query.Include(item => item.CompanyDomesticWaybillPeriod);
            query = query.Include(item => item.CompanySenderBank);
            query = query.Include(item => item.Counter);
            query = query.Include(item => item.CustomerPanel);
            query = query.Include(item => item.CompanyInsurance);
        }

        if (loadDataReceiverCompany)
        {
            query = query.Include(item => item.BikeDeliveryInReceiverCompany);
            query = query.Include(item => item.CompanyReceiverBank);
            query = query.Include(item => item.CompanyReceiverResponsibleCustomer);
        }

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.No == companyDomesticWaybillNo && item.CompanySenderId == companySenderId, cancellationToken);
    }

    public async Task<CompanyDomesticWaybill?> GetCompanyDomesticWaybillByNoAndCompanyReceiverIdAsync(long companyDomesticWaybillNo, int companyReceiverId, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyDomesticWaybill> query = dbContext.CompanyDomesticWaybills;

        if (loadDataGeneralData)
        {
            query = query.Include(item => item.CompanyManifestForm);
            query = query.Include(item => item.CompanyReceiver);
            query = query.Include(item => item.CompanySender);
            query = query.Include(item => item.DestinationCity);
            query = query.Include(item => item.CompanyDomesticWaybillPackageTypes);
            query = query.Include(item => item.SourceCity);
        }

        if (loadDataSenderCompany)
        {
            query = query.Include(item => item.BikeDeliveryInSenderCompany);
            query = query.Include(item => item.CompanyDomesticWaybillPeriod);
            query = query.Include(item => item.CompanySenderBank);
            query = query.Include(item => item.Counter);
            query = query.Include(item => item.CustomerPanel);
            query = query.Include(item => item.CompanyInsurance);
        }

        if (loadDataReceiverCompany)
        {
            query = query.Include(item => item.BikeDeliveryInReceiverCompany);
            query = query.Include(item => item.CompanyReceiverBank);
            query = query.Include(item => item.CompanyReceiverResponsibleCustomer);
        }

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.No == companyDomesticWaybillNo && item.CompanyReceiverId == companyReceiverId, cancellationToken);
    }

    public async Task<bool> CheckExistCompanyDomesticWaybillByIdAsync(int companyDomesticWaybillId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyDomesticWaybills.AnyAsync(item => item.Id == companyDomesticWaybillId, cancellationToken);
    }

    public async Task<bool> CheckExistCompanyDomesticWaybillByNoAsync(long companyDomesticWaybillNo, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyDomesticWaybills.AnyAsync(item => item.No == companyDomesticWaybillNo, cancellationToken);
    }

    public async Task<bool> CheckExistCompanyDomesticWaybillByNoAndCompanySenderIdAsync(long companyDomesticWaybillNo, int companySenderId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyDomesticWaybills.AnyAsync(item => item.No == companyDomesticWaybillNo && item.CompanySenderId == companySenderId, cancellationToken);
    }

    public async Task<bool> CheckExistCompanyDomesticWaybillByNoAndCompanyReceiverIdAsync(long companyDomesticWaybillNo, int companyReceiverId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyDomesticWaybills.AnyAsync(item => item.No == companyDomesticWaybillNo && item.CompanyReceiverId == companyReceiverId, cancellationToken);
    }

    public async Task<int> InsertCompanyDomesticWaybillAsync(CompanyDomesticWaybill companyDomesticWaybill, CancellationToken cancellationToken)
    {
        dbContext.CompanyDomesticWaybills.Add(companyDomesticWaybill);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyDomesticWaybill.Id;
    }

    public async Task DeleteCompanyDomesticWaybillAsync(int companyDomesticWaybillId, CancellationToken cancellationToken)
    {
        var companyDomesticWaybillPackageTypes = await dbContext.CompanyDomesticWaybillPackageTypes.Where(item => item.CompanyDomesticWaybillId == companyDomesticWaybillId).ToListAsync(cancellationToken);
        if (companyDomesticWaybillPackageTypes == null)
            return;

        dbContext.CompanyDomesticWaybillPackageTypes.RemoveRange(companyDomesticWaybillPackageTypes);

        var companyDomesticWaybill = await dbContext.CompanyDomesticWaybills.SingleOrDefaultAsync(item => item.Id == companyDomesticWaybillId, cancellationToken);
        if (companyDomesticWaybill == null)
            return;

        dbContext.CompanyDomesticWaybills.Remove(companyDomesticWaybill);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<int>> GetCompanyDomesticWaybillIdByCompanyManifestFormIdAsync(int companyManifestFormId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyDomesticWaybills.Where(item => item.CompanyManifestFormId.HasValue && item.CompanyManifestFormId == companyManifestFormId)
                                               .Select(item => item.Id)
                                               .ToListAsync(cancellationToken);
    }

    public async Task<List<int>> GetCompanyDomesticWaybillIdByCompanyDomesticWaybillPeriodIdAndLessThanStartNumberAsync(int companyDomesticWaybillPeriodId, long startNumber, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyDomesticWaybills.Where(item => item.CompanyDomesticWaybillPeriodId == companyDomesticWaybillPeriodId && item.No < startNumber)
                                               .Select(item => item.Id)
                                               .ToListAsync(cancellationToken);
    }

    public async Task<List<int>> GetCompanyDomesticWaybillIdByCompanyDomesticWaybillPeriodIdAndGreatherThanEndNumberAsync(int companyDomesticWaybillPeriodId, long endNumber, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyDomesticWaybills.Where(item => item.CompanyDomesticWaybillPeriodId == companyDomesticWaybillPeriodId && item.No > endNumber)
                                               .Select(item => item.Id)
                                               .ToListAsync(cancellationToken);
    }

    public async Task<List<int>> GetCompanyDomesticWaybillIdByCompanyDomesticWaybillPeriodIdAsync(int companyDomesticWaybillPeriodId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyDomesticWaybills.Where(item => item.CompanyDomesticWaybillPeriodId == companyDomesticWaybillPeriodId)
                                               .Select(item => item.Id)
                                               .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsCompanyManifestStatusConsistentByCompanyManifestFormIdStateAsync(int companyManifestFormId, short state, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyDomesticWaybills.AnyAsync(item => item.CompanyManifestFormId.HasValue && item.CompanyManifestFormId == companyManifestFormId && item.State == state);
    }

    public async Task<bool> AnyIssunedCompanyDomesticWaybillByCompanyDomesticPeriodIdStartNumberEndNumberAsync(int companyDomesticWaybillPeriod, long startNumber, long endNumber, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyDomesticWaybills.AnyAsync(item => item.CompanyDomesticWaybillPeriodId == companyDomesticWaybillPeriod && (item.No < startNumber || item.No > endNumber) && item.State != (short)CompanyDomesticWaybillState.Ready);
    }

    public async Task<bool> AnyIssunedCompanyDomesticWaybillByCompanyDomesticPeriodIdAsync(int companyDomesticWaybillPeriod, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyDomesticWaybills.AnyAsync(item => item.CompanyDomesticWaybillPeriodId == companyDomesticWaybillPeriod && item.State != (short)CompanyDomesticWaybillState.Ready);
    }

    public async Task ChangeStateCompanyDomesticWaybillAsync(List<int> companyDomesticWaybills, short state, CancellationToken cancellationToken)
    {
        foreach (var waybillId in companyDomesticWaybills)
        {
            var waybill = await dbContext.CompanyDomesticWaybills.SingleOrDefaultAsync(item => item.Id == waybillId);
            if (waybill == null)
                return;

            switch (state)
            {
                case (short)CompanyDomesticWaybillState.Issued:
                    waybill.DateIssued = Tools.GetTodayInPersianDate();
                    waybill.TimeIssued = Tools.GetTime();
                    break;
                case (short)CompanyDomesticWaybillState.Collectiong:
                    waybill.DateCollectiong = Tools.GetTodayInPersianDate();
                    waybill.TimeCollectiong = Tools.GetTime();
                    break;
                case (short)CompanyDomesticWaybillState.ReceivedAtSenderCompany:
                    waybill.DateReceivedAtSenderCompany = Tools.GetTodayInPersianDate();
                    waybill.TimeReceivedAtSenderCompany = Tools.GetTime();
                    break;
                case (short)CompanyDomesticWaybillState.Manifested:
                    waybill.DateManifested = Tools.GetTodayInPersianDate();
                    waybill.TimeManifested = Tools.GetTime();
                    break;
                case (short)CompanyDomesticWaybillState.AirlineDelivery:
                    waybill.DateAirlineDelivery = Tools.GetTodayInPersianDate();
                    waybill.TimeAirlineDelivery = Tools.GetTime();
                    break;
                case (short)CompanyDomesticWaybillState.ReceivedAtReceiverCompany:
                    waybill.DateReceivedAtReceiverCompany = Tools.GetTodayInPersianDate();
                    waybill.TimeReceivedAtReceiverCompany = Tools.GetTime();
                    break;
                case (short)CompanyDomesticWaybillState.Distribution:
                    waybill.DateDistribution = Tools.GetTodayInPersianDate();
                    waybill.TimeDistribution = Tools.GetTime();
                    break;
                case (short)CompanyDomesticWaybillState.Delivery:
                    waybill.DateDelivery = Tools.GetTodayInPersianDate();
                    waybill.TimeDelivery = Tools.GetTime();
                    break;
            }

            waybill.State = state;
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task BackCompanyDomesticWaybillFromStateAsync(List<int> companyDomesticWaybills, short state, CancellationToken cancellationToken)
    {
        foreach (var waybillId in companyDomesticWaybills)
        {
            var waybill = await dbContext.CompanyDomesticWaybills.SingleOrDefaultAsync(item => item.Id == waybillId);
            if (waybill == null)
                return;

            switch (state)
            {
                case (short)CompanyDomesticWaybillState.Issued:
                    waybill.DateCollectiong = null;
                    waybill.TimeCollectiong = null;
                    waybill.BikeDeliveryInSenderCompanyId = null;
                    waybill.BikeDeliveryInSenderCompanyAgent = null;
                    if (waybill.State == (short)CompanyDomesticWaybillState.ReceivedAtSenderCompany)
                    {
                        waybill.DateReceivedAtSenderCompany = null;
                        waybill.TimeReceivedAtSenderCompany = null;
                    }
                    break;
                case (short)CompanyDomesticWaybillState.Collectiong:
                    waybill.DateReceivedAtSenderCompany = null;
                    waybill.TimeReceivedAtSenderCompany = null;
                    break;
                case (short)CompanyDomesticWaybillState.ReceivedAtSenderCompany:
                    waybill.CompanyManifestFormId = null;
                    waybill.DateManifested = null;
                    waybill.TimeManifested = null;
                    break;
                case (short)CompanyDomesticWaybillState.Manifested:
                    waybill.DateAirlineDelivery = null;
                    waybill.TimeAirlineDelivery = null;
                    break;
                case (short)CompanyDomesticWaybillState.AirlineDelivery:
                    waybill.DateReceivedAtReceiverCompany = null;
                    waybill.TimeReceivedAtReceiverCompany = null;
                    break;
                case (short)CompanyDomesticWaybillState.ReceivedAtReceiverCompany:
                    waybill.DateDistribution = null;
                    waybill.TimeDistribution = null;
                    waybill.BikeDeliveryInReceiverCompanyId = null;
                    waybill.BikeDeliveryInReceiverCompanyAgent = null;
                    if (waybill.State == (short)CompanyDomesticWaybillState.Delivery)
                    {
                        waybill.DateDelivery = null;
                        waybill.TimeDelivery = null;
                        waybill.CompanyReceiverDateFinancial = null;
                        waybill.CompanyReceiverCashPayment = null;
                        waybill.CompanyReceiverBankPayment = null;
                        waybill.CompanyReceiverCashOnDelivery = null;
                        waybill.CompanyReceiverBankId = null;
                        waybill.CompanyReceiverBankPaymentNo = null;
                        waybill.CompanyReceiverCreditPayment = null;
                        waybill.CompanyReceiverResponsibleCustomerId = null;
                        waybill.EntranceDeliveryPerson = null;
                        waybill.EntranceTransfereePersonName = null;
                        waybill.EntranceTransfereePersonNationalCode = null;
                    }
                    break;
                case (short)CompanyDomesticWaybillState.Distribution:
                    waybill.DateDelivery = null;
                    waybill.TimeDelivery = null;
                    waybill.CompanyReceiverDateFinancial = null;
                    waybill.CompanyReceiverCashPayment = null;
                    waybill.CompanyReceiverBankPayment = null;
                    waybill.CompanyReceiverCashOnDelivery = null;
                    waybill.CompanyReceiverBankId = null;
                    waybill.CompanyReceiverBankPaymentNo = null;
                    waybill.CompanyReceiverCreditPayment = null;
                    waybill.CompanyReceiverResponsibleCustomerId = null;
                    waybill.EntranceDeliveryPerson = null;
                    waybill.EntranceTransfereePersonName = null;
                    waybill.EntranceTransfereePersonNationalCode = null;
                    break;
            }

            waybill.State = state;
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
