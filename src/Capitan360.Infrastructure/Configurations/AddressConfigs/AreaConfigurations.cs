using Capitan360.Domain.Entities.AddressEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Capitan360.Infrastructure.Configurations.AddressConfigs
{
    internal class AreaConfigurations : BaseEntityConfiguration<Area>
    {
        public override void Configure(EntityTypeBuilder<Area> builder)
        {
            base.Configure(builder);

            builder.Property(ar => ar.PersianName).IsRequired().HasMaxLength(50).IsUnicode();
            builder.Property(ar => ar.EnglishName).HasMaxLength(50);
            builder.Property(ar => ar.Code).IsRequired().HasMaxLength(10).IsUnicode(false);
            builder.Property(a => a.Latitude).HasDefaultValue(0);
            builder.Property(a => a.Longitude).HasDefaultValue(0);

            builder
                .HasOne(ar => ar.Parent)
                .WithMany(ar => ar.Children)
                .HasForeignKey(ar => ar.ParentId)
                .OnDelete(DeleteBehavior.Restrict);


            var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Data/SeedData/Areas.json");
            if (!File.Exists(jsonPath))
            {
                throw new FileNotFoundException($"فایل JSON در مسیر {jsonPath} یافت نشد.");
            }

            var jsonData = File.ReadAllText(jsonPath);
            var areas = JsonSerializer.Deserialize<List<Area>>(jsonData);

            if (areas != null && areas.Any())
            {
                builder.HasData(areas);
            }



        }
    }
}
