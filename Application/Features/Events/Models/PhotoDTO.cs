using CQRS.Application.Common.Mapping;
using Domain.Entities;

namespace CQRS.Application.Features.Events.Models

{
    public class PhotoDTO:IMapFrom<Photo>
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
       

    }
}
