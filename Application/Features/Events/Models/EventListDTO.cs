using CQRS.Application.Common.Mapping;
using Domain.Entities;

namespace CQRS.Application.Features.Events.Models
{
    public class EventListDTO : IMapFrom<Event>
    {
        public int Id { get; set; }
        public string ArabicTitle { get; set; }
        public string Title { get; set; }
        public string CoverPhotoPath { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Source Source { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; }
    }
}
