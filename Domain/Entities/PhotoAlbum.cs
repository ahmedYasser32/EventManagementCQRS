using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PhotoAlbum
    {
        public PhotoAlbum()
        {
            PhotoPaths = new List<string>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public List<string> PhotoPaths { get; set; }
        public IEnumerable<Event>? Events { get; set; }



    }
}
