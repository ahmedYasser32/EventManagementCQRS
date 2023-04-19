using AutoMapper;
using AutoMapper.QueryableExtensions;
using CQRS.Application.Features.Events.Models;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Application.Features.Events.Queries
{
    public class GetEventsQuery : IRequest<IEnumerable<EventListDTO>>
    {
        public string? search { get; set; }
        public int? SourceId { get; set; }


    }
    public class Handler : IRequestHandler<GetEventsQuery, IEnumerable<EventListDTO>>
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext db;

        public Handler(IMapper mapper, ApplicationDbContext db)
        {
            this.db = db;
            this.mapper = mapper;

        }

        public async Task<IEnumerable<EventListDTO>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var query = db.Event.AsQueryable();

                if (request.search != null)
                {
                    query = query.Where(x => x.Title.Contains(request.search));

                }

                if (request.SourceId != null)
                {
                    query = query.Where(x => x.Source.Id == request.SourceId);

                }

                List<EventListDTO> result = await query.Include(x=>x.Categories).Include(x => x.Source).ProjectTo<EventListDTO>(mapper.ConfigurationProvider).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                return new List<EventListDTO>();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
