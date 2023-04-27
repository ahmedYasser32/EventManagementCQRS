using AutoMapper;
using AutoMapper.QueryableExtensions;
using CQRS.Application.Features.Events.Models;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Application.Features.Events.Queries
{
    public class GetCategoriesQuery : IRequest<IEnumerable<CategoryDTO>>
    {
  


    }
    public class CategoryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryDTO>>
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext db;

        public CategoryHandler(IMapper mapper, ApplicationDbContext db)
        {
            this.db = db;
            this.mapper = mapper;

        }

        public async Task<IEnumerable<CategoryDTO>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {

                IEnumerable<CategoryDTO> result = await db.Category.ProjectTo<CategoryDTO>(mapper.ConfigurationProvider).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                return new List<CategoryDTO>();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
