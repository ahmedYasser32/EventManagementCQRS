using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public IEnumerable<PhotoAlbum> Albums { get; set; }

    }
}
