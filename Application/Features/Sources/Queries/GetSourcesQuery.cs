using AutoMapper;
using AutoMapper.QueryableExtensions;
using CQRS.Application.Features.Sources.Models; 
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Application.Features.Sources.Queries
{
    public class GetSourcesQuery : IRequest<IEnumerable<SourceDTO>>
    {
  


    }
    public class Handler : IRequestHandler<GetSourcesQuery, IEnumerable<SourceDTO>>
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext db;

        public Handler(IMapper mapper, ApplicationDbContext db)
        {
            this.db = db;
            this.mapper = mapper;

        }

        public async Task<IEnumerable<SourceDTO>> Handle(GetSourcesQuery request, CancellationToken cancellationToken)
        {
            try
            {

                IEnumerable<SourceDTO> result = await db.Source.ProjectTo<SourceDTO>(mapper.ConfigurationProvider).ToListAsync();
                Console.WriteLine(result.Count());
                return result;
            }
            catch (Exception ex)
            {
                return new List<SourceDTO>();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
