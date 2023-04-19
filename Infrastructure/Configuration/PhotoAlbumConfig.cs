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
            builder.HasKey(p => p.Id);

            builder.HasMany(p => p.Events)
                   .WithOne(e => e.PhotoAlbum)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(p => p.Title)
                .HasDefaultValue("No title"); 
  
        }
    }
}
