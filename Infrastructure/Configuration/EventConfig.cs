using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using Domain.Entities;

namespace InfraStructure.Configuration
{
  
    public class EventConfig : IEntityTypeConfiguration<Event>
    {

        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);


            builder.Property(e => e.CoverPhotoPath)
                .HasDefaultValue(@"C:\Users\Ahmad yasser\source\repos\CQRS_Task_IDSC\no-img.jpg");


            builder.HasMany(e => e.Categories)
                   .WithMany(c => c.Events);


            builder.Property(e=> e.Content)
                .HasMaxLength(500)
                .HasDefaultValue("No Content yet");

            builder.Property(e => e.Address)
               .HasDefaultValue("No Address yet");

        }
    }

}
