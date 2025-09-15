using Capitan360.Domain.Entities.Companies;
using Capitan360.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Companies;

public class CompanySmsPatternsConfigurations : BaseEntityConfiguration<CompanySmsPatterns>
{
    public override void Configure(EntityTypeBuilder<CompanySmsPatterns> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.PatternSmsIssueSender).IsRequired(false).HasMaxLength(500);
        builder.Property(x => x.PatternSmsIssueReceiver).IsRequired(false).HasMaxLength(500);
        builder.Property(x => x.PatternSmsIssueCompany).IsRequired(false).HasMaxLength(500);
        builder.Property(x => x.PatternSmsSendSenderPeakSender).IsRequired(false).HasMaxLength(500);
        builder.Property(x => x.PatternSmsSendSenderPeakReceiver).IsRequired(false).HasMaxLength(500);
        builder.Property(x => x.PatternSmsPackageInCompanySender).IsRequired(false).HasMaxLength(500);
        builder.Property(x => x.PatternSmsPackageInCompanyReceiver).IsRequired(false).HasMaxLength(500);
        builder.Property(x => x.PatternSmsManifestSender).IsRequired(false).HasMaxLength(500);
        builder.Property(x => x.PatternSmsManifestReceiver).IsRequired(false).HasMaxLength(500);
        builder.Property(x => x.PatternSmsReceivedInReceiverCompanySender).IsRequired(false).HasMaxLength(500);
        builder.Property(x => x.PatternSmsReceivedInReceiverCompanyReceiver).IsRequired(false).HasMaxLength(500);
        builder.Property(x => x.PatternSmsSendReceiverPeakSender).IsRequired(false).HasMaxLength(500);
        builder.Property(x => x.PatternSmsSendReceiverPeakReceiver).IsRequired(false).HasMaxLength(500);
        builder.Property(x => x.PatternSmsDeliverSender).IsRequired(false).HasMaxLength(500);
        builder.Property(x => x.PatternSmsDeliverReceiver).IsRequired(false).HasMaxLength(500);
        builder.Property(x => x.PatternSmsCancelSender).IsRequired(false).HasMaxLength(500);
        builder.Property(x => x.PatternSmsCancelReceiver).IsRequired(false).HasMaxLength(500);
        builder.Property(x => x.PatternSmsCancelByCustomerSender).IsRequired(false).HasMaxLength(500);
        builder.Property(x => x.PatternSmsCancelByCustomerReceiver).IsRequired(false).HasMaxLength(500);
        builder.Property(x => x.PatternSmsCancelByCustomerCompany).IsRequired(false).HasMaxLength(500);
        builder.Property(x => x.PatternSmsSendManifestReceiverCompany).IsRequired(false).HasMaxLength(500);
        builder.Property(x => x.SmsPanelNumber).IsRequired(false).HasMaxLength(100);
        builder.Property(x => x.SmsPanelPassword).IsRequired(false).HasMaxLength(100);
        builder.Property(x => x.SmsPanelUserName).IsRequired(false).HasMaxLength(100);


        builder.HasOne(x => x.Company).WithOne(x => x.CompanySmsPatterns)
            .HasForeignKey<CompanySmsPatterns>(x => x.CompanyId);



    }
}