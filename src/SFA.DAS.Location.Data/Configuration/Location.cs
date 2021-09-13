using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.Location.Data.Configuration
{
    public class Location: IEntityTypeConfiguration<Domain.Entities.Location>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Location> builder)
        {
            builder.ToTable("Location");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("int").IsRequired();
            builder.Property(x => x.LocationName).HasColumnName("LocationName").HasColumnType("varchar").HasMaxLength(256).IsRequired();
            builder.Property(x => x.CountyName).HasColumnName("CountyName").HasColumnType("varchar").HasMaxLength(256).IsRequired(false);
            builder.Property(x => x.LocalAuthorityName).HasColumnName("LocalAuthorityName").HasColumnType("varchar").HasMaxLength(256).IsRequired(false);
            builder.Property(x => x.Lat).HasColumnName("Lat").HasColumnType("float").IsRequired();
            builder.Property(x => x.Long).HasColumnName("Long").HasColumnType("float").IsRequired();
            builder.Property(x => x.Region).HasColumnName("Region");
            builder.Property(x => x.LocalAuthorityDistrict).HasColumnName("LocalAuthorityDistrict");
        }
    }
}