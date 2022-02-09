using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.Location.Domain.Entities;

namespace SFA.DAS.Location.Data.Configuration
{
    public class PostcodeOutcodeConfiguration : IEntityTypeConfiguration<PostcodeOutcode>
    {
        public void Configure(EntityTypeBuilder<PostcodeOutcode> builder)
        {
            builder.ToTable("PostcodeOutcode");
            builder.HasKey(x => x.Outcode);
            builder.Property(x => x.Outcode).HasColumnName("Outcode");
            builder.Property(x => x.AreaName).HasColumnName("AreaName");
            builder.Property(x => x.PostalTown).HasColumnName("PostalTown");
        }
    }
}