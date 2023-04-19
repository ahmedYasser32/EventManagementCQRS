using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.Configuration
{
    public class SourceConfig : IEntityTypeConfiguration<Source>
    {
        public void Configure(EntityTypeBuilder<Source> builder)
        {
            builder.HasKey(s => s.Id);


            builder.HasMany(s => s.Events)
                   .WithOne(e => e.Source)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(e => e.Description)
                .HasMaxLength(500)
                .HasDefaultValue("");


        }
    }
}
