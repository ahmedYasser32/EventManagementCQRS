using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using Domain.Entities;

namespace InfraStructure.Configuration
{
  
    public class PhotoConfig : IEntityTypeConfiguration<Photo>
    {

        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Path)
               .HasDefaultValue("~/Images/no-img.jpg");

        }
    }

}
