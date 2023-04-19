using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PhotoAlbum
    {
       
        public int Id { get; set; }
        public string Title { get; set; }
       
        public List<Photo> Photos { get; set; }
        public IEnumerable<Event>? Events { get; set; }



    }
}
