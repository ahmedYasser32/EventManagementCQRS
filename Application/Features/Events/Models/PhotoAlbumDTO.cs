using CQRS.Application.Common.Mapping;
using Domain.Entities;
namespace CQRS.Application.Features.Event.Models

{
    public class PhotoAlbumDTO :IMapFrom<PhotoAlbum>
    {
       
        public int Id { get; set; }
        public string Title { get; set; }
       
        public List<PhotoDTO> Photos { get; set; }




    }
}
