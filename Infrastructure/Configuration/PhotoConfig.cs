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
               .HasDefaultValue(@"C:\Users\Ahmad yasser\source\repos\CQRS_Task_IDSC\no-img.jpg");

        }
    }

}
