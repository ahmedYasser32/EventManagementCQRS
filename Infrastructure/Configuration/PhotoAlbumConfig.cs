using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace InfraStructure.Configuration
{
    public class PhotoAlbumConfig : IEntityTypeConfiguration<PhotoAlbum>
    {
     
        
        public void Configure(EntityTypeBuilder<PhotoAlbum> builder)
        {
            builder.HasKey(A => A.Id);

            builder.HasMany(A => A.Events)
                   .WithOne(e => e.PhotoAlbum)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(A => A.Photos)
                   .WithMany(p => p.Albums);
                   

            builder.Property(A => A.Title)
                .HasDefaultValue("No title");
            

        }
    }
}
