using Capitan360.Domain.Entities.Companies;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Companies;

public class CompanySmsPatternsConfiguration : BaseEntityConfiguration<CompanySmsPatterns>
{
    public override void Configure(EntityTypeBuilder<CompanySmsPatterns> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Id)
               .UseIdentityColumn(1, 1)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.CompanyId)
               .IsRequired();

        builder.Property(x => x.SmsPanelUserName)
               .IsRequired()
               .HasMaxLength(100)
               .IsUnicode()
               .HasColumnType("nvarchar(100)");

        builder.Property(x => x.SmsPanelPassword)
               .IsRequired()
               .HasMaxLength(100)
               .IsUnicode()
               .HasColumnType("nvarchar(100)");

        builder.Property(x => x.SmsPanelNumber)
               .IsRequired()
               .HasMaxLength(100)
               .IsUnicode()
               .HasColumnType("nvarchar(100)");

        builder.Property(x => x.PatternSmsIssueSender)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.ActivePatternSmsIssueSender)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.PatternSmsIssueReceiver)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.ActivePatternSmsIssueReceiver)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.PatternSmsIssueCompany)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.ActivePatternSmsIssueCompany)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.PatternSmsSendSenderPeakSender)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.ActivePatternSmsSendSenderPeakSender)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.PatternSmsSendSenderPeakReceiver)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.ActivePatternSmsSendSenderPeakReceiver)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.PatternSmsPackageInCompanySender)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.ActivePatternSmsPackageInCompanySender)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.PatternSmsPackageInCompanyReceiver)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.ActivePatternSmsPackageInCompanyReceiver)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.PatternSmsManifestSender)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.ActivePatternSmsManifestSender)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.PatternSmsManifestReceiver)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.ActivePatternSmsManifestReceiver)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.PatternSmsReceivedInReceiverCompanySender)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.ActivePatternSmsReceivedInReceiverCompanySender)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.PatternSmsReceivedInReceiverCompanyReceiver)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.ActivePatternSmsReceivedInReceiverCompanyReceiver)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.PatternSmsSendReceiverPeakSender)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.ActivePatternSmsSendReceiverPeakSender)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.PatternSmsSendReceiverPeakReceiver)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.ActivePatternSmsSendReceiverPeakReceiver)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.PatternSmsDeliverSender)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.ActivePatternSmsDeliverSender)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.PatternSmsDeliverReceiver)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.ActivePatternSmsDeliverReceiver)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.PatternSmsCancelSender)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.ActivePatternSmsCancelSender)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.PatternSmsCancelReceiver)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.ActivePatternSmsCancelReceiver)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.PatternSmsCancelByCustomerSender)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.ActivePatternSmsCancelByCustomerSender)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.PatternSmsCancelByCustomerReceiver)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.ActivePatternSmsCancelByCustomerReceiver)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.PatternSmsCancelByCustomerCompany)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.ActivePatternSmsCancelByCustomerCompany)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(x => x.PatternSmsSendManifestReceiverCompany)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.ActivePatternSmsSendManifestReceiverCompany)
               .IsRequired()
               .HasColumnType("bit");

        builder.HasOne(x => x.Company)
               .WithOne(c => c.CompanySmsPatterns)
               .HasForeignKey<CompanySmsPatterns>(x => x.CompanyId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}