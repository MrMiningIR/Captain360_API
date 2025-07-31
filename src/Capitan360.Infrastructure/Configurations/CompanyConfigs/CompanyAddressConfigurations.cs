namespace Capitan360.Infrastructure.Configurations.CompanyConfigs;

//internal class CompanyAddressConfigurations : BaseEntityConfiguration<CompanyAddress>
//{
//    //public override void Configure(EntityTypeBuilder<CompanyAddress> builder)
//    //{

//    //    builder.HasKey(ca => ca.Id);

//    //    builder.Property(ca => ca.Active).HasDefaultValue(true);
//    //    builder.Property(ca => ca.OrderAddress).IsRequired();



//    //    builder.HasOne(ca => ca.Company)
//    //           .WithMany(ca => ca.CompanyAddresses)
//    //           .HasForeignKey(ca => ca.CompanyId)
//    //           .OnDelete(DeleteBehavior.NoAction);

//    //    builder.HasOne(ca=>ca.Address)
//    //            .WithMany(ca=>ca.CompanyAddresses)
//    //            .HasForeignKey(ca => ca.AddressId)
//    //            .OnDelete(DeleteBehavior.NoAction);


//    //}
//}