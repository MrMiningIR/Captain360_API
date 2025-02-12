using Capitan360.Domain.Entities.AddressEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.AddressConfigs
{
    internal class AreaConfigurations : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(ar => ar.PersianName).IsRequired().HasMaxLength(50).IsUnicode();
            builder.Property(ar => ar.EnglishName).HasMaxLength(50);
            builder.Property(ar => ar.Code).IsRequired().HasMaxLength(10).IsUnicode(false);
            builder.Property(ar => ar.Coordinates).HasColumnType("geography");

            builder
                .HasOne(ar => ar.Parent) 
                .WithMany(ar => ar.Children)
                .HasForeignKey(ar => ar.ParentId) 
                .OnDelete(DeleteBehavior.Restrict); 

        }
    }
}
