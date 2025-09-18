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

        builder.Property(x => x.CompanyId).IsRequired();

        builder.Property(x => x.SmsPanelUserName)
               .HasMaxLength(100)
               .IsUnicode()
               .HasColumnType("nvarchar(100)");

        builder.Property(x => x.SmsPanelPassword)
               .HasMaxLength(100)
               .IsUnicode()
               .HasColumnType("nvarchar(100)");

        builder.Property(x => x.SmsPanelNumber)
               .HasMaxLength(100)
               .IsUnicode()
               .HasColumnType("nvarchar(100)");

        builder.Property(x => x.PatternSmsIssueSender)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");
        
        builder.Property(x => x.PatternSmsIssueReceiver)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.PatternSmsIssueCompany)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.PatternSmsSendSenderPeakSender)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.PatternSmsSendSenderPeakReceiver)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.PatternSmsPackageInCompanySender)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.PatternSmsPackageInCompanyReceiver)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.PatternSmsManifestSender)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.PatternSmsManifestReceiver)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.PatternSmsReceivedInReceiverCompanySender)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.PatternSmsReceivedInReceiverCompanyReceiver)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.PatternSmsSendReceiverPeakSender)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.PatternSmsSendReceiverPeakReceiver)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.PatternSmsDeliverSender)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.PatternSmsDeliverReceiver)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.PatternSmsCancelSender)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.PatternSmsCancelReceiver)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.PatternSmsCancelByCustomerSender)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.PatternSmsCancelByCustomerReceiver)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.PatternSmsCancelByCustomerCompany)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.PatternSmsSendManifestReceiverCompany)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");
        
        builder.HasOne(x => x.Company)
               .WithOne(c => c.CompanySmsPatterns)
               .HasForeignKey<CompanySmsPatterns>(x => x.CompanyId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}