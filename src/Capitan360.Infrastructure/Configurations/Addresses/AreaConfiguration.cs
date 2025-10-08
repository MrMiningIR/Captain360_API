using Capitan360.Domain.Entities.Addresses;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Capitan360.Infrastructure.Configurations.Addresses;

public class AreaConfiguration : BaseEntityConfiguration<Area>
{
    public override void Configure(EntityTypeBuilder<Area> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.LevelId)
               .IsRequired()
               .HasColumnType("smallint");

        builder.Property(x => x.PersianName)
               .IsRequired()
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.EnglishName)
               .IsRequired()
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.Code)
               .IsRequired()
               .HasMaxLength(20)
               .IsUnicode(false)
               .HasColumnType("varchar(20)");

        builder.Property(x => x.Latitude)
               .IsRequired()
               .HasColumnType("decimal(9,6)");

        builder.Property(x => x.Longitude)
               .IsRequired()
               .HasColumnType("decimal(9,6)");

        builder.Property(x => x.ParentId)
               .IsRequired(false);

        builder.HasOne(x => x.Parent)
               .WithMany(p => p.Children)
               .HasForeignKey(x => x.ParentId)
               .OnDelete(DeleteBehavior.NoAction);

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
