using AutoMapper;
using AutoMapper.QueryableExtensions;
using CQRS.Application.Features.Events.Models;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Application.Features.Events.Queries
{
    public class GetEventDetailQuery : IRequest<EventDTO>
    {
        public int Id { get; set; } 

    }
    public class QueryHandler : IRequestHandler<GetEventDetailQuery, EventDTO>
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext db;

        public QueryHandler(IMapper mapper, ApplicationDbContext db)
        {
            this.db = db;
            this.mapper = mapper;

        }

       
        public async Task<EventDTO> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                EventDTO result = await db.Event.Where(x => x.Id == request.Id)
                    .Include(x => x.Categories)
                    .Include(x => x.Source)
                    .Include(x => x.PhotoAlbum)
                    .ProjectTo<EventDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync();

                if (result == null) throw new Exception("No Event Fouund");

                return result;
            }
            catch (Exception ex)
            {
                return null;
                Console.WriteLine(ex.Message);
            }

       }
    }
}

