using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Event
    {
        public int Id { get; set; } 
        public string   ArabicTitle { get; set; }
        public string   Title { get; set; }   
        public string   Content { get; set; }     
        public string   Address { get; set; }    
        public string   CoverPhotoPath { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Source Source { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public PhotoAlbum PhotoAlbum { get; set; }

    }
}
