using CQRS.Application.Common.Mapping;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace CQRS.Application.Features.Events.Models
{
    public class EventDTO : IMapFrom<Event>
    {
        public int Id { get; set; }
        public string ArabicTitle { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Address { get; set; }
        public string CoverPhotoPath { get; set; } 
        public IFormFile  CoverPhoto { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SourceDTO Source { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; }
        public PhotoAlbumDTO PhotoAlbum { get; set; }
    }
}
