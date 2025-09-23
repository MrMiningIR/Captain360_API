using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.CompanyManifestForms;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Capitan360.Domain.Entities.CompanyManifestForms;

namespace Capitan360.Infrastructure.Repositories.CompanyManifestForms;

public class CompanyManifestFormRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : IManifestFormRepository
{
    public async Task<CompanyManifestForm?> GetManifestFormByIdAsync(int companyManifestFormId, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyManifestForm> query = dbContext.CompanyManifestForms;

        if (loadDataGeneralData)
        {
            query = query.Include(item => item.CompanyReceiver);
            query = query.Include(item => item.CompanySender);
            query = query.Include(item => item.DestinationCity);
            query = query.Include(item => item.SourceCity);
        }

        if (loadDataSenderCompany)
        {
            query = query.Include(item => item.Counter);
        }

        if (loadDataReceiverCompany)
        {
        }

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == companyManifestFormId, cancellationToken);
    }

    public async Task<CompanyManifestForm?> GetManifestFormByNoAsync(long companyManifestFormNo,  bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyManifestForm> query = dbContext.CompanyManifestForms;

        if (loadDataGeneralData)
        {
            query = query.Include(item => item.CompanyReceiver);
            query = query.Include(item => item.CompanySender);
            query = query.Include(item => item.DestinationCity);
            query = query.Include(item => item.SourceCity);
        }

        if (loadDataSenderCompany)
        {
            query = query.Include(item => item.Counter);
        }

        if (loadDataReceiverCompany)
        {
        }

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.No == companyManifestFormNo, cancellationToken);
    }
    
    public async Task<CompanyManifestForm?> GetManifestFormByNoAndCompanySenderIdAsync(long companyManifestFormNo, int companySenderId,  bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyManifestForm> query = dbContext.CompanyManifestForms;

        if (loadDataGeneralData)
        {
            query = query.Include(item => item.CompanyReceiver);
            query = query.Include(item => item.CompanySender);
            query = query.Include(item => item.DestinationCity);
            query = query.Include(item => item.SourceCity);
        }

        if (loadDataSenderCompany)
        {
            query = query.Include(item => item.Counter);
        }

        if (loadDataReceiverCompany)
        {
        }

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.No == companyManifestFormNo && item.CompanySenderId == companySenderId, cancellationToken);
    }

    public async Task<CompanyManifestForm?> GetManifestFormByNoAndCompanyReceiverIdAsync(long companyManifestFormNo, int companyReceiverId, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<CompanyManifestForm> query = dbContext.CompanyManifestForms;

        if (loadDataGeneralData)
        {
            query = query.Include(item => item.CompanyReceiver);
            query = query.Include(item => item.CompanySender);
            query = query.Include(item => item.DestinationCity);
            query = query.Include(item => item.SourceCity);
        }

        if (loadDataSenderCompany)
        {
            query = query.Include(item => item.Counter);
        }

        if (loadDataReceiverCompany)
        {
        }

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.No == companyManifestFormNo && item.CompanyReceiverId == companyReceiverId, cancellationToken);
    }

    public async Task<bool> CheckExistManifestFormByIdAsync(int companyManifestFormId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyManifestForms.AnyAsync(item => item.Id == companyManifestFormId, cancellationToken);
    }

    public async Task<bool> CheckExistManifestFormByNoAsync(long companyManifestFormNo, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyManifestForms.AnyAsync(item => item.No == companyManifestFormNo, cancellationToken);
    }

    public async Task<bool> CheckExistManifestFormByNoAndCompanySenderIdAsync(long companyManifestFormNo, int companySenderId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyManifestForms.AnyAsync(item => item.No == companyManifestFormNo && item.CompanySenderId == companySenderId, cancellationToken);
    }

    public async Task<bool> CheckExistManifestFormByNoAndCompanyReceiverIdAsync(long companyManifestFormNo, int companyReceiverId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyManifestForms.AnyAsync(item => item.No == companyManifestFormNo && item.CompanyReceiverId == companyReceiverId, cancellationToken);
    }

    public async Task<int> InsertManifestFormAsync(CompanyManifestForm companyManifestForm, CancellationToken cancellationToken)
    {
        dbContext.CompanyManifestForms.Add(companyManifestForm);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return companyManifestForm.Id;
    }

    public async Task DeleteManifestFormAsync(int companyManifestFormId, CancellationToken cancellationToken)
    {
        var companyManifestForm = await dbContext.CompanyManifestForms.SingleOrDefaultAsync(item => item.Id == companyManifestFormId, cancellationToken);
        dbContext.CompanyManifestForms.Remove(companyManifestForm);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<int>> GetManifestFormIdByManifestFormPeriodIdAndLessThanStartNumberAsync(int companyManifestFormPeriodId, long startNumber, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyManifestForms.Where(item => item.CompanyManifestFormPeriodId == companyManifestFormPeriodId && item.No < startNumber)
                                            .Select(item => item.Id)
                                            .ToListAsync(cancellationToken);
    }

    public async Task<List<int>> GetManifestFormIdByManifestFormPeriodIdAndGreatherThanEndNumberAsync(int companyManifestFormPeriodId, long endNumber, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyManifestForms.Where(item => item.CompanyManifestFormPeriodId == companyManifestFormPeriodId && item.No > endNumber)
                                            .Select(item => item.Id)
                                            .ToListAsync(cancellationToken);
    }

    public async Task<List<int>> GetManifestFormIdByManifestFormPeriodIdAsync(int companyManifestFormPeriodId, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyManifestForms.Where(item => item.CompanyManifestFormPeriodId == companyManifestFormPeriodId)
                                            .Select(item => item.Id)
                                            .ToListAsync(cancellationToken);
    }

    public async Task<bool> AnyIssunedManifestFormByDomesticPeriodIdStartNumberEndNumberAsync(int domesticPeriod, long startNumber, long endNumber, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyManifestForms.AnyAsync(item => item.CompanyManifestFormPeriodId == domesticPeriod && (item.No < startNumber || item.No > endNumber) && item.State != (short)WaybillState.Ready);
    }

    public async Task<bool> AnyIssunedManifestFormByDomesticPeriodIdAsync(int domesticPeriod, CancellationToken cancellationToken)
    {
        return await dbContext.CompanyManifestForms.AnyAsync(item => item.CompanyManifestFormPeriodId == domesticPeriod && item.State != (short)WaybillState.Ready);
    }
}
